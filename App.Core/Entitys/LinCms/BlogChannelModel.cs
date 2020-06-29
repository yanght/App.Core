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
	/// 技术频道，官方分类。标签的分类。
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_channel")]
	public partial class BlogChannelModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		/// <summary>
		/// 编码
		/// </summary>
		[JsonProperty, Column(Name = "channel_code", DbType = "varchar(50)")]
		public string ChannelCode { get; set; } = string.Empty;

		/// <summary>
		/// 技术频道名称
		/// </summary>
		[JsonProperty, Column(Name = "channel_name", DbType = "varchar(50)")]
		public string ChannelName { get; set; } = string.Empty;

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
		/// 备注描述
		/// </summary>
		[JsonProperty, Column(Name = "remark", DbType = "varchar(500)")]
		public string Remark { get; set; } = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty, Column(Name = "sort_code")]
		public int SortCode { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		[JsonProperty, Column(Name = "status")]
		public bool Status { get; set; }

		/// <summary>
		/// 封面图
		/// </summary>
		[JsonProperty, Column(Name = "thumbnail", DbType = "varchar(100)")]
		public string Thumbnail { get; set; } = string.Empty;

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
