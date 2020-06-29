using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_settings")]
	public partial class LinSettingsModel {

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
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 键
		/// </summary>
		[JsonProperty, Column(Name = "name", DbType = "varchar(128)")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// 提供者值
		/// </summary>
		[JsonProperty, Column(Name = "provider_key", DbType = "varchar(64)")]
		public string ProviderKey { get; set; } = string.Empty;

		/// <summary>
		/// 提供者
		/// </summary>
		[JsonProperty, Column(Name = "provider_name", DbType = "varchar(64)")]
		public string ProviderName { get; set; } = string.Empty;

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
		/// 值
		/// </summary>
		[JsonProperty, Column(Name = "value", DbType = "varchar(2048)")]
		public string Value { get; set; } = string.Empty;

	}

}
