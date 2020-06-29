using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "base_type")]
	public partial class BaseTypeModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

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

		[JsonProperty, Column(Name = "full_name", DbType = "varchar(50)")]
		public string FullName { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		[JsonProperty, Column(Name = "sort_code")]
		public int? SortCode { get; set; }

		[JsonProperty, Column(Name = "type_code", DbType = "varchar(50)")]
		public string TypeCode { get; set; } = string.Empty;

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
