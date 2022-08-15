using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dythervin.Core.Extensions
{
    public static class Types
    {
        private static readonly Stack<StringBuilder> Stack = new Stack<StringBuilder>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ImplementsOrInherits(this Type type, Type to)
        {
            return to.IsAssignableFrom(type);
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
            Stack.Push(builder);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Instantiatable(this Type type)
        {
            return !type.IsInterface && !type.IsGenericTypeDefinition && !type.IsAbstract;
        }

        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                Type cur = toCheck.IsGenericType
                    ? toCheck.GetGenericTypeDefinition()
                    : toCheck;
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
    }
}