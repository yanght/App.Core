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
	/// 接口管理
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_api")]
	public partial class AdApiModel {

		/// <summary>
		/// 编号
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

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
		/// 说明
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// 启用
		/// </summary>
		[JsonProperty]
		public bool Enabled { get; set; }

		/// <summary>
		/// 接口提交方法
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string HttpMethods { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 接口名称
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Label { get; set; } = string.Empty;

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty]
		public DateTime? ModifiedTime { get; set; }

		/// <summary>
		/// 修改者Id
		/// </summary>
		[JsonProperty]
		public long? ModifiedUserId { get; set; }

		/// <summary>
		/// 修改者
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string ModifiedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 接口命名
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// 所属模块
		/// </summary>
		[JsonProperty]
		public long ParentId { get; set; }

		/// <summary>
		/// 接口地址
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Path { get; set; } = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty]
		public int Sort { get; set; }

		/// <summary>
		/// 版本
		/// </summary>
		[JsonProperty]
		public long Version { get; set; }

	}

}
