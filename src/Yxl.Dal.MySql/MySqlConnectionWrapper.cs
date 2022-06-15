using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Yxl.Dal.Context;
using Yxl.Dal.Options;

namespace Yxl.Dal.MySql
{
    internal class MySqlConnectionWrapper<T> : ConnectionWrapper<T>
    {
        public MySqlConnectionWrapper(IEnumerable<ReadWriteConnectionOptions> readWriteConnection) : base(readWriteConnection)
        {
        }

        protected override DbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection("");
        }
    }
}
