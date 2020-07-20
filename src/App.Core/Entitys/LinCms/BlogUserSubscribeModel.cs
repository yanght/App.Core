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
	/// 用户关注用户
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_user_subscribe")]
	public partial class BlogUserSubscribeModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "create_time")]
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 关注的用户Id
		/// </summary>
		[JsonProperty, Column(Name = "create_user_id")]
		public long CreateUserId { get; set; }

		/// <summary>
		/// 被关注的用户Id
		/// </summary>
		[JsonProperty, Column(Name = "subscribe_user_id")]
		public long SubscribeUserId { get; set; }

	}

}
