using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	/// <summary>
	/// 用户信息
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_user")]
	public partial class LinUserModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 当前用户是否为激活状态，非激活状态默认失去用户权限 ; 1 -&gt; 激活 | 2 -&gt; 非激活
		/// </summary>
		[JsonProperty, Column(Name = "active")]
		public int Active { get; set; }

		/// <summary>
		/// 用户默认生成图像，为null、头像url
		/// </summary>
		[JsonProperty, Column(Name = "avatar")]
		public string Avatar { get; set; } = string.Empty;

		/// <summary>
		/// 个人主页
		/// </summary>
		[JsonProperty, Column(Name = "blog_address")]
		public string BlogAddress { get; set; } = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty, Column(Name = "create_time")]
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 创建者ID
		/// </summary>
		[JsonProperty, Column(Name = "create_user_id")]
		public long CreateUserId { get; set; }

		/// <summary>
		/// 删除时间
		/// </summary>
		[JsonProperty, Column(Name = "delete_time")]
		public DateTime? DeleteTime { get; set; }

		/// <summary>
		/// 删除人id
		/// </summary>
		[JsonProperty, Column(Name = "delete_user_id")]
		public long? DeleteUserId { get; set; }

		/// <summary>
		/// 电子邮箱
		/// </summary>
		[JsonProperty, Column(Name = "email", DbType = "varchar(100)")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// 个人介绍
		/// </summary>
		[JsonProperty, Column(Name = "introduction", DbType = "varchar(100)")]
		public string Introduction { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 最后一次登录的时间
		/// </summary>
		[JsonProperty, Column(Name = "last_login_time")]
		public DateTime LastLoginTime { get; set; }

		/// <summary>
		/// 昵称
		/// </summary>
		[JsonProperty, Column(Name = "nickname", DbType = "varchar(24)")]
		public string Nickname { get; set; } = string.Empty;

		/// <summary>
		/// 手机号
		/// </summary>
		[JsonProperty, Column(Name = "phone_number", DbType = "varchar(100)")]
		public string PhoneNumber { get; set; } = string.Empty;

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty, Column(Name = "update_time")]
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// 最后修改人Id
		/// </summary>
		[JsonProperty, Column(Name = "update_user_id")]
		public long? UpdateUserId { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		[JsonProperty, Column(Name = "username", DbType = "varchar(24)")]
		public string Username { get; set; } = string.Empty;

	}

}
