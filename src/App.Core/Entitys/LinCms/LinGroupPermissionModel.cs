using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_group_permission")]
	public partial class LinGroupPermissionModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 分组id
		/// </summary>
		[JsonProperty, Column(Name = "group_id")]
		public long GroupId { get; set; }

		/// <summary>
		/// 权限Id
		/// </summary>
		[JsonProperty, Column(Name = "permission_id")]
		public long PermissionId { get; set; }

	}

}
