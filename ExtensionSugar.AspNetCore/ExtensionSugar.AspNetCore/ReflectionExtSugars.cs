using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExtensionSugar.AspNetCore
{
    public static class ReflectionExtSugars
    {
        public static PropertyInfo GetProperty<T>(this T ClassObject, string PropName) where T : class
        {
            return ClassObject.GetType().GetProperty(PropName);
        }
        public static object GetPropertyValue<T>(this T ClassObject, string PropName) where T : class
        {
            return ClassObject.GetProperty(PropName).GetValue(ClassObject);
        }
    }
}
