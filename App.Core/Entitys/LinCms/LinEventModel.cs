using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_event")]
	public partial class LinEventModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 所属权限组id
		/// </summary>
		[JsonProperty, Column(Name = "group_id")]
		public int GroupId { get; set; }

		/// <summary>
		/// 信息
		/// </summary>
		[JsonProperty, Column(Name = "message_events", DbType = "varchar(250)")]
		public string MessageEvents { get; set; } = string.Empty;

	}

}
