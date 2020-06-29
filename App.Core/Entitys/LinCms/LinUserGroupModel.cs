using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_user_group")]
	public partial class LinUserGroupModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		[JsonProperty, Column(Name = "group_id")]
		public long GroupId { get; set; }

		[JsonProperty, Column(Name = "user_id")]
		public long UserId { get; set; }

	}

}
