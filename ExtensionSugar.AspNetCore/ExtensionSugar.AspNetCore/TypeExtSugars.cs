using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExtensionSugar
{
    public static class TypeExtSugars
    {
        public static Boolean IsDouble(this Type aType)
        {
            switch (Type.GetTypeCode(aType))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                    return true;
                default:
                    return false;
            }
        }
        public static Boolean IsDateTime(this Type aType)
        {
            switch (Type.GetTypeCode(aType))
            {
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }
        public static Boolean IsInt(this Type aType)
        {
            switch (Type.GetTypeCode(aType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            else
                return null;
        }
        public static object ToDefault(this Type targetType)
        {

            if (targetType == null)
                throw new NullReferenceException();

            var mi = typeof(TypeExtSugars)
                .GetMethod("_ToDefaultHelper", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            var generic = mi.MakeGenericMethod(targetType);

            var returnValue = generic.Invoke(null, new object[0]);
            return returnValue;
        }
        static T _ToDefaultHelper<T>()
        {
            return default(T);
        }
        public static bool IsCollection(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>));
        }
        public static bool IsHasCollectionInterface(this Type type)
        {
            return type.GetInterfaces().Any(typ =>
                     typ.IsGenericType &&
                     typ.GetGenericTypeDefinition() == typeof(ICollection<>)
                    );
        }
    }
}
