using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Uitls;
using Yxl.Dapper.Extensions.Wrapper;
using Yxl.Dapper.Extensions.Wrapper.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yxl.Dapper.Extensions.Core;

namespace Yxl.Dapper.Extensions
{
    public class SqlInsertBuilder<T> : ISqlBuilder
    {
        private readonly T model;
        public SqlInsertBuilder(T model)
        {
            this.model = model;
        }

        public SqlInfo GetSql(ISqlDialect sqlDialect)
        {

            var sql = "INSERT INTO {0} ({1}) VALUES ({2})";
            Dictionary<IFiled, object> insertDic = new Dictionary<IFiled, object>();
            foreach (var item in typeof(T).CreateFiles())
            {
                if (item.IgnoreInsert || item.FunctionColumn || item.UpdatedAt) continue;
                if (item.CreateAt)
                {
                    insertDic.Add(item, DateTime.Now);
                    continue;
                }
                insertDic.Add(item, item.MetaData.GetValue(model));
            }
            sql = string.Format(sql, typeof(T).CreateTable().GetTableName(sqlDialect),
                 string.Join(",", insertDic.Keys.Select(a => a.Name)),
                 string.Join(",", insertDic.Keys.Select(a => a.GetParameterName(sqlDialect))));
            var param = insertDic.Select(a => new Parameter(a.Key.GetParameterName(sqlDialect), a.Value));
            return new SqlInfo(sql, param);
        }
    }

}
