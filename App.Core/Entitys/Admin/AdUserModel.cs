using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.Admin {

	/// <summary>
	/// 用户
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_user")]
	public partial class AdUserModel {

		/// <summary>
		/// 编号
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 头像
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string Avatar { get; set; } = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

		/// <summary>
		/// 创建者Id
		/// </summary>
		[JsonProperty]
		public long? CreatedUserId { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string CreatedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty]
		public DateTime? ModifiedTime { get; set; }

		/// <summary>
		/// 修改者Id
		/// </summary>
		[JsonProperty]
		public long? ModifiedUserId { get; set; }

		/// <summary>
		/// 修改者
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string ModifiedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 昵称
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(60)")]
		public string NickName { get; set; } = string.Empty;

		/// <summary>
		/// 密码
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(60)")]
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Remark { get; set; } = string.Empty;

		/// <summary>
		/// 状态
		/// </summary>
		[JsonProperty]
		public int Status { get; set; }

		/// <summary>
		/// 账号
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(60)")]
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// 版本
		/// </summary>
		[JsonProperty]
		public long Version { get; set; }

	}

}
