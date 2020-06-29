using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "image")]
	public partial class ImageModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		[JsonProperty, Column(Name = "from")]
		public int From { get; set; }

		[JsonProperty, Column(Name = "url")]
		public string Url { get; set; } = string.Empty;

	}

}
