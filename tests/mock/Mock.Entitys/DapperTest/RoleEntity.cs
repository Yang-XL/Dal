﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions.Attributes;

namespace Mock.Entitys.DapperTest
{
    [Table("role")]
    public class RoleEntity : UserDbEntity
    {
        [Key, Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime? Created { get; set; }

        [Column("update_at"), UpdatedAt]
        public DateTime? Updated { get; set; }
    }
}
