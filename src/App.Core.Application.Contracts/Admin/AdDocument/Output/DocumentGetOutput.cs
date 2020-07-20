using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Contracts.Amdin.AdDocument.Output
{
    public class DocumentGetOutput
    {
		/// <summary>
		/// 编号
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; } = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreatedTime { get; set; }

		/// <summary>
		/// 创建者Id
		/// </summary>
		public long? CreatedUserId { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		public string CreatedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// 启用
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// Html
		/// </summary>
		public string Html { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		public string Label { get; set; } = string.Empty;

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? ModifiedTime { get; set; }

		/// <summary>
		/// 修改者Id
		/// </summary>
		public long? ModifiedUserId { get; set; }

		/// <summary>
		/// 修改者
		/// </summary>
		public string ModifiedUserName { get; set; } = string.Empty;

		/// <summary>
		/// 命名
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// 打开组
		/// </summary>
		public bool? Opened { get; set; }

		/// <summary>
		/// 父级节点
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int? Sort { get; set; }

		/// <summary>
		/// 类型
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// 版本
		/// </summary>
		public long Version { get; set; }
	}
}
