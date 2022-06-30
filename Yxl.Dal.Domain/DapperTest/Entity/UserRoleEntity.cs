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
    /// CreateAt:2022-06-21 07:57:33
    /// UpdateAt:
    /// </summary>
	[DB("Test"),Table("user_role")]
    public class UserRoleEntity : IEntity
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
		[Column("role_id")]
		public Guid RoleId { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("update_at")]
		public DateTime? UpdateAt { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("user_id")]
		public Guid UserId { get;  set ;  }
			
	}
}
