using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Contracts.LinCms.Books.Output
{
    public class BookGetOutput
	{
		/// <summary>
		/// 主键Id
		/// </summary>
		public long Id { get; set; }

		public string Author { get; set; } = string.Empty;

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 创建者ID
		/// </summary>
		public long CreateUserId { get; set; }

		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? DeleteTime { get; set; }

		/// <summary>
		/// 删除人id
		/// </summary>
		public long? DeleteUserId { get; set; }

		public string Image { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool IsDeleted { get; set; }

		public string Summary { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// 最后修改人Id
		/// </summary>
		public long? UpdateUserId { get; set; }
	}
}
