using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace App.Core.Entitys.LinCms {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "lin_file")]
	public partial class LinFileModel {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(Name = "id", IsPrimary = true, IsIdentity = true)]
		public long Id { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty, Column(Name = "create_time")]
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 创建者ID
		/// </summary>
		[JsonProperty, Column(Name = "create_user_id")]
		public long CreateUserId { get; set; }

		/// <summary>
		/// 删除时间
		/// </summary>
		[JsonProperty, Column(Name = "delete_time")]
		public DateTime? DeleteTime { get; set; }

		/// <summary>
		/// 删除人id
		/// </summary>
		[JsonProperty, Column(Name = "delete_user_id")]
		public long? DeleteUserId { get; set; }

		/// <summary>
		/// 后缀
		/// </summary>
		[JsonProperty, Column(Name = "extension", DbType = "varchar(50)")]
		public string Extension { get; set; } = string.Empty;

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty, Column(Name = "is_deleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// 图片md5值，防止上传重复图片
		/// </summary>
		[JsonProperty, Column(Name = "md5", DbType = "varchar(40)")]
		public string Md5 { get; set; } = string.Empty;

		/// <summary>
		/// 名称
		/// </summary>
		[JsonProperty, Column(Name = "name", DbType = "varchar(300)")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// 路径
		/// </summary>
		[JsonProperty, Column(Name = "path", DbType = "varchar(500)")]
		public string Path { get; set; } = string.Empty;

		/// <summary>
		/// 大小
		/// </summary>
		[JsonProperty, Column(Name = "size")]
		public long? Size { get; set; }

		/// <summary>
		/// 1 local，2 代表七牛云 3 其他表示其他地方
		/// </summary>
		[JsonProperty, Column(Name = "type")]
		public short? Type { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty, Column(Name = "update_time")]
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// 最后修改人Id
		/// </summary>
		[JsonProperty, Column(Name = "update_user_id")]
		public long? UpdateUserId { get; set; }

	}

}
