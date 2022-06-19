using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yxl.Dal.MySql.Tests.Mock.Entity
{
    [Table("user")]
    public class UserEntity
    {
        
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
