using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_poem")]
	public partial class LinPoemModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 作者
		/// </summary>
		[JsonProperty, Column(Name = "author", DbType = "varchar(50)")]
		public string Author { get; set; } = string.Empty;

		/// <summary>
		/// 内容，以/来分割每一句，以|来分割宋词的上下片
		/// </summary>
		[JsonProperty, Column(Name = "content", DbType = "text")]
		public string Content { get; set; } = string.Empty;

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
		/// 朝代
		/// </summary>
		[JsonProperty, Column(Name = "dynasty", DbType = "varchar(50)")]
		public string Dynasty { get; set; } = string.Empty;

		/// <summary>
		/// 配图
		/// </summary>
		[JsonProperty, Column(Name = "image")]
		public string Image { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		[JsonProperty, Column(Name = "title", DbType = "varchar(50)")]
		public string Title { get; set; } = string.Empty;

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
