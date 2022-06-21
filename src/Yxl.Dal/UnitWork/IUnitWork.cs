using System;
using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.UnitWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitWork : IDisposable
    {
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        bool RegistAdd<T>(T entity) where T : IEntity;
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        bool RegistDeleteById<T>(object id) where T : IEntity;
        /// <summary>
        /// 注册更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        bool RegistUpdateByID<T>(T entity) where T : IEntity;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        bool RegistDelete<T>(SqlDeleteBuilder<T> builder) where T : IEntity;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        bool RegistUpdate<T>(SqlUpdateBuilder<T> builder) where T : IEntity;

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        bool Commit();
        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();

        /// <summary>
        /// 事务是否提交
        /// </summary>
        bool Commited { get; }

    }

    public interface IUnitWork<T> : IUnitWork { }
}
