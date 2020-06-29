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
	/// 文章专栏，由普通用户创建
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_classify")]
	public partial class BlogClassifyModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		/// <summary>
		/// 随笔数量
		/// </summary>
		[JsonProperty, Column(Name = "article_count")]
		public int ArticleCount { get; set; }

		/// <summary>
		/// 分类专栏名称
		/// </summary>
		[JsonProperty, Column(Name = "classify_name", DbType = "varchar(50)")]
		public string ClassifyName { get; set; } = string.Empty;

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
		/// 排序
		/// </summary>
		[JsonProperty, Column(Name = "sort_code")]
		public int SortCode { get; set; }

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
