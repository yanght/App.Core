using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_group")]
	public partial class LinGroupModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 权限组描述
		/// </summary>
		[JsonProperty, Column(Name = "info", DbType = "varchar(500)")]
		public string Info { get; set; } = string.Empty;

		/// <summary>
		/// 是否是静态分组
		/// </summary>
		[JsonProperty, Column(Name = "is_static")]
		public bool IsStatic { get; set; }

		/// <summary>
		/// 权限组名称
		/// </summary>
		[JsonProperty, Column(Name = "name", DbType = "varchar(60)")]
		public string Name { get; set; } = string.Empty;

	}

}
