using System;
using System.Linq;

namespace ExtensionSugar
{
    public static class AttributeExtSugar
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        public static TValue GetPropAttributeValue<TAttribute, TValue>(
                            this Type type,
                            string MemberName,
                            Func<TAttribute, TValue> valueSelector,
                            bool inherit = false)
                        where TAttribute : Attribute
        {
            var att = type
                        .GetMember(MemberName)
                            .FirstOrDefault()
                            .GetCustomAttributes(
                                                typeof(TAttribute),
                                                inherit
                                                )
                                                .FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}
