using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Attributes;
using Yxl.Dapper.Extensions.Attributes;
namespace Yxl.Dal.Domain.DapperTest.Entity
{
    /// <summary>
    /// 
    /// CreateAt:2022-06-21 07:57:38
    /// UpdateAt:
    /// </summary>
	[DB("Test"),Table("user")]
    public class UserEntity : IEntity
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
		[Column("login_name")]
		public string LoginName { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("name")]
		public string Name { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("pwd")]
		public string Pwd { get;  set ;  }
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("update_at")]
		public DateTime? UpdateAt { get;  set ;  }
			
	}
}
