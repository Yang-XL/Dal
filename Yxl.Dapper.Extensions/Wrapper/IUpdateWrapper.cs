using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.SqlDialect;
using Yxl.Dapper.Extensions.Wrapper.Impl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Yxl.Dapper.Extensions.Wrapper
{
    public interface IUpdateWrapper<T> : ICompare<Expression<Func<T, object>>, UpdateWrapper<T>>
    {

        IList<IUpdateFiled> Fileds { get; }

        ITable Table { get; }

        IUpdateWrapper<T> Set(params IUpdateFiled[] fileds);

        IUpdateWrapper<T> Set(T entity);

        IUpdateWrapper<T> Set(Expression<Func<T, object>> colum, object value);

        SqlInfo CreateSqlInfo(ISqlDialect sqlDialect, SqlInfo sqlWhere);
    }
}
