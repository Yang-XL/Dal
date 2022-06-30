using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yxl.Dal.Aggregate;
using Yxl.Dal.Attributes;
using Yxl.Dapper.Extensions.Attributes;
namespace Yxl.Dal.Entity.DapperTest
{
    /// <summary>
    /// 会员表
    /// CreateAt:2022-06-28 05:18:41
    /// UpdateAt:
    /// </summary>
	[DB("Test"),Table("member")]
    public class MemberEntity : IEntity
    {
		
		/// <summary>
		/// 
		/// </summary>		
		[Column("create_at")]
		public DateTime? CreateAt { get;  set ;  }
		
		/// <summary>
		/// 主键
		/// </summary>		
		[Column("id"),Key,Ignore(Dapper.Extensions.Enum.IgnoreEnum.Insert)]
		public long Id { get;  set ;  }
		
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
