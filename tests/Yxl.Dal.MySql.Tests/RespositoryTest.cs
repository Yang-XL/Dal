using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Yxl.Dal.MySql.Tests.Mock.Entity;
using Yxl.Dal.Repository;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class RespositoryTest : BaseTest
    {
       
        [ClassInitialize]
        public static async Task Init(TestContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("RespositoryTest Is running");
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
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            await UserRepository.InsertAsync(entity);

            var result = await UserRepository.GetByIdAsync(entity.Id);

            Assert.IsNull(result.Updated);
            Assert.IsNotNull(result.Created);

            entity.Password = "456";
            await UserRepository.UpdateByIdAsync(entity);
            result = await UserRepository.GetByIdAsync(entity.Id);
            Assert.AreEqual(entity.Password, result.Password);

            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Created);
        }

        [TestMethod]
        public void UpdateById()
        {
            var entity = new UserEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            UserRepository.Insert(entity);
            var result = UserRepository.GetById(entity.Id);

            Assert.IsNull(result.Updated);
            Assert.IsNotNull(result.Created);

            entity.Password = "456";
            UserRepository.UpdateById(entity);
            result = UserRepository.GetById(entity.Id);
            Assert.AreEqual(entity.Password, result.Password);

            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Created);
        }

        [TestMethod]
        public void Delete()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);
            var result = UserRepository.Delete(where => where.Eq(b => b.Name, "administrator"));
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);
            var result = await UserRepository.DeleteAsync(where => where.Eq(b => b.Name, "administrator"));
            Assert.IsTrue(result > 0);
        }


        [TestMethod]
        public void DeleteById()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);
            var result = UserRepository.DeleteById(entity.Id);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task DeleteByIdAsync()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);
            var result = await UserRepository.DeleteByIdAsync(entity.Id);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetByIdAsync()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);
            var result = await UserRepository.GetByIdAsync(entity.Id);
            Assert.AreEqual(result.Id, entity.Id);
        }
        [TestMethod]
        public void GetById()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);
            var result = UserRepository.GetById(entity.Id);
            Assert.AreEqual(result.Id, entity.Id);
        }

        [TestMethod]
        public async Task QueryWhereAsync()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);
            var result = await UserRepository.QueryWhereAsync(w => w.Eq(b => b.Name, "administrator"));
            Assert.IsTrue(result.Any());
        }
        [TestMethod]
        public void QueryWhere()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);
            var result = UserRepository.QueryWhere(w => w.Eq(b => b.Name, "administrator"));
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task QueryAsync()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);

            var query = new SqlQueryBuilder<UserEntity>();

            var result = await UserRepository.QueryAsync(w => w.Select(a => new { a.Id, a.Password }).Where(a => a.Eq(b => b.Name, "administrator")));
            Assert.IsTrue(result.Any());
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.FirstOrDefault()?.LoginName));
        }
        [TestMethod]
        public void Query()
        {
            var entity = new UserEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);

            var query = new SqlQueryBuilder<UserEntity>();

            var result = UserRepository.Query(w => w.Select(a => new { a.Id, a.Password }).Where(a => a.Eq(b => b.Name, "administrator")));
            Assert.IsTrue(result.Any());
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.FirstOrDefault()?.LoginName));
        }


    }
}
