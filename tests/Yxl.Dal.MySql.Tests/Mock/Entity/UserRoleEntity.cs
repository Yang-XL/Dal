﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions.Attributes;

namespace Yxl.Dal.MySql.Tests.Mock.Entity
{
    [Table("user_role")]
    public class UserRoleEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public Guid RoleId { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime Created { get; set; }

        [Column("update_at"), UpdatedAt]
        public DateTime Updated { get; set; }


    }
}
