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

        Task<DbConnection> OpenWriteAsync();

        DbConnection OpenWrite();

        Task<DbConnection> OpenReadAsync();

        DbConnection OpenRead();
    }

    public interface IDbContext<T> : IDbContext
    {

    }


}
