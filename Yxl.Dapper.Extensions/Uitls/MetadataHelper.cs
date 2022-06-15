using Yxl.Dapper.Extensions.Attributes;
using Yxl.Dapper.Extensions.Metadata;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Yxl.Dapper.Extensions.Uitls
{
    public static class MetadataHelper
    {
        private static readonly ConcurrentDictionary<Type, ITable> _metaDataTableCache = new ConcurrentDictionary<Type, ITable>();
        private static readonly ConcurrentDictionary<Type, IEnumerable<IFiled>> _metaDataFiledsCache = new ConcurrentDictionary<Type, IEnumerable<IFiled>>();

        public static ITable CreateTable(this Type objectType, string alais = "")
        {
            if (_metaDataTableCache.TryGetValue(objectType, out var cachedEntry))
                return cachedEntry;
            var tableAttribute = objectType.GetCustomAttribute<TableAttribute>();
            var table = new Table(tableAttribute.Name, alais, tableAttribute?.Schema ?? "");
            _metaDataTableCache.TryAdd(objectType, table);
            return table;
        }

        public static IEnumerable<IFiled> CreateFiles(this Type objectType)
        {
            if (_metaDataFiledsCache.TryGetValue(objectType, out var cachedEntry))
                return cachedEntry;

            var props = objectType.GetProperties();
            var propertyInfos = props
                .OrderByDescending(x => x.GetCustomAttribute<KeyAttribute>() != null)
                .ThenBy(p => p.GetCustomAttributes<ColumnAttribute>()
                    .Select(a => a.Order)
                    .DefaultIfEmpty(int.MaxValue)
                    .FirstOrDefault()).Where(ExpressionHelper.GetPrimitivePropertiesPredicate())
                .Where(p => !p.GetCustomAttributes<NotMappedAttribute>().Any())
                .Where(igs => !igs.GetCustomAttributes<IgnoreAttribute>().Any(ig => ig.Ignore == Enum.IgnoreEnum.All))
                .Select(p => p.CreateFiled(objectType)).ToList();
            _metaDataFiledsCache.TryAdd(objectType, propertyInfos);
            return propertyInfos;
        }


        public static IFiled CreateFiled(this PropertyInfo propertyInfo, Type type)
        {
            return new Filed()
            {
                IgnoreInsert = propertyInfo.GetCustomAttributes<IgnoreAttribute>()?.Any(ig => ig.Ignore == Enum.IgnoreEnum.Insert) ?? false,
                IgnoreSelect = propertyInfo.GetCustomAttributes<IgnoreAttribute>()?.Any(ig => ig.Ignore == Enum.IgnoreEnum.Select) ?? false,
                IgnoreUpdate = propertyInfo.GetCustomAttributes<IgnoreAttribute>()?.Any(ig => ig.Ignore == Enum.IgnoreEnum.Update) ?? false,
                LogicalDelete = propertyInfo.GetCustomAttributes<LogicalDeleteAttribute>().Any(),
                UpdatedAt = propertyInfo.GetCustomAttributes<UpdatedAtAttribute>().Any(),
                TenantId = propertyInfo.GetCustomAttributes<TenantAttribute>().Any(),
                Key = propertyInfo.GetCustomAttributes<KeyAttribute>().Any(),
                CreateAt = propertyInfo.GetCustomAttributes<CreateAtAttribute>().Any(),
                Name = propertyInfo.GetCustomAttributes<ColumnAttribute>()?.FirstOrDefault()?.Name ?? propertyInfo.Name,
                Table = type.CreateTable(),
                MetaData = propertyInfo
            };
        }
    }
}
