using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dapper.Extensions.Attributes;

namespace Yxl.Dal.Tests.Mock.Entity
{
    [Table("user_role")]
    public class UserRoleEntity
    {
        [Key]
        private string Id;
        [Column("user_id")]
        private int UserId;

        [Column("role_id")]
        private Guid RoleId;

        [Column("delete_tag"), LogicalDelete]
        public bool Delete { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime CreateAt { get; set; }

        [Column("create_at"), UpdatedAt]
        public DateTime UpdateAt { get; set; }
    }
}
