using Yxl.Dapper.Extensions.Wrapper.Impl;
using System;
using System.Linq.Expressions;

namespace Yxl.Dapper.Extensions
{
    public class SqlWhereBuilder<T> : Compare<Expression<Func<T, object>>, SqlWhereBuilder<T>>
    {
        public SqlWhereBuilder()
        {
        }
    }
      
    public class SqlWhereBuilder
    {
        public static SqlWhereBuilder<T> Build<T>()
        {
            return new SqlWhereBuilder<T>();
        }
    }

}
