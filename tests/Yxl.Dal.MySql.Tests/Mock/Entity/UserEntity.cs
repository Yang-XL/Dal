using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yxl.Dal.MySql.Tests.Mock.Entity
{
    [Table("user")]
    public class UserEntity
    {
        [Key, Dapper.Extensions.Attributes.Ignore(Dapper.Extensions.Enum.IgnoreEnum.Insert)]
        public long Id { get; set; }

        [Column("login_name")]
        public string LoginName { get; set; }

        [Column("pwd")]
        public string Password { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
