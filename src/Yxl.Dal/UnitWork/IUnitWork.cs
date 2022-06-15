namespace Yxl.Dal.UnitWork
{
    public interface IUnitWork
    {
        void RegistAdd<T>(T entity);

        void RegistDelete<T>(T entity);

        void RegistUpdate<T>(T entity);

        bool Commit();

        bool Committed { get; }

        /// <summary>
        /// 回滚当前的Unit Of Work事务。
        /// </summary>
        void Rollback();
    }
}
