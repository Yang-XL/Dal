using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using System.Collections.Generic;
using System.Linq;
using Yxl.Dapper.Extensions.Core;

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
        public abstract void GetSql(ISqlDialect sqlDialect, ref SqlInfo sqlWhere);
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string GetParamName(ISqlDialect sqlDialect, object val, ref SqlInfo sqlWhereItem)
        {            
            var key = $"{Filed.GetParameterName(sqlDialect)}_W";
            return sqlWhereItem.AddParameter(key, val);
        }
    }



}
