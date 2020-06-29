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
	/// 用户评论信息
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_comment")]
	public partial class BlogCommentModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "childs_count")]
		public int ChildsCount { get; set; }

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
		/// 是否已审核
		/// </summary>
		[JsonProperty, Column(Name = "is_audit")]
		public bool? IsAudit { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 点赞量
		/// </summary>
		[JsonProperty, Column(Name = "likes_quantity")]
		public int LikesQuantity { get; set; }

		/// <summary>
		/// 回复评论Id
		/// </summary>
		[JsonProperty, Column(Name = "resp_id")]
		public Guid? RespId { get; set; }

		/// <summary>
		/// 被回复的用户Id
		/// </summary>
		[JsonProperty, Column(Name = "resp_user_id")]
		public long? RespUserId { get; set; }

		/// <summary>
		/// 根回复id
		/// </summary>
		[JsonProperty, Column(Name = "root_comment_id")]
		public Guid? RootCommentId { get; set; }

		/// <summary>
		/// 关联随笔id
		/// </summary>
		[JsonProperty, Column(Name = "subject_id")]
		public Guid? SubjectId { get; set; }

		/// <summary>
		/// 评论的对象 1 是随笔，其他为以后扩展
		/// </summary>
		[JsonProperty, Column(Name = "subject_type")]
		public int SubjectType { get; set; }

		/// <summary>
		/// 回复的文本内容
		/// </summary>
		[JsonProperty, Column(Name = "text", DbType = "varchar(500)")]
		public string Text { get; set; } = string.Empty;

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
