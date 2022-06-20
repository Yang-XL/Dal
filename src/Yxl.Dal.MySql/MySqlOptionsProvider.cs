using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using Yxl.Dal.Options;

namespace Yxl.Dal.MySql
{
    public class MySqlOptionsProvider : OptionsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">当您的项目有多个数据库的时候,请给每隔数据库配置一个别名</param>
        /// <param name="connectionString"></param>
        public void UserMysql(string name, string connectionString)
        {
            base.Config(() => new MySqlConnection(connectionString));
            base.Config(name, connectionString, Dapper.Extensions.Enum.SqlProvider.MYSQL);
        }       

        /// <summary>
        /// 当您的项目只有一个数据的时候使用
        /// </summary>
        /// <param name="connectionString"></param>
        public void UserMysql(string connectionString)
        {
            UserMysql(String.Empty, connectionString);
        }

    }
}
