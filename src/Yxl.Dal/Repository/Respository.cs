using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Context;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.Repository
{
    public class Respository<T> : DbContext<T>, IRespository<T> where T : IEntity
    {
        public T Insert(T model)
        {
            var sqlBuilder = new SqlInsertBuilder<T>(model);
            var sqlInfo = sqlBuilder.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                if (sqlBuilder.IsQuery)
                {
                    var identity = connection.QueryFirstOrDefault<object>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
                    return sqlBuilder.GetModesResutOfIdentity(identity);
                }
                connection.Execute(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
            return model;
        }

        public async Task<T> InsertAsync(T model)
        {
            var sqlBuilder = new SqlInsertBuilder<T>(model);
            var sqlInfo = sqlBuilder.GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                if (sqlBuilder.IsQuery)
                {
                    var identity = await connection.QueryFirstOrDefaultAsync<object>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
                    return sqlBuilder.GetModesResutOfIdentity(identity);
                }
                await connection.ExecuteAsync(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
            return model;
        }

        public int UpdateById(T model)
        {
            var sqlInfo = new SqlUpdateBuilder<T>().UpdateById(model).GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                return connection.Execute(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public async Task<int> UpdateByIdAsync(T model)
        {
            var sqlInfo = new SqlUpdateBuilder<T>().UpdateById(model).GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.ExecuteAsync(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public int Update(Action<SqlUpdateBuilder<T>> update)
        {
            var builder = new SqlUpdateBuilder<T>();
            update(builder);
            return Update(builder);
        }

        public async Task<int> UpdateAsync(Action<SqlUpdateBuilder<T>> update)
        {
            var builder = new SqlUpdateBuilder<T>();
            update(builder);
            return await UpdateAsync(builder);
        }

        public int Delete(Action<SqlWhereBuilder<T>> where)
        {
            var builder = new SqlDeleteBuilder<T>();
            builder.Where(where);
            return Delete(builder);
        }

        public async Task<int> DeleteAsync(Action<SqlWhereBuilder<T>> where)
        {
            var builder = new SqlDeleteBuilder<T>();
            builder.Where(where);
            return await DeleteAsync(builder);
        }



        public int Delete(SqlDeleteBuilder<T> where)
        {
            var sqlInfo = where.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                return connection.Execute(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }
    


        public async Task<int> DeleteAsync(SqlDeleteBuilder<T> where)
        {
            var sqlInfo = where.GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.ExecuteAsync(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public int DeleteById(object id)
        {
            var builder = new SqlDeleteBuilder<T>();
            builder.DeleteById(id);
            return Delete(builder);
        }

        public async Task<int> DeleteByIdAsync(object id)
        {
            var builder = new SqlDeleteBuilder<T>();
            builder.DeleteById(id);
            return await DeleteAsync(builder);
        }

        public T GetById(object id)
        {
            var builder = new SqlQueryBuilder<T>();
            var sqlInfo = builder.QueryById(_sqlDialect, id);
            using (var connection = OpenConnection())
            {
                return connection.QueryFirstOrDefault<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var builder = new SqlQueryBuilder<T>();
            var sqlInfo = builder.QueryById(_sqlDialect, id);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public async Task<IEnumerable<T>> QueryWhereAsync(Action<SqlWhereBuilder<T>> where)
        {
            var builder = new SqlQueryBuilder<T>().Where(where);
            return await QueryAsync(builder);
        }

        public IEnumerable<T> QueryWhere(Action<SqlWhereBuilder<T>> where)
        {
            var builder = new SqlQueryBuilder<T>().Where(where);
            return Query(builder);
        }

        public async Task<IEnumerable<T>> QueryAsync(Action<SqlQueryBuilder<T>> query)
        {
            var builder = new SqlQueryBuilder<T>();
            query(builder);
            return await QueryAsync(builder);
        }

        public IEnumerable<T> Query(Action<SqlQueryBuilder<T>> query)
        {
            var builder = new SqlQueryBuilder<T>();
            query(builder);
            return Query(builder);
        }


        public async Task<IEnumerable<T>> QueryWhereAsync(SqlWhereBuilder<T> where)
        {
            var sqlInfo = where.GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.QueryAsync<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public IEnumerable<T> QueryWhere(SqlWhereBuilder<T> where)
        {
            var sqlInfo = where.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                return connection.Query<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public async Task<IEnumerable<T>> QueryAsync(SqlQueryBuilder<T> query)
        {

            var sqlInfo = query.GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.QueryAsync<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public IEnumerable<T> Query(SqlQueryBuilder<T> query)
        {
            var sqlInfo = query.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                return connection.Query<T>(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }


        public int Update(SqlUpdateBuilder<T> update)
        {
            var sqlInfo = update.GetSql(_sqlDialect);
            using (var connection = OpenConnection())
            {
                return connection.Execute(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }

        public async Task<int> UpdateAsync(SqlUpdateBuilder<T> update)
        {
            var sqlInfo = update.GetSql(_sqlDialect);
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.ExecuteAsync(sqlInfo.Sql.ToString(), sqlInfo.GetDynamicParameters());
            }
        }
    }
}
