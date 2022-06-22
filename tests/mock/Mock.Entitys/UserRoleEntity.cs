using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions.Attributes;

namespace Mock.Entitys
{
    [Table("user_role")]
    public class UserRoleEntity : IEntity
    {
        [Key, Column("id"),]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("role_id")]
        public Guid RoleId { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime? Created { get; set; }

        [Column("update_at"), UpdatedAt]
        public DateTime? Updated { get; set; }


    }
}
