using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    [Table(Name = "groups")]
    public class GroupEntity : Entity<long>
    {
        /// <summary>
        /// 权限组名称
        /// </summary>
        [Column(DbType = "varchar(60)")]
        public string Name { get; set; }
        /// <summary>
        /// 权限组描述
        /// </summary>
        [Column(DbType = "varchar(500)")]
        public string Info { get; set; }

        /// <summary>
        /// 是否是静态分组
        /// </summary>
        public bool IsStatic { get; set; } = false;

        [Navigate(ManyToMany = typeof(UserGroupEntity))]
        public virtual ICollection<UserEntity> Users { get; set; }

        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// Cms管理员
        /// </summary>
        public const string CmsAdmin = "CmsAdmin";

        /// <summary>
        /// 普通用户
        /// </summary>
        public const string User = "User";

    }
}
