using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yxl.Dal.Options;

namespace Yxl.Dal.Context
{
    public abstract class DbContext : IDbContext
    {
        private IConnectionWrapper connectionWrapper;


        protected DbContext(ReadWriteConnectionOptions dbOptions)
        {
            this.connectionWrapper = dbOptions.Wrapper;
        }

        public virtual async Task<IDbConnection> OpenWriteAsync()
        {
            var _dbConnection = connectionWrapper.GetWriteDbConnection();
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                await _dbConnection.OpenAsync();
            return _dbConnection;
        }


        public virtual IDbConnection OpenWrite()
        {
            var _dbConnection = connectionWrapper.GetWriteDbConnection();
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                _dbConnection.Open();
            return _dbConnection;
        }

        public virtual async Task<IDbConnection> OpenReadAsync()
        {
            var _dbConnection = connectionWrapper.GetReadDbConnection();
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public virtual IDbConnection OpenRead()
        {
            var _dbConnection = connectionWrapper.GetReadDbConnection();
            if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                _dbConnection.Open();
            return _dbConnection;
        }

        public IDbTransaction BeginTransaction()
        {
            opre
        }

        public Task<IDbTransaction> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDbConnection> OpenConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public IDbConnection OpenConnection()
        {
            throw new NotImplementedException();
        }
    }
}
