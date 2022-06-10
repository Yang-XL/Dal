using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Yxl.Dal.Context
{
    public abstract class DbContext : IDbContext, IDisposable, IAsyncDisposable
    {
        private readonly DbConnection _dbConnection;

        public DbContext(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public virtual async Task<IDbConnection> OpenWriteAsync()
        {
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                await _dbConnection.OpenAsync();
            return _dbConnection;
        }


        public virtual IDbConnection OpenWrite()
        {
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                _dbConnection.Open();
            return _dbConnection;
        }

        public virtual async Task<IDbConnection> OpenReadAsync()
        {
            return await OpenWriteAsync();
        }

        public virtual IDbConnection OpenRead()
        {
            return OpenWrite();
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                if (_dbConnection.State != ConnectionState.Closed)
                {
                    _dbConnection.Close();
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbConnection != null)
            {
                if (_dbConnection.State != ConnectionState.Closed)
                {
                    await _dbConnection.CloseAsync();
                }
                await _dbConnection.DisposeAsync();
            }
        }
    }
}
