using System;
using System.Globalization;

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
        public static object ChangeStrType(this string aText, Type aType)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.CurrencyDecimalSeparator = ",";
            nfi.CurrencyGroupSeparator = "";
            nfi.PercentDecimalSeparator = ",";
            nfi.PercentGroupSeparator = "";
            nfi.NumberDecimalSeparator = ",";
            nfi.NumberGroupSeparator = "";

            switch (Type.GetTypeCode(aType))
            {
                case TypeCode.Boolean:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Byte:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Char:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.DBNull:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.DateTime:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Decimal:
                    return aText.IsNullOrEmpty() ? 0 : Convert.ChangeType(aText.Replace(".", ","), aType);
                case TypeCode.Double:
                    return aText.IsNullOrEmpty() ? 0 :
                        Convert.ChangeType(Convert.ChangeType(aText, aType).ToString().Replace(".", ","), aType);
                case TypeCode.Empty:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Int16:
                    return aText.IsNullOrEmpty() ? 0 : Convert.ChangeType(aText, aType);
                case TypeCode.Int32:
                    return aText.IsNullOrEmpty() ? 0 : Convert.ChangeType(aText, aType);
                case TypeCode.Int64:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Object:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.SByte:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.Single:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.String:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.UInt16:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.UInt32:
                    return Convert.ChangeType(aText, aType);
                case TypeCode.UInt64:
                    return Convert.ChangeType(aText, aType);
                default:
                    return Convert.ChangeType(aText, aType);
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
    }
}
