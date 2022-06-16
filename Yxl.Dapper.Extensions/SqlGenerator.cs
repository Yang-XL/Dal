using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using Yxl.Dapper.Extensions.Wrapper;
using Yxl.Dapper.Extensions.Wrapper.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yxl.Dapper.Extensions
{
    //public class SqlGenerator<T> : ISqlGenerator<T>
    //{
    //    private readonly ISqlDialect SqlDialect;

    //    public SqlGenerator(ISqlDialect sqlDialect)
    //    {
    //        Init();
    //        SqlDialect = sqlDialect;
    //    }

    //    public ITable Table { get; set; }

    //    public IEnumerable<IFiled> Fileds { get; set; }

    //    /// <inheritdoc />
    //    public bool HasUpdatedAt => Fileds.Any(a => a.UpdatedAt);

    //    /// <inheritdoc />

    //    /// <inheritdoc />
    //    public bool LogicalDelete => Fileds.Any(a => a.LogicalDelete);




    //    private void Init()
    //    {
    //        var entityType = typeof(T);
    //        Table = entityType.CreateTable();
    //        Fileds = entityType.CreateFiles();
    //    }


    //    public SqlInfo GetCountSql(IQueryWrapper<T> queryWrapper)
    //    {
    //        var sqlData = queryWrapper.GetSqlWhere(SqlDialect);

    //        var sqlBuilder = new StringBuilder();

    //        sqlBuilder.Append($"SELECT COUNT(*) AS Total FROM {Table.GetTableName(SqlDialect)}");
    //        if (!string.IsNullOrEmpty(sqlData.Sql))
    //        {
    //            sqlBuilder.Append(" WHERE ");
    //            sqlBuilder.Append(sqlData.Sql);
    //        }
    //        return new SqlInfo { Parameters = sqlData.Parameters, Sql = sqlBuilder.ToString() };

    //    }
              

    //    public SqlInfo GetDeleteSql(IQueryWrapper<T> queryWrapper)
    //    {
    //        var sqlData = queryWrapper.GetSqlWhere(SqlDialect);

    //        if (LogicalDelete)
    //        {
    //            var update = new SqlUpdateBuilder<T>();
    //            foreach (var item in Fileds.Where(a => a.LogicalDelete))
    //            {
    //                update.Set(item, true);
    //            }
    //            return update.CreateSqlInfo(SqlDialect, sqlData);
    //        }

    //        var sqlBuilder = new StringBuilder();
    //        sqlBuilder.Append($"DELETE FROM {Table.GetTableName(SqlDialect)}");
    //        if (!string.IsNullOrEmpty(sqlData.Sql))
    //        {
    //            sqlBuilder.Append(" WHERE ");
    //            sqlBuilder.Append(sqlData.Sql);
    //        }
    //        return new SqlInfo { Parameters = sqlData.Parameters, Sql = sqlBuilder.ToString() };

    //    }

    //    public SqlInfo GetUpdate(IUpdateWrapper<T> wrapper)
    //    {
    //        return wrapper.GetSql(SqlDialect);
    //    }


    //    public PageSqlInfo GetPaged(IQueryWrapper<T> wrapper)
    //    {

    //        return new PageSqlInfo
    //        {
    //            TotalSql = GetCountSql(wrapper),
    //            PagedSql = wrapper.GetSql(SqlDialect),
    //        };
    //    }

    //    /// <summary>
    //    ///     Get SQL for INSERT Query
    //    /// </summary>
    //    public SqlInfo GetInsertSql(T model)
    //    {
    //        var sql = "INSERT INTO {0} ({1}) VALUES ({2})";
    //        Dictionary<IFiled, object> insertDic = new Dictionary<IFiled, object>();

    //        foreach (var item in Fileds)
    //        {
    //            if (item.IgnoreInsert || item.FunctionColumn || item.UpdatedAt) continue;
    //            if (item.CreateAt)
    //            {
    //                insertDic.Add(item, DateTime.Now);
    //                continue;
    //            }
    //            insertDic.Add(item, model.GetType().GetProperty(item.MetaData.Name).GetValue(model));
    //        }
    //        sql = string.Format(sql, Table.GetTableName(SqlDialect),
    //             string.Join(",", insertDic.Keys.Select(a => a.Name)),
    //             string.Join(",", insertDic.Keys.Select(a => a.GetParameterName(SqlDialect))));
    //        var param = insertDic.Select(a => new Parameter(a.Key.GetParameterName(SqlDialect), a.Value));
    //        return new SqlInfo(sql, param);
    //    }




    //}
}
