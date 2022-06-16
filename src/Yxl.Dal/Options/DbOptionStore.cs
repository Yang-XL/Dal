using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Attributes;

namespace Yxl.Dal.Options
{
    public static class DbOptionStore
    {
        private static readonly ConcurrentDictionary<string, DbOptions> store = new ConcurrentDictionary<string, DbOptions>();

        public static void AddOptions(DbOptions options)
        {
            if (!store.TryAdd(options.Name, options))
            {
                throw new ArgumentException($"name {options.Name} is config");
            }
        }

        public static DbOptions GetOptions(string name)
        {
            if (!store.TryGetValue(name, out var db))
            {
                throw new ArgumentException($"name {name} is not config");
            }
            return db;
        }

        public static DbOptions GetOptions<T>() where T : IEntity
        {
            var db = typeof(T).GetCustomAttribute<DBAttribute>(true);
            if (db != null)
            {
                return GetOptions(db.Name);
            }
            return store.First().Value;
        }
    }
}
