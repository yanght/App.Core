using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_user_like")]
	public partial class BlogUserLikeModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "create_time")]
		public DateTime CreateTime { get; set; }

		[JsonProperty, Column(Name = "create_user_id")]
		public long CreateUserId { get; set; }

		/// <summary>
		/// 点赞对象
		/// </summary>
		[JsonProperty, Column(Name = "subject_id")]
		public Guid SubjectId { get; set; }

		/// <summary>
		/// 点赞类型 1 是文章，2 是评论
		/// </summary>
		[JsonProperty, Column(Name = "subject_type")]
		public int SubjectType { get; set; }

	}

}
