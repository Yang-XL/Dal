﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dapper.Extensions.Attributes;

namespace Mock.Entitys.DapperTest
{
    [Table("user")]
    public class UserEntity : UserDbEntity
    {
        [Key, Column("id")]
        public Guid Id { get; set; }

        [Column("login_name")]
        public string LoginName { get; set; }

        [Column("pwd")]
        public string Password { get; set; }
        [Column("age"), Ignore]
        public int Age { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime? Created { get; set; }

        [Column("update_at"), UpdatedAt]
        public DateTime? Updated { get; set; }
    }
}
