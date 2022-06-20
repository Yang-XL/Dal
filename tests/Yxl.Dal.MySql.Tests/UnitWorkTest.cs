using Microsoft.Extensions.DependencyInjection;
using Yxl.Dal.MySql.Tests.Mock.Entity;
using Yxl.Dal.Repository;
using Yxl.Dal.UnitWork;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class UnitWorkTest : BaseTest
    {
        protected readonly IRespository<UserEntity> UserRepository;
        public UnitWorkTest()
        {
            UserRepository = serviceProvider.GetRequiredService<IRespository<UserEntity>>();
        }

        [TestMethod]
        public void RegistAdd()
        {
            
            IUnitWork work = new UnitWork<UserEntity>();
            Assert.IsTrue(work.RegistAdd(new UserEntity()));
        }

        [TestMethod]
        public void RegistDelete()
        {
            IUnitWork work = new UnitWork<UserEntity>();
            var builder = new SqlDeleteBuilder<UserEntity>();

            Assert.IsTrue(work.RegistDelete(builder));
            Assert.IsTrue(work.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistDeleteById()
        {
            IUnitWork work = new UnitWork<UserEntity>();
            Assert.IsTrue(work.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistUpdate()
        {

            IUnitWork work = new UnitWork<UserEntity>();
            var builder = new SqlUpdateBuilder<UserEntity>();
            Assert.IsTrue(work.RegistUpdate(builder));
        }

        [TestMethod]
        public void RegistUpdateByID()
        {
            IUnitWork work = new UnitWork<UserEntity>();
            Assert.IsTrue(work.RegistUpdateByID(new UserEntity()));
        }

        [TestMethod]
        public void Commit()
        {

        }

        [TestMethod]
        public async Task CommitAsync()
        {

            await Task.FromResult(1);
        }
    }
}
