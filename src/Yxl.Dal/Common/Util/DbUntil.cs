using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Yxl.Dal.Attributes;

namespace Yxl.Dal.Common.Util
{
    public static class DbUntil
    {
        public static string? GetDbName<T>()
        {
            return typeof(T).GetCustomAttribute<DBAttribute>(false)?.Name ?? string.Empty;
        }
    }
}
