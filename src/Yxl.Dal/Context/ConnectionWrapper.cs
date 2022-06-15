using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Yxl.Dal.Common.Util;
using Yxl.Dal.Options;

namespace Yxl.Dal.Context
{
    public interface IConnectionWrapper
    {
        DbConnection GetReadDbConnection();

        DbConnection GetWriteDbConnection();
    }  

    public  class ConnectionWrapper : IConnectionWrapper
    {
        private readonly ReadWriteConnectionOptions readWriteConnection;

        public ConnectionWrapper(ReadWriteConnectionOptions readWriteConnection)
        {

            this.readWriteConnection = readWriteConnection;
        }

        public virtual DbConnection GetReadDbConnection()
        {
            var options = GetConnctionStr(readWriteConnection.ReadDbOptions);
            return CreateConnection(options.ConnectionString);

        }

        public ReadDbOptions GetConnctionStr(IEnumerable<ReadDbOptions> readConnctions)
        {
            ReadDbOptions? best = null;
            int total = 0;

            foreach (ReadDbOptions readConnectionInfo in readConnctions)
            {
                readConnectionInfo.CurrentWeight += readConnectionInfo.EffectiveWeight;
                total += readConnectionInfo.EffectiveWeight;

                if (readConnectionInfo.EffectiveWeight < readConnectionInfo.Weight)
                {
                    readConnectionInfo.EffectiveWeight++;
                }

                if (best == null || readConnectionInfo.CurrentWeight > best.CurrentWeight)
                {
                    best = readConnectionInfo;
                }
            }
            if (best == null)
            {
                return null;
            }

            best.CurrentWeight -= total;
            return best;
        }

        public DbConnection GetWriteDbConnection()
        {
            return CreateConnection(readWriteConnection.WriteDbOptions.ConnectionString);
        }
             

        protected abstract DbConnection CreateConnection(string connectionString);
    }


}
