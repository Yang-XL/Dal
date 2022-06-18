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
                    foreach (var item in _store)
                    {
                        var sql = item.GetSql(options.SqlDialect);
                        await InnerConnection.ExecuteAsync(sql.Sql, sql.GetDynamicParameters(), tran);
                    }
                    Commited = true;
                    await tran.CommitAsync();
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                }

            }
            return Commited;
        }

        public void RegistAdd<T>(T entity) where T : IEntity
        {
            Regist(new SqlInsertBuilder<T>(entity));
        }

        public void RegistDelete<T>(SqlDeleteBuilder<T> builder) where T : IEntity
        {
            Regist(builder);
        }

        public void RegistDeleteById<T>(T entity) where T : IEntity
        {
            Regist(new SqlDeleteBuilder<T>().DeleteById(entity));
        }

        public void RegistUpdate<T>(SqlUpdateBuilder<T> builder) where T : IEntity
        {
            Regist(builder);
        }

        public void RegistUpdateByID<T>(T entity) where T : IEntity
        {
            Regist(new SqlUpdateBuilder<T>().UpdateById(entity));
        }

        public void Regist(ISqlBuilder sqlBuilder)
        {
            _store.Push(sqlBuilder);
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
        }
    }

    public class UnitWork<T> : UnitWork where T : IEntity
    {
        public UnitWork() : base(DbOptionStore.GetOptions<T>())
        {
        }
    }
}
