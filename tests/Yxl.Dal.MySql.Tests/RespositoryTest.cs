using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dal.MySql.Tests.Mock.Entity;
using Yxl.Dal.Repository;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class RespositoryTest : BaseTest
    {
        protected readonly IRespository<UserEntity> UserRepository;
        public RespositoryTest()
        {
            UserRepository = serviceProvider.GetRequiredService<IRespository<UserEntity>>();
        }
        [TestMethod]
        public async Task InsertAsync()
        {
            var entity = new UserEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);
            Assert.IsTrue(entity.Id > 0);
        }
        [TestMethod]
        public void Insert()
        {
            var entity = new UserEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);
            Assert.IsTrue(entity.Id > 0);
        }

        [TestMethod]
        public async Task UpdateByIdAsync()
        {
            var entity = new UserEntity()
            {
                Id = 1,
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            await UserRepository.UpdateByIdAsync(entity);

            var result = await UserRepository.GetByIdAsync(1);
            Assert.AreEqual(entity.Password, result.Password);
        }
        [TestMethod]
        public void UpdateById()
        {
            var entity = new UserEntity()
            {
                Id = 2,
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            UserRepository.UpdateById(entity);

            var result = UserRepository.GetById(2);
            Assert.AreEqual(entity.Password, result.Password);
        }
    }
}
