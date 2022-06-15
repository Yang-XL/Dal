using Yxl.Dapper.Extensions.Metadata;
using Yxl.Dapper.Extensions.Uitls;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Yxl.Dapper.Extensions.Wrapper.Impl
{

    public class QueryWrapper<T> : BaseQueryWrapper<Expression<Func<T, object>>, QueryWrapper<T>>, IQueryWrapper<T>
    {
        public QueryWrapper()
        {
            Table = typeof(T).CreateTable(null);
        }

        public IQueryWrapper<T> Select(params string[] filedName)
        {
            var table = typeof(T).CreateTable(null);
            return Select(filedName?.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new Filed { Name = a, Table = table }).ToArray());
        }


        public IQueryWrapper<T> Select(Expression<Func<T, object>> filedName)
        {
            var members = ExpressionHelper.GetMemberInfo(filedName);
            foreach (var item in members.Where(a => a.MemberType == MemberTypes.Property))
            {
                if (item.MemberType == MemberTypes.Property && item is PropertyInfo p)
                {
                    var filed = p.CreateFiled(typeof(T));
                    if (filed.IgnoreSelect) { continue; }
                    AddFiled(filed);
                }
            }
            return this;
        }

        protected override IFiled GetColumn(Expression<Func<T, object>> column)
        {
            var columnName = ExpressionHelper.GetProperty(column).ToString();
            return new Filed(columnName, Table);
        }


        public IQueryWrapper<T> Paged(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            return this;
        }

       

        protected override Action<QueryWrapper<T>> AppendQuery
        {
            get
            {
                return (query) =>
                 {
                     var logicalColumns = typeof(T).CreateFiles().Where(a => a.LogicalDelete);
                     foreach (var logical in logicalColumns)
                     {
                         query.AppendEq(logical, false);
                     }
                 };
            }

        }
    }

    public class QueryWrapper : BaseQueryWrapper<string, QueryWrapper>, IQueryWrapper
    {
        

        public IQueryWrapper Paged(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            return this;
        }

        public IQueryWrapper Select(params string[] filedName)
        {
            return Select(filedName?.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new Filed { Name = a }).ToArray());
        }
    }
}
