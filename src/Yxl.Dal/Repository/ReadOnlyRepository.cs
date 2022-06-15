using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yxl.Dapper.Extensions.Enum;
using Yxl.Dapper.Extensions.Wrapper.Impl;

namespace Yxl.Dal.Repository
{
    public class ReadOnlyRepository<T> : Compare<Expression<Func<T, object>>, ReadOnlyRepository<T>>, IReadOnlyRepository<T>
    {
       
    }
}
