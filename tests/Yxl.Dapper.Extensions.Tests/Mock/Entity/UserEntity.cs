using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dapper.Extensions.Attributes;
using IgnoreAttribute = Yxl.Dapper.Extensions.Attributes.IgnoreAttribute;

namespace Yxl.Dapper.Extensions.Tests.Mock.Entity
{
    [Table("`user`")]
    public class UserEntity
    {
        [Key(), Column("id")]
        public string Id { get; set; }

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
