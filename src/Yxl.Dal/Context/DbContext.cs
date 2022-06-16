using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dal.Options;

namespace Yxl.Dal.Context
{
    internal class DbContext : IDbContext
    {

        public DbContext(DbOptions dbOptions)
        {

        }
        public DbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<DbTransaction> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAsync()
        {
            throw new NotImplementedException();
        }



        public DbConnection OpenConnection()
        {
            throw new NotImplementedException();
        }

        public Task<DbConnection> OpenConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public void RoolBackAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
