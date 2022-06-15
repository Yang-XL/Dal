using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlWhere.Impl;
using System.Collections;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{
    public abstract class Compare<TColumn, Children> :
        Nested<Children>,
        ICompare<TColumn, Children> where Children : class, ICompare<TColumn, Children>, new()
    {

        public virtual Children Between(TColumn columnName, object from, object to)
        {
            return AddSqlItem(new BetweenWhere(GetColumn(columnName), from, to));
        }

        public Children EndWith(TColumn columnName, string val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.EndWith, "%" + val));
        }

        public Children Eq(TColumn columnName, object val)
        {
            return Eq(GetColumn(columnName), val);
        }
        public Children Eq(IFiled filed, object val)
        {
            return AddSqlItem(new CompareSqlWhere(filed, Operator.Eq, val));
        }

        public Children Ge(TColumn columnName, object val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Ge, val));
        }

        public Children Gt(TColumn columnName, object val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Gt, val));
        }

        public Children In(TColumn columnName, IEnumerable data)
        {
            return AddSqlItem(new InSqlWhere(GetColumn(columnName), Operator.In, data));
        }

        public Children Le(TColumn columnName, object val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Le, val));
        }

        public Children Like(TColumn columnName, string val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Like, "%" + val + "%"));
        }

        public Children Lt(TColumn columnName, object val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Lt, val));
        }

        public Children Ne(TColumn columnName, object val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.Ne, val));
        }

        public Children StartsWith(TColumn columnName, string val)
        {
            return AddSqlItem(new CompareSqlWhere(GetColumn(columnName), Operator.StartsWith, val + "%"));
        }

        protected virtual IFiled GetColumn(TColumn column)
        {
            if (column is string)
            {
                return new Filed(column as string);
            }
            throw new System.NotImplementedException("TColumn is not default type");
        }

        /// <summary>
        /// 拼接一个内部使用的 如逻辑删除 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal Children AppendEq(IFiled column, object val)
        {
            return AddSqlItem(new CompareSqlWhere(column, Operator.Eq, val));
        }

    }
}
