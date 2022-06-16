using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.UnitWork
{
    public class UnitWork : IUnitWork
    {
        private readonly ConcurrentStack<ISqlBuilder> _store = new ConcurrentStack<ISqlBuilder>();

        private readonly DbOptions options;

        public UnitWork(DbOptions options)
        {
            this.options = options;
        }

        public bool Commited { get; protected set; }


        public Func<DbOptions, DbConnection> OpenConnection { get; set; }

        public bool Commit()
        {
            using (var conneciont = options.CreateDbConnection())
            {
                conneciont.Open();
                using (var tran = conneciont.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in _store)
                        {
                            var sql = item.GetSql(options.SqlDialect);
                            conneciont.Execute(sql.Sql, sql.GetDynamicParameters(), tran);
                        }
                        Commited = true;
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
            using (var conneciont = options.CreateDbConnection())
            {
                await conneciont.OpenAsync();
                using (var tran = conneciont.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in _store)
                        {
                            var sql = item.GetSql(null);
                            //TODO:
                            await conneciont.ExecuteAsync(sql.Sql, sql.GetDynamicParameters(), tran);
                        }
                        await tran.CommitAsync();
                        Commited = true;
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                    }
                }
            }
            return Commited;
        }

        public void RegistAdd<T>(T entity) where T : IEntity
        {

            throw new NotImplementedException();
        }

        public void RegistDelete<T>(SqlDeleteBuilder<T> builder) where T : IEntity
        {
            _store.Push(builder);
        }

        public void RegistDeleteById<T>(T entity) where T : IEntity
        {
            _store.Push(new SqlDeleteBuilder<T>().DeleteById(entity));
        }

        public void RegistUpdate<T>(SqlUpdateBuilder<T> builder) where T : IEntity
        {
            _store.Push(builder);
        }

        public void RegistUpdateByID<T>(T entity) where T : IEntity
        {
            _store.Push(new SqlUpdateBuilder<T>().UpdateById(entity));
        }


    }
}
