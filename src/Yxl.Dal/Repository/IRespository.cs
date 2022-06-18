using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Context;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.Repository
{
    public interface IRespository<T> : IDbContext where T : IEntity
    {
        T Insert(T model);

        Task<T> InsertAsync(T model);



        T Update(T model);
        Task<T> UpdateAsync(T model);

        T Update(Action<SqlUpdateBuilder<T>> update);

        Task<T> UpdateAsync(Action<SqlUpdateBuilder<T>> update);

            


        bool Delete(Action<SqlWhereBuilder<T>> where);

        Task<bool> DeleteAsync(Action<SqlWhereBuilder<T>> where);

        bool DeleteById(object id);

        Task<bool> DeleteByIdAsync(object id);




        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        Task<IEnumerable<T>> QueryAsync(Action<SqlWhereBuilder<T>> where);

        IEnumerable<T> Query(Action<SqlWhereBuilder<T>> where);

        Task<IEnumerable<T>> QueryAsync(Action<SqlQueryBuilder<T>> where);

        IEnumerable<T> Query(Action<SqlQueryBuilder<T>> where);
    }
}
