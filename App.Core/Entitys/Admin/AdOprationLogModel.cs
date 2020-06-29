using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.Admin {

	/// <summary>
	/// 操作日志
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_opration_log")]
	public partial class AdOprationLogModel {

		/// <summary>
		/// 编号
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 接口名称
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string ApiLabel { get; set; } = string.Empty;

		/// <summary>
		/// 接口提交方法
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string ApiMethod { get; set; } = string.Empty;

		/// <summary>
		/// 接口地址
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string ApiPath { get; set; } = string.Empty;

		/// <summary>
		/// 浏览器
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string Browser { get; set; } = string.Empty;

		/// <summary>
		/// 浏览器信息
		/// </summary>
		[JsonProperty, Column(DbType = "text")]
		public string BrowserInfo { get; set; } = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty]
		public DateTime? CreatedTime { get; set; }

		/// <summary>
		/// 创建者Id
		/// </summary>
		[JsonProperty]
		public long? CreatedUserId { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string CreatedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 设备
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string Device { get; set; } = string.Empty;

		/// <summary>
		/// 耗时（毫秒）
		/// </summary>
		[JsonProperty]
		public long ElapsedMilliseconds { get; set; }

		/// <summary>
		/// IP
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string IP { get; set; } = string.Empty;

		/// <summary>
		/// 操作消息
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Msg { get; set; } = string.Empty;

		/// <summary>
		/// 昵称
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(60)")]
		public string NickName { get; set; } = string.Empty;

		/// <summary>
		/// 操作系统
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string Os { get; set; } = string.Empty;

		/// <summary>
		/// 操作参数
		/// </summary>
		[JsonProperty, Column(DbType = "text")]
		public string Params { get; set; } = string.Empty;

		/// <summary>
		/// 操作结果
		/// </summary>
		[JsonProperty, Column(DbType = "text")]
		public string Result { get; set; } = string.Empty;

		/// <summary>
		/// 操作状态
		/// </summary>
		[JsonProperty]
		public bool Status { get; set; }

	}

}
