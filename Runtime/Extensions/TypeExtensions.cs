using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEditor.Callbacks;

namespace Dythervin
{
    public static class TypeExtensions
    {
        private const bool CacheDefault = false;

        private static readonly IReadOnlyDictionary<Type, string> PrimitiveNames = new Dictionary<Type, string>()
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(object), "object" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
        };

        private static readonly Dictionary<MemberInfo, IReadOnlyList<Attribute>> AttributesMap =
            new Dictionary<MemberInfo, IReadOnlyList<Attribute>>();

        private static readonly Dictionary<Type, Type[]> GenericTypeArgumentsMap = new Dictionary<Type, Type[]>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCustomAttribute<TAttribute>(this MemberInfo type,
            [NotNullWhen(true)] out TAttribute? attribute, bool cache = CacheDefault)
            where TAttribute : Attribute
        {
            var attributes = GetAttributesAllAttributes(type, cache);

            for (var i = 0; i < attributes.Count; i++)
            {
                Attribute attribute1 = attributes[i];
                if (attribute1 is TAttribute tAttribute)
                {
                    attribute = tAttribute;
                    return true;
                }
            }

            attribute = null;
            return false;
        }

        [Pure]
        public static IReadOnlyList<Type> GetGenericTypeArguments(this Type type, bool cache = CacheDefault)
        {
            if (!GenericTypeArgumentsMap.TryGetValue(type, out var arguments))
            {
                arguments = type.GenericTypeArguments;
                if (cache)
                    GenericTypeArgumentsMap.Add(type, arguments);
            }

            return arguments;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAttribute? GetCustomAttribute<TAttribute>(this MemberInfo type, bool cache = CacheDefault)
            where TAttribute : Attribute
        {
            TryGetCustomAttribute(type, out TAttribute? attribute, cache);
            return attribute;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCustomAttributes<TAttribute>(this MemberInfo type, ICollection<TAttribute> result,
            bool cache = CacheDefault)
            where TAttribute : Attribute
        {
            var attributes = GetAttributesAllAttributes(type, cache);

            for (var i = 0; i < attributes.Count; i++)
            {
                Attribute attribute = attributes[i];
                if (attribute is TAttribute tAttribute)
                {
                    result.Add(tAttribute);
                }
            }
        }

        private static IReadOnlyList<Attribute> GetAttributesAllAttributes(MemberInfo type, bool cache)
        {
            if (!AttributesMap.TryGetValue(type, out var attributes))
            {
                attributes = CustomAttributeExtensions.GetCustomAttributes(type, true).ToArray();
                if (cache)
                    AttributesMap.Add(type, attributes);
            }

            return attributes;
        }

        public static string GetNameOrAlias(this Type type)
        {
            if (!PrimitiveNames.TryGetValue(type, out string value))
                value = type.Name;

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Is(this Type type, Type to)
        {
            return to.IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Is<T>(this Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNot(this Type type, Type to)
        {
            return !to.IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNot<T>(this Type type)
        {
            return !typeof(T).IsAssignableFrom(type);
        }

        public static string GetNiceName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            using (SharedPools.StringBuilder.Get(out StringBuilder builder))
            {
                builder.Append(type.GetGenericTypeDefinition().Name);
                builder.Remove(builder.Length - 2, 2);
                builder.Append('<');

                foreach (Type typeArgument in type.GenericTypeArguments)
                {
                    builder.Append(typeArgument.GetNiceName());
                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);

                builder.Append('>');
                return builder.ToStringAndClear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInstantiatable(this Type type)
        {
            return !type.IsAbstract && !type.IsGenericTypeDefinition;
        }

        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        public static bool TryGetBaseGeneric(this Type type, out Type baseGenericType)
        {
            do
            {
                baseGenericType = type.BaseType;
                if (baseGenericType == null)
                    return false;
            } while (!baseGenericType.IsGenericType);

            return true;
        }

        public static FieldInfo? GetFieldExt(this Type type, string name, BindingFlags bindingAttr)
        {
            FieldInfo? fieldInfo = type.GetField(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                Type? targetType = type;
                while (fieldInfo == null)
                {
                    fieldInfo = targetType.GetField(name, bindingAttr);
                    if (fieldInfo == null)
                    {
                        targetType = targetType.BaseType;
                        if (targetType == null)
                            break;
                    }
                }
            }

            return fieldInfo;
        }

        public static MethodInfo? GetMethodExt(this Type type, string name, BindingFlags bindingAttr)
        {
            MethodInfo? methodInfo = type.GetMethod(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                Type? targetType = type;
                while (methodInfo == null)
                {
                    methodInfo = targetType.GetMethod(name, bindingAttr);
                    if (methodInfo == null)
                    {
                        targetType = targetType.BaseType;
                        if (targetType == null)
                            break;
                    }
                }
            }

            return methodInfo;
        }

        public static PropertyInfo? GetPropertyExt(this Type type, string name, BindingFlags bindingAttr)
        {
            PropertyInfo? propertyInfo = type.GetProperty(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                Type? targetType = type;
                while (propertyInfo == null)
                {
                    propertyInfo = targetType.GetProperty(name, bindingAttr);
                    if (propertyInfo == null)
                    {
                        targetType = targetType.BaseType;
                        if (targetType == null)
                            break;
                    }
                }
            }

            return propertyInfo;
        }

        public static EventInfo? GetEventExt(this Type type, string name, BindingFlags bindingAttr)
        {
            EventInfo? eventInfo = type.GetEvent(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                Type? targetType = type;
                while (eventInfo == null)
                {
                    eventInfo = targetType.GetEvent(name, bindingAttr);
                    if (eventInfo == null)
                    {
                        targetType = targetType.BaseType;
                        if (targetType == null)
                            break;
                    }
                }
            }

            return eventInfo;
        }

        public static Type? GetNestedTypeExt(this Type type, string name, BindingFlags bindingAttr)
        {
            Type? nestedType = type.GetNestedType(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                Type? targetType = type;
                while (nestedType == null)
                {
                    nestedType = targetType.GetNestedType(name, bindingAttr);
                    if (nestedType == null)
                    {
                        targetType = targetType.BaseType;
                        if (targetType == null)
                            break;
                    }
                }
            }

            return nestedType;
        }

        public static Type[] GetInterfacesExt(this Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            if (interfaces.Length == 0)
            {
                Type? baseType = type.BaseType;
                while (baseType != null)
                {
                    interfaces = baseType.GetInterfaces();
                    if (interfaces.Length > 0)
                        return interfaces;

                    baseType = baseType.BaseType;
                }
            }

            return interfaces;
        }

#if UNITY_EDITOR
        [DidReloadScripts]
        private static void OnCompile()
        {
            AttributesMap.Clear();
            GenericTypeArgumentsMap.Clear();
        }
#endif
    }
}