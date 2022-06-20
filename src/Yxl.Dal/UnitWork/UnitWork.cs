using Dapper;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.UnitWork
{
    public class UnitWork : IUnitWork, IDisposable
    {
        private readonly ConcurrentStack<ISqlBuilder> _store = new ConcurrentStack<ISqlBuilder>();

        protected readonly DbConnection InnerConnection;
        protected readonly DbOptions options;
        public UnitWork(DbOptions options)
        {
            this.options = options;
            InnerConnection = options.CreateDbConnection();
        }
        public UnitWork() : this(DbOptionStore.GetOptions(string.Empty))
        {
        }

        public bool Commited { get; protected set; }

        protected virtual void OpenConnection()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                InnerConnection.Open();
        }
        protected virtual async Task OpenConnectionAsync()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                await InnerConnection.OpenAsync();
        }

        public bool Commit()
        {
            OpenConnection();
            using (var tran = InnerConnection.BeginTransaction())
            {
                try
                {
                    foreach (var item in _store)
                    {
                        var sql = item.GetSql(options.SqlDialect);
                        InnerConnection.Execute(sql.Sql, sql.GetDynamicParameters(), tran);
                    }
                    Commited = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }

            }
            return Commited;
        }

        public async Task<bool> CommitAsync()
        {
            await OpenConnectionAsync();
            using (var tran = await InnerConnection.BeginTransactionAsync())
            {
                try
                {
                    while (!_store.IsEmpty)
                    {
                        if (_store.TryPop(out var sqlBuilder))
                        {
                            var sql = sqlBuilder.GetSql(options.SqlDialect);
                            await InnerConnection.ExecuteAsync(sql.Sql, sql.GetDynamicParameters(), tran);
                        }
                        else
                        {
                            throw new InvalidOperationException("Unit Work Pop Error");
                        }
                    }
                    Commited = true;
                    await tran.CommitAsync();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                }
                finally
                {
                    _store.Clear();
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
            if (InnerConnection != null)
            {
                if (InnerConnection.State != ConnectionState.Closed)
                {
                    InnerConnection.Close();
                }
                InnerConnection.Dispose();
            }
            _store.Clear();
        }
    }

    public class UnitWork<T> : UnitWork where T : IEntity
    {
        public UnitWork() : base(DbOptionStore.GetOptions<T>())
        {
        }
    }
}
