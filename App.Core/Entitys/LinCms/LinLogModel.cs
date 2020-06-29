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
	/// 日志表
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_log")]
	public partial class LinLogModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 访问哪个权限
		/// </summary>
		[JsonProperty, Column(Name = "authority", DbType = "varchar(100)")]
		public string Authority { get; set; } = string.Empty;

		/// <summary>
		/// 日志信息
		/// </summary>
		[JsonProperty, Column(Name = "message", DbType = "varchar(450)")]
		public string Message { get; set; } = string.Empty;

		/// <summary>
		/// 请求方法
		/// </summary>
		[JsonProperty, Column(Name = "method", DbType = "varchar(20)")]
		public string Method { get; set; } = string.Empty;

		[JsonProperty, Column(Name = "other_message", DbType = "longtext")]
		public string OtherMessage { get; set; } = string.Empty;

		/// <summary>
		/// 请求路径
		/// </summary>
		[JsonProperty, Column(Name = "path", DbType = "varchar(100)")]
		public string Path { get; set; } = string.Empty;

		/// <summary>
		/// 请求的http返回码
		/// </summary>
		[JsonProperty, Column(Name = "status_code")]
		public int? StatusCode { get; set; }

		/// <summary>
		/// 日志创建时间
		/// </summary>
		[JsonProperty, Column(Name = "time", DbType = "datetime")]
		public DateTime? Time { get; set; }

		/// <summary>
		/// 用户id
		/// </summary>
		[JsonProperty, Column(Name = "user_id")]
		public long UserId { get; set; }

		/// <summary>
		/// 用户当时的昵称
		/// </summary>
		[JsonProperty, Column(Name = "user_name", DbType = "varchar(24)")]
		public string UserName { get; set; } = string.Empty;

	}

}
