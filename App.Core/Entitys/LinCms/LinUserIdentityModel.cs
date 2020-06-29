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
	/// 用户身份认证登录表
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_user_identity")]
	public partial class LinUserIdentityModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

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
		/// 凭证，例如 密码,存OpenId、Id，同一IdentityType的OpenId的值是唯一的
		/// </summary>
		[JsonProperty, Column(Name = "credential", DbType = "varchar(50)")]
		public string Credential { get; set; } = string.Empty;

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
		/// 认证者，例如 用户名,手机号，邮件等，
		/// </summary>
		[JsonProperty, Column(Name = "identifier", DbType = "varchar(24)")]
		public string Identifier { get; set; } = string.Empty;

		/// <summary>
		/// 认证类型， Password，GitHub、QQ、WeiXin等
		/// </summary>
		[JsonProperty, Column(Name = "identity_type", DbType = "varchar(20)")]
		public string IdentityType { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

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

	}

}
