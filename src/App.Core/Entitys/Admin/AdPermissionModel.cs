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
	/// 权限
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "ad_permission")]
	public partial class AdPermissionModel {

		/// <summary>
		/// 编号
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 接口
		/// </summary>
		[JsonProperty]
		public long? ApiId { get; set; }

		/// <summary>
		/// 可关闭
		/// </summary>
		[JsonProperty]
		public bool? Closable { get; set; }

		/// <summary>
		/// 权限编码
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(550)")]
		public string Code { get; set; } = string.Empty;

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
		/// 描述
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// 启用
		/// </summary>
		[JsonProperty]
		public bool Enabled { get; set; }

		/// <summary>
		/// 链接外显
		/// </summary>
		[JsonProperty]
		public bool? External { get; set; }

		/// <summary>
		/// 隐藏
		/// </summary>
		[JsonProperty]
		public bool Hidden { get; set; }

		/// <summary>
		/// 图标
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(100)")]
		public string Icon { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 权限名称
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
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
		/// 打开新窗口
		/// </summary>
		[JsonProperty]
		public bool? NewWindow { get; set; }

		/// <summary>
		/// 打开组
		/// </summary>
		[JsonProperty]
		public bool? Opened { get; set; }

		/// <summary>
		/// 父级节点
		/// </summary>
		[JsonProperty]
		public long ParentId { get; set; }

		/// <summary>
		/// 菜单访问地址
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(500)")]
		public string Path { get; set; } = string.Empty;

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty]
		public int? Sort { get; set; }

		/// <summary>
		/// 权限类型
		/// </summary>
		[JsonProperty]
		public int Type { get; set; }

		/// <summary>
		/// 版本
		/// </summary>
		[JsonProperty]
		public long Version { get; set; }

		/// <summary>
		/// 视图
		/// </summary>
		[JsonProperty]
		public long? ViewId { get; set; }

	}

}
