using System;
using System.Collections.Generic;
using Yxl.Dal.Common.Util;
using Yxl.Dal.Options;

namespace Yxl.Dal.Context
{
    internal class DbContextProvider
    {
        private static Dictionary<string, IDbContext> store = new Dictionary<string, IDbContext>();

        internal static IDbContext GetDbContext(string dbName)
        {
            return store.TryGetValue(dbName, out var conetxt) ? conetxt : throw new NullReferenceException(nameof(dbName));
        }

        internal static IDbContext GetDbContext<T>()
        {
            return GetDbContext(DbUntil.GetDbName<T>());
        }

        internal static bool Register(DbOptions options)
        {
            return store.TryAdd(options.Name, new DbContext(options));
        }
    }
}
