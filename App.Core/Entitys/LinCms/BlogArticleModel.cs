using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_article")]
	public partial class BlogArticleModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		/// <summary>
		/// 随笔档案   如2019年1月
		/// </summary>
		[JsonProperty, Column(Name = "archive", DbType = "varchar(50)")]
		public string Archive { get; set; } = string.Empty;

		/// <summary>
		/// 随笔类型
		/// </summary>
		[JsonProperty, Column(Name = "article_type")]
		public int ArticleType { get; set; }

		/// <summary>
		/// 系统内置技术频道Id
		/// </summary>
		[JsonProperty, Column(Name = "channel_id")]
		public Guid? ChannelId { get; set; }

		/// <summary>
		/// 文章所在分类专栏Id
		/// </summary>
		[JsonProperty, Column(Name = "classify_id")]
		public Guid? ClassifyId { get; set; }

		/// <summary>
		/// 评论数量
		/// </summary>
		[JsonProperty, Column(Name = "comment_quantity")]
		public int CommentQuantity { get; set; }

		/// <summary>
		/// 正文
		/// </summary>
		[JsonProperty, Column(Name = "content", DbType = "longtext")]
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
		/// 1:MarkDown编辑器  2:富文本编辑器,
		/// </summary>
		[JsonProperty, Column(Name = "editor")]
		public int Editor { get; set; }

		/// <summary>
		/// 摘要
		/// </summary>
		[JsonProperty, Column(Name = "excerpt", DbType = "varchar(400)")]
		public string Excerpt { get; set; } = string.Empty;

		/// <summary>
		/// 是否审核（默认为true),为false是代表拉黑
		/// </summary>
		[JsonProperty, Column(Name = "is_audit")]
		public bool IsAudit { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 是否置顶
		/// </summary>
		[JsonProperty, Column(Name = "is_stickie")]
		public bool IsStickie { get; set; }

		/// <summary>
		/// 关键字
		/// </summary>
		[JsonProperty, Column(Name = "keywords", DbType = "varchar(400)")]
		public string Keywords { get; set; } = string.Empty;

		/// <summary>
		/// 点赞数量
		/// </summary>
		[JsonProperty, Column(Name = "likes_quantity")]
		public int LikesQuantity { get; set; }

		/// <summary>
		/// 预计阅读时长
		/// </summary>
		[JsonProperty, Column(Name = "reading_time")]
		public long ReadingTime { get; set; }

		/// <summary>
		/// 是否推荐
		/// </summary>
		[JsonProperty, Column(Name = "recommend")]
		public bool Recommend { get; set; }

		/// <summary>
		/// 来源
		/// </summary>
		[JsonProperty, Column(Name = "source", DbType = "varchar(400)")]
		public string Source { get; set; } = string.Empty;

		/// <summary>
		/// 状态：1.暂存，2.发布文章。
		/// </summary>
		[JsonProperty, Column(Name = "status")]
		public int Status { get; set; }

		/// <summary>
		/// 列表缩略图封面
		/// </summary>
		[JsonProperty, Column(Name = "thumbnail", DbType = "varchar(400)")]
		public string Thumbnail { get; set; } = string.Empty;

		/// <summary>
		/// 标题
		/// </summary>
		[JsonProperty, Column(Name = "title", DbType = "varchar(200)")]
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

		[JsonProperty, Column(Name = "view_hits")]
		public int ViewHits { get; set; }

		/// <summary>
		/// 字数
		/// </summary>
		[JsonProperty, Column(Name = "word_number")]
		public long WordNumber { get; set; }

	}

}
