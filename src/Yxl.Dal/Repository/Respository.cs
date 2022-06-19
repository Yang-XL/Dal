using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Common.Util;
using Yxl.Dal.Context;
using Yxl.Dal.Options;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.Repository
{
    public class Respository<T> : DbContext<T>, IRespository<T> where T : IEntity
    {

        private IUnitWork UnitWork { get; }

        public Respository()
        {
            UnitWork = new UnitWork<T>();
        }

        public T Insert(T model)
        {
            var sqlBuilder = new SqlInsertBuilder<T>(model);
            var sqlInfo = sqlBuilder.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                if (sqlBuilder.IsQuery)
                {
                    var identity = connection.Query<object>(sqlInfo.Sql, sqlInfo.GetDynamicParameters());
                    return sqlBuilder.GetModesResutOfIdentity(identity);
                }
                connection.Execute(sqlInfo.Sql, sqlInfo.GetDynamicParameters());
            }
            return model;
        }

        public Task<T> InsertAsync(T model)
        {
            throw new NotImplementedException();
        }

        public T Update(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }

        public T Update(Action<SqlUpdateBuilder<T>> update)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(Action<SqlUpdateBuilder<T>> update)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Action<SqlWhereBuilder<T>> where)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Action<SqlWhereBuilder<T>> where)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync(Action<SqlWhereBuilder<T>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Action<SqlWhereBuilder<T>> where)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> QueryAsync(Action<SqlQueryBuilder<T>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Action<SqlQueryBuilder<T>> where)
        {
            throw new NotImplementedException();
        }
    }
}
