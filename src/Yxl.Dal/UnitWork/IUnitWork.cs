using System.Threading.Tasks;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.UnitWork
{
    public interface IUnitWork
    {
        void RegistAdd<T>(T entity) where T : IEntity;

        void RegistDeleteById<T>(T entity) where T : IEntity;

        void RegistUpdateByID<T>(T entity) where T : IEntity;

        void RegistDelete<T>(SqlDeleteBuilder<T> builder) where T : IEntity;

        void RegistUpdate<T>(SqlUpdateBuilder<T> builder) where T : IEntity;


        bool Commit();

        Task<bool> CommitAsync();

        bool Commited { get; }

    }
}
