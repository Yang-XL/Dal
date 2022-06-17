using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yxl.Dapper.Extensions.Attributes;
using Yxl.Dapper.Extensions.Enum;
using IgnoreAttribute = Yxl.Dapper.Extensions.Attributes.IgnoreAttribute;

namespace Yxl.Dal.Tests.Mock.Entity
{
    [Table("user")]
    public class UserEntity
    {
        /// <summary>
        /// 自动生成的ID 忽略插入
        /// </summary>
        [Key(), Column("id"), Ignore(Ignore = IgnoreEnum.Insert)]
        public int Id { get; set; }

        [Column("delete_tag"), LogicalDelete]
        public bool Delete { get; set; }

        [Column("create_at"), CreateAt]
        public DateTime CreateAt { get; set; }

        [Column("create_at"), UpdatedAt]
        public DateTime UpdateAt { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Ignore]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            set
            {
                var arr = value.Split(" ");
                if (arr.Length > 1)
                {
                    FirstName = arr[0];
                }
                if (arr.Length > 2)
                {
                    LastName = arr[1];
                }
            }
        }
    }
}
