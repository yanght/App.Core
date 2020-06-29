using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_notification")]
	public partial class BlogNotificationModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "article_id")]
		public Guid? ArticleId { get; set; }

		[JsonProperty, Column(Name = "comment_id")]
		public Guid? CommentId { get; set; }

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
		/// 默认消息未读
		/// </summary>
		[JsonProperty, Column(Name = "is_read")]
		public bool IsRead { get; set; }

		/// <summary>
		/// 当前登录人Id，消息接收者
		/// </summary>
		[JsonProperty, Column(Name = "notification_resp_user_id")]
		public long NotificationRespUserId { get; set; }

		/// <summary>
		/// 消息类型
		/// </summary>
		[JsonProperty, Column(Name = "notification_type")]
		public BlogNotificationNOTIFICATIONTYPE NotificationType { get; set; }

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
		/// 创建消息者
		/// </summary>
		[JsonProperty, Column(Name = "user_info_id")]
		public long UserInfoId { get; set; }

	}

	public enum BlogNotificationNOTIFICATIONTYPE {
		UserLikeArticle = 1, UserLikeArticleComment, UserCommentOnArticle, UserCommentOnComment, UserLikeUser
	}
}
