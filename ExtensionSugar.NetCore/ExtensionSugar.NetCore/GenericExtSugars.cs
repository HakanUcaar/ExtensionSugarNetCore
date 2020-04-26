using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExtensionSugar
{
    public static class GenericExtSugars
    {
        public static bool IsNull<T>(this T obj) 
        {
            if (obj is string)
            {
                return (obj as string).IsNullOrEmpty();
            }
            return obj == null;
        }
        public static bool IsNotNull<T>(this T obj) 
        {
            if (obj is string)
            {
                return !(obj as string).IsNullOrEmpty();
            }
            return obj != null;
        }

        public static T With<T>(this T item, Action<T> action)
        {
            if (item.IsNotNull())
            {
                action(item);
            }
            return item;
        }
        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
        {
            if (o.IsNotNull())
            {
                action(o);
            }
            return o;
        }

        public static bool In<T>(this T obj, params T[] args)
        {
            return args.Contains(obj);
        }
        public static bool In<T>(this T obj, String[] args)
        {
            return Array.IndexOf(args, obj) > -1;
        }
        public static bool In<T>(this T obj, IEnumerable<T> args)
        {
            return args.Contains(obj);
        }
        public static bool In<T>(this T obj, List<T> args)
        {
            return args.Contains(obj);
        }

        public static T ReturnSelf<T>(this T Input, Func<T, bool> check, T failureValue)
            where T : class
        {
            if (Input.IsNull()) return failureValue;
            try
            {
                return check(Input) ? Input : failureValue;
            }
            catch (Exception e)
            {
                return failureValue;
            }
        }
        public static T To<T>(this object o) where T : class
        {
            try
            {
                return o as T;
            }
            catch (Exception)
            {
                try
                {
                    return (T)Convert.ChangeType(o, typeof(T));
                }
                catch (Exception)
                {
                    return default(T);
                }                
            }            
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
            return enumeration;
        }

        public static object GetPropertyValue<T>(this T classInstance, string propertyName)
        {
            PropertyInfo property = classInstance.GetType().GetProperty(propertyName);
            if (property != null)
                return property.GetValue(classInstance, null);
            return null;
        }
    }
}
