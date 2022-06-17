using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Yxl.Dal.Attributes;

namespace Yxl.Dal.Common.Util
{
    public static class DbUntil
    {

        private static Dictionary<Type, string> store = new Dictionary<Type, string>();

        public static string? GetDbName<T>()
        {
            if (store.TryGetValue(typeof(T), out var name))
            {
                return name;
            }
            name = typeof(T).GetCustomAttribute<DBAttribute>(false)?.Name ?? string.Empty;
            store.Add(typeof(T), name);
            return name;
        }
    }
}
