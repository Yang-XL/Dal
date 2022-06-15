using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Wrapper.Impl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Yxl.Dapper.Extensions.Wrapper
{
    public interface IQueryWrapper<TColumn, Children>
        : ICompare<TColumn, Children>, IFunction<TColumn, Children> 
        where Children : ICompare<TColumn, Children>
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        Children Select(params Filed[] fileds);
    }

    public interface IQueryWrapper<T>
        : IQueryWrapper<Expression<Func<T, object>>, QueryWrapper<T>>
    {

        IQueryWrapper<T> Select(Expression<Func<T, object>> filedName);

        IQueryWrapper<T> Select(params string[] filedName);

        IQueryWrapper<T> Paged(int pageIndex, int pageSize);

    }

    public interface IQueryWrapper : IQueryWrapper<string, QueryWrapper>
    {
        IQueryWrapper Select(params string[] filedName);

        IQueryWrapper Paged(int pageIndex, int pageSize);
    }

}
