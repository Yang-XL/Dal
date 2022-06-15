using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Yxl.Dal.Context
{
    /// <summary>
    /// 提供读写 分离链接维护
    /// </summary>

    public interface IDbContext
    {
        IDbTransaction BeginTransaction();

        Task<IDbTransaction> BeginTransactionAsync();

        Task<IDbConnection> OpenConnectionAsync();

        IDbConnection OpenConnection();
    }



}
