using Yxl.Dal.Options;

namespace Yxl.Dal.MySql
{
    public static class DI
    {
        public static void AddMysqlDal(this DbOptions options, string dbName, string connectionString)
        {
            Dal.DI.YxlDal.Register(new MySqlDbOptions(dbName, connectionString));
        }
    }
}
