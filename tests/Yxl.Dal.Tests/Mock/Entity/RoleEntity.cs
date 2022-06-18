using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dapper.Extensions.Attributes;

namespace Yxl.Dal.Tests.Mock.Entity
{
    [Table("role")]
    public class RoleEntity
    {
        [Column("id"), Key()]
        private Guid Id { get; set; }

        [Column("name")]
        private string Name { get; set; }

        [Column("delete_tag"), LogicalDelete]
        public bool Delete { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime CreateAt { get; set; }

        [Column("create_at"), UpdatedAt]
        public DateTime UpdateAt { get; set; }
    }
}
