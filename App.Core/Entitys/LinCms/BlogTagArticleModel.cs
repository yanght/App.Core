using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "blog_tag_article")]
	public partial class BlogTagArticleModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true)]
		public Guid Id { get; set; }

		[JsonProperty, Column(Name = "article_id")]
		public Guid ArticleId { get; set; }

		[JsonProperty, Column(Name = "tag_id")]
		public Guid TagId { get; set; }

	}

}
