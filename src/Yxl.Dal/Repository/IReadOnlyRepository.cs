using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yxl.Dapper.Extensions.Wrapper;

namespace Yxl.Dal.Repository
{
    public interface IReadOnlyRepository<T> :
        ICompare<Expression<Func<T, object>>, ReadOnlyRepository<T>>,
        IFunction<Expression<Func<T, object>>, ReadOnlyRepository<T>>
    {

    }
}
