using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Attributes;
using Yxl.Dapper.Extensions.Attributes;
namespace Yxl.Dal.DapperTest.Entity
{
    /// <summary>
    /// 
    /// CreateAt:2022-06-21 07:57:42
    /// UpdateAt:
    /// </summary>
	[DB("Test"),Table("role")]
    public class RoleEntity : IEntity
    {
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("create_at")]
		public DateTime? CreateAt { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("id"),Key]
		public Guid Id { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("name")]
		public string Name { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("update_at")]
		public DateTime? UpdateAt { get;  set ;  }
			
	}
}
