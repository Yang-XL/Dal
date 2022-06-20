using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;

namespace Yxl.Dal.Context
{
    /// <summary>
    /// 数据库链接维护
    /// </summary>

    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        DbTransaction BeginTransaction();
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task<DbTransaction> BeginTransactionAsync();
               
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <returns></returns>
        Task<DbConnection> OpenConnectionAsync();
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <returns></returns>
        DbConnection OpenConnection();
    }

    public interface IDbContext<T> : IDbContext where T : IEntity
    {

    }
}
