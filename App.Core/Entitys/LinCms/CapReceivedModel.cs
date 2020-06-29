using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "cap.received")]
	public partial class CapReceivedModel {

		[JsonProperty, Column(IsPrimary = true)]
		public long Id { get; set; }

		[JsonProperty, Column(DbType = "datetime")]
		public DateTime Added { get; set; }

		[JsonProperty, Column(DbType = "longtext")]
		public string Content { get; set; } = string.Empty;

		[JsonProperty, Column(DbType = "datetime")]
		public DateTime? ExpiresAt { get; set; }

		[JsonProperty, Column(DbType = "varchar(200)")]
		public string Group { get; set; } = string.Empty;

		[JsonProperty, Column(DbType = "varchar(400)")]
		public string Name { get; set; } = string.Empty;

		[JsonProperty]
		public int? Retries { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string StatusName { get; set; } = string.Empty;

		[JsonProperty, Column(DbType = "varchar(20)")]
		public string Version { get; set; } = string.Empty;

	}

}
