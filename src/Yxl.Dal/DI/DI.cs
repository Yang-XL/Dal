

using Yxl.Dapper.Extensions.Dapper;

namespace Yxl.Dal.DI
{
    public static class YxlDal
    {
        public static void UseDal()
        {
            DapperExtenstion.UseDapper();
        }
    }

}
