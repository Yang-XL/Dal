using DapperExtensions.Custom.Enum;
using System;

namespace Yxl.Dal.Options
{
    public class DbContextOptions
    {
        public SqlProvider Provider { get; internal set; }

        public ReadWriteConnectionOptions ConnectionOptions { get; set; }

        public DbContextOptions UseMysql(Action<ReadWriteConnectionOptions> action)
        {
            action(ConnectionOptions);
            Provider = SqlProvider.MySQL;
            return this;
        }
    }



}
