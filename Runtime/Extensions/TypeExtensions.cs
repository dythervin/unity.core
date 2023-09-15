using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dythervin.Core.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Stack<StringBuilder> Stack = new Stack<StringBuilder>();

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

        private static readonly Dictionary<MemberInfo, Attribute[]> AttributesMap =
            new Dictionary<MemberInfo, Attribute[]>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCustomAttribute<TAttribute>(this MemberInfo type, out TAttribute attribute,
            bool cache = false)
            where TAttribute : Attribute
        {
            attribute = GetCustomAttribute<TAttribute>(type, cache);
            return attribute != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo type, bool cache = false)
            where TAttribute : Attribute
        {
            var attributes = GetAttributesAllAttributes(type, cache);

            TAttribute target = null;
            foreach (Attribute attribute in attributes)
            {
                if (attribute is TAttribute tAttribute)
                {
                    if (target != null)
                        throw new AmbiguousMatchException("More than one of the requested attributes was found.");

                    target = tAttribute;
                }
            }

            return target;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCustomAttributes<TAttribute>(this MemberInfo type, ICollection<TAttribute> result,
            bool cache = false)
            where TAttribute : Attribute
        {
            var attributes = GetAttributesAllAttributes(type, cache);

            foreach (Attribute attribute in attributes)
            {
                if (attribute is TAttribute tAttribute)
                {
                    result.Add(tAttribute);
                }
            }
        }

        private static Attribute[] GetAttributesAllAttributes(MemberInfo type, bool cache)
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
        public static bool Implements(this Type type, Type to)
        {
            return to.IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Implements<T>(this Type type)
        {
            return Implements(type, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Implements(this TypeInfo type, TypeInfo to)
        {
            return to.IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Implements(this TypeInfo type, Type to)
        {
            return to.IsAssignableFrom(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Implements<T>(this TypeInfo type)
        {
            return Implements(type, typeof(T));
        }

        public static string GetNiceName(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            StringBuilder builder = Stack.Count > 0 ? Stack.Pop() : new StringBuilder();
            builder.Append(type.GetGenericTypeDefinition().Name);
            builder.Remove(builder.Length - 2, 2);
            builder.Append('<');

            foreach (Type typeArgument in type.GenericTypeArguments)
                builder.Append(typeArgument.GetNiceName());

            builder.Append('>');
            string value = builder.ToString();
            builder.Clear();
            Stack.Push(builder);
            return value;
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

        public static FieldInfo GetFieldExt(this Type type, string name, BindingFlags bindingAttr)
        {
            FieldInfo fieldInfo = type.GetField(name, bindingAttr);
            if (bindingAttr.HasFlagFast(BindingFlags.NonPublic) &&
                bindingAttr.HasFlagFast(BindingFlags.FlattenHierarchy))
            {
                while (fieldInfo == null)
                {
                    fieldInfo = type.GetField(name, bindingAttr);
                    if (fieldInfo == null)
                    {
                        type = type.BaseType;
                        if (type == null)
                            break;
                    }
                }
            }

            return fieldInfo;
        }
    }
}