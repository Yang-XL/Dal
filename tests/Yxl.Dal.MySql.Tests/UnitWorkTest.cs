using Microsoft.Extensions.DependencyInjection;
using Mock.Entitys.DapperTest;
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

        protected IUnitWork<MemberEntity> unitWork;
        [TestInitialize]
        public void GetRepository()
        {
            unitWork = serviceProvider.GetRequiredService<IUnitWork<MemberEntity>>();
        }

        [TestCleanup]
        public void ClearRepository()
        {
            unitWork.Dispose();
        }


        [TestMethod]
        public void RegistAdd()
        {
            Assert.IsTrue(unitWork.RegistAdd(new UserEntity()));
        }

        [TestMethod]
        public void RegistDelete()
        {
            var builder = new SqlDeleteBuilder<UserEntity>();

            Assert.IsTrue(unitWork.RegistDelete(builder));
            Assert.IsTrue(unitWork.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistDeleteById()
        {
            Assert.IsTrue(unitWork.RegistDeleteById<UserEntity>(0));
        }

        [TestMethod]
        public void RegistUpdate()
        {
            var builder = new SqlUpdateBuilder<UserEntity>();
            Assert.IsTrue(unitWork.RegistUpdate(builder));
        }

        [TestMethod]
        public void RegistUpdateByID()
        {
            Assert.IsTrue(unitWork.RegistUpdateByID(new UserEntity()));
        }

        [TestMethod]
        public void Commit()
        {

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };

            var role = new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "administrator"
            };

            var userRole = new UserRoleEntity()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = role.Id,
            };

            unitWork.RegistAdd(user);
            unitWork.RegistAdd(role);
            unitWork.RegistAdd(userRole);
            var result = unitWork.Commit();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CommitAsync()
        {
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };

            var role = new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "administrator"
            };

            var userRole = new UserRoleEntity()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = role.Id,
            };

            unitWork.RegistAdd(user);
            unitWork.RegistAdd(role);
            unitWork.RegistAdd(userRole);
            var result = await unitWork.CommitAsync();
            Assert.IsTrue(result);
        }
    }
}
