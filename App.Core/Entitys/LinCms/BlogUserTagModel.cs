using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_user_tag")]
	public partial class BlogUserTagModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "create_time")]
		public DateTime CreateTime { get; set; }

		[JsonProperty, Column(Name = "create_user_id")]
		public long CreateUserId { get; set; }

		[JsonProperty, Column(Name = "tag_id")]
		public Guid TagId { get; set; }

	}

}
