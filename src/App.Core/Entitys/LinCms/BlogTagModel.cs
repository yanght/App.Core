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
	/// 标签
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_tag")]
	public partial class BlogTagModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		/// <summary>
		/// 别名
		/// </summary>
		[JsonProperty, Column(Name = "alias", DbType = "varchar(200)")]
		public string Alias { get; set; } = string.Empty;

		/// <summary>
		/// 随笔数量
		/// </summary>
		[JsonProperty, Column(Name = "article_count")]
		public int ArticleCount { get; set; }

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
		/// 标签备注情况
		/// </summary>
		[JsonProperty, Column(Name = "remark")]
		public string Remark { get; set; } = string.Empty;

		/// <summary>
		/// 标签状态，true:正常，false：拉黑
		/// </summary>
		[JsonProperty, Column(Name = "status")]
		public bool Status { get; set; }

		/// <summary>
		/// 关注数量
		/// </summary>
		[JsonProperty, Column(Name = "subscribers_count")]
		public int SubscribersCount { get; set; }

		/// <summary>
		/// 标签名
		/// </summary>
		[JsonProperty, Column(Name = "tag_name", DbType = "varchar(50)")]
		public string TagName { get; set; } = string.Empty;

		/// <summary>
		/// 标签封面图
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

		/// <summary>
		/// 浏览次数
		/// </summary>
		[JsonProperty, Column(Name = "view_hits")]
		public int ViewHits { get; set; }

	}

}
