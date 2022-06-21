using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Context;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.Repository
{
    public interface IRespository<T> : IDbContext where T : IEntity
    {
        IUnitWork CreateUnitWork();

        T Insert(T model);

        Task<T> InsertAsync(T model);

        int UpdateById(T model);
        Task<int> UpdateByIdAsync(T model);

        int Update(Action<SqlUpdateBuilder<T>> update);

        Task<int> UpdateAsync(Action<SqlUpdateBuilder<T>> update);

        int Delete(Action<SqlWhereBuilder<T>> where);

        Task<int> DeleteAsync(Action<SqlWhereBuilder<T>> where);

        int DeleteById(object id);

        Task<int> DeleteByIdAsync(object id);

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        Task<IEnumerable<T>> QueryWhereAsync(Action<SqlWhereBuilder<T>> where);

        IEnumerable<T> QueryWhere(Action<SqlWhereBuilder<T>> where);

        Task<IEnumerable<T>> QueryAsync(Action<SqlQueryBuilder<T>> query);

        IEnumerable<T> Query(Action<SqlQueryBuilder<T>> query);
    }
}
