﻿using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using System.Linq;

namespace Yxl.Dapper.Extensions.SqlWhere.Impl
{
    public abstract class BaseSqlWhere : ISqlItemWhere
    {

        public virtual Operator Op { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IFiled Filed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetColumnName(ISqlDialect sqlDialect)
        {
            return Filed.GetSqlWhereColumnName(sqlDialect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract SqlInfo GetSql(ISqlDialect sqlDialect, ref IList<Parameter> parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string GetParamName(ISqlDialect sqlDialect, ref IList<Parameter> parameters, int index = 1)
        {
            var key = $"{Filed.GetParameterName(sqlDialect)}_{index}";
            if (parameters.FirstOrDefault(a => a.Name == key) == null)
            {
                return key;
            }
            index++;
            return GetParamName(sqlDialect, ref parameters, index);
        }
    }



}