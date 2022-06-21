using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
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
        [ClassInitialize]
        public static async Task Init(TestContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("UnitWorkTest Is Running");
            });
        }

        [TestInitialize]
        public void GetRepository()
        {
            UserRepository = serviceProvider.GetRequiredService<IRespository<UserEntity>>();
        }

        [TestCleanup]
        public void ClearRepository()
        {
            UserRepository = null;
        }


        [TestMethod]
        public void RegistAdd()
        {

            IUnitWork work = UserRepository.CreateUnitWork();
            Assert.IsTrue(work.RegistAdd(new UserEntity()));
        }

        [TestMethod]
        public void RegistDelete()
        {
            IUnitWork work = UserRepository.CreateUnitWork();
            var builder = new SqlDeleteBuilder<UserEntity>();

            Assert.IsTrue(work.RegistDelete(builder));
            Assert.IsTrue(work.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistDeleteById()
        {
            IUnitWork work = UserRepository.CreateUnitWork();
            Assert.IsTrue(work.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistUpdate()
        {

            IUnitWork work = UserRepository.CreateUnitWork();
            var builder = new SqlUpdateBuilder<UserEntity>();
            Assert.IsTrue(work.RegistUpdate(builder));
        }

        [TestMethod]
        public void RegistUpdateByID()
        {
            IUnitWork work = UserRepository.CreateUnitWork();
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
