using Microsoft.Extensions.DependencyInjection;
using Mock.Entitys;
using Yxl.Dal.Repository;
using Yxl.Dapper.Extensions;

namespace Yxl.Dal.MySql.Tests
{
    [TestClass]
    public class RespositoryTest : BaseTest
    {

        protected IRespository<MemberEntity> UserRepository;

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
            UserRepository = serviceProvider.GetRequiredService<IRespository<MemberEntity>>();
        }
        [TestCleanup]
        public void ClearRepository()
        {
            UserRepository = null;
        }

        [TestMethod]
        public async Task InsertAsync()
        {
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
        public async Task UpdateAsync()
        {
            var entity = new MemberEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            await UserRepository.InsertAsync(entity);

            var result = await UserRepository.GetByIdAsync(entity.Id);

            Assert.IsNull(result.Updated);
            Assert.IsNotNull(result.Created);

            await UserRepository.UpdateAsync((builder) => builder.Set(a => a.Password, "456").Where(w => w.Eq(a => a.Id, entity.Id)));
            result = await UserRepository.GetByIdAsync(entity.Id);
            Assert.AreEqual(result.Password, "456");

            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Created);
        }

        [TestMethod]
        public void Update()
        {
            var entity = new MemberEntity()
            {
                Name = "admin",
                LoginName = "admin",
                Password = "123",
            };
            UserRepository.Insert(entity);

            var result = UserRepository.GetById(entity.Id);

            Assert.IsNull(result.Updated);
            Assert.IsNotNull(result.Created);

            UserRepository.Update((builder) => builder.Set(a => a.Password, "456").Where(w => w.Eq(a => a.Id, entity.Id)));
            result = UserRepository.GetById(entity.Id);
            Assert.AreEqual(result.Password, "456");

            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Created);
        }


        [TestMethod]
        public void Delete()
        {
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
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
            var entity = new MemberEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            await UserRepository.InsertAsync(entity);

            var query = new SqlQueryBuilder<MemberEntity>();

            var result = await UserRepository.QueryAsync(w => w.Select(a => new { a.Id, a.Password }).Where(a => a.Eq(b => b.Name, "administrator")));
            Assert.IsTrue(result.Any());
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.FirstOrDefault()?.LoginName));
        }
        [TestMethod]
        public void Query()
        {
            var entity = new MemberEntity()
            {
                Name = "administrator",
                LoginName = "admin",
                Password = "admin",
            };
            UserRepository.Insert(entity);

            var query = new SqlQueryBuilder<MemberEntity>();

            var result = UserRepository.Query(w => w.Select(a => new { a.Id, a.Password }).Where(a => a.Eq(b => b.Name, "administrator")));
            Assert.IsTrue(result.Any());
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.FirstOrDefault()?.LoginName));
        }


    }
}
