﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Options;
using Yxl.Dapper.Extensions.SqlDialect;

namespace Yxl.Dal.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class DbContext : IDbContext
    {
        public ISqlDialect SqlDialect { get; }
        /// <summary>
        ///     DB Connection for internal use
        /// </summary>
        protected readonly DbConnection InnerConnection;

        public string DbName { get; }

        public DbContext(DbOptions dbOptions)
        {
            SqlDialect = dbOptions.SqlDialect;
            DbName = dbOptions.Name;
            InnerConnection = dbOptions.CreateDbConnection();
        }

        public DbTransaction BeginTransaction()
        {
            return InnerConnection.BeginTransaction();
        }

        public async Task<DbTransaction> BeginTransactionAsync()
        {
            return await InnerConnection.BeginTransactionAsync();
        }

        public async Task<DbConnection> OpenConnectionAsync()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                await InnerConnection.OpenAsync();
            return InnerConnection;
        }

        public DbConnection OpenConnection()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                InnerConnection.Open();
            return InnerConnection;
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

}
