using Dapper;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Common.Util;
using Yxl.Dal.Context;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.UnitWork
{
    public class UnitWork : IUnitWork
    {
        private readonly ConcurrentStack<ISqlBuilder> _store = new ConcurrentStack<ISqlBuilder>();

        protected readonly IDbContext dbContext;
        protected readonly DbOptions options;
        public UnitWork(string dbName)
        {
            dbContext = DbContextProvider.GetDbContext(dbName);
        }

        public bool Commited { get; protected set; }


        public bool Commit()
        {
            using (var connection = dbContext.OpenConnection())
            {
                using (var tran = dbContext.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in _store)
                        {
                            var sql = item.GetSql(options.SqlDialect);
                            connection.Execute(sql.Sql.ToString(), sql.GetDynamicParameters(), tran);
                        }
                        tran.Commit();
                        Commited = true;
                        _store.Clear();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                    }

                }
            }
            return Commited;
        }

        public async Task<bool> CommitAsync()
        {
            using (var connection = await dbContext.OpenConnectionAsync())
            {
                using (var tran = await dbContext.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var item in _store)
                        {
                            var sql = item.GetSql(options.SqlDialect);
                            await connection.ExecuteAsync(sql.Sql.ToString(), sql.GetDynamicParameters(), tran);
                        }
                        tran.Commit();
                        Commited = true;
                        _store.Clear();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                    }

                }
            }
            return Commited;
        }

        public bool RegistAdd<T>(T entity) where T : IEntity
        {
            return Regist(new SqlInsertBuilder<T>(entity));
        }

        public bool RegistDelete<T>(SqlDeleteBuilder<T> builder) where T : IEntity
        {
            return Regist(builder);
        }

        public bool RegistDeleteById<T>(object id) where T : IEntity
        {
            return Regist(new SqlDeleteBuilder<T>().DeleteById(id));
        }

        public bool RegistUpdate<T>(SqlUpdateBuilder<T> builder) where T : IEntity
        {
            return Regist(builder);
        }

        public bool RegistUpdateByID<T>(T entity) where T : IEntity
        {
            return Regist(new SqlUpdateBuilder<T>().UpdateById(entity));
        }

        public bool Regist(ISqlBuilder sqlBuilder)
        {
            _store.Push(sqlBuilder);
            return true;
        }

        public void Dispose()
        {
            _store.Clear();
        }
    }

    public class UnitWork<T> : UnitWork, IUnitWork<T> where T : IEntity
    {
        public UnitWork() : base(DbUntil.GetDbName<T>())
        {
        }
    }
}
