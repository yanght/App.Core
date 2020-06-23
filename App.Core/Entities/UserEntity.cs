using App.Core.Data.Enum;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities
{
    [Table(Name = "users")]
    public class UserEntity : FullAduitEntity
    {
        public UserEntity() { }
        public UserEntity(string username)
        {
            this.Username = username;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Column(DbType = "varchar(24)")]
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Column(DbType = "varchar(24)")]
        public string Nickname { get; set; }

        /// <summary>
        ///  用户默认生成图像，为null、头像url
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Column(DbType = "varchar(100)")]
        public string Email { get; set; }

        /// <summary>
        /// 当前用户是否为激活状态，非激活状态默认失去用户权限 ; 1 -> 激活 | 2 -> 非激活
        /// </summary>
        public int Active { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column(DbType = "varchar(100)")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        [Column(DbType = "varchar(100)")]
        public string Introduction { get; set; }

        /// <summary>
        ///  个人主页
        /// </summary>
        public string BlogAddress { get; set; }

        /// <summary>
        /// 最后一次登录的时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        [Navigate(ManyToMany = typeof(UserGroupEntity))]
        public virtual ICollection<GroupEntity>Groups { get; set; }
        [Navigate("UserId")]
        public virtual ICollection<UserGroupEntity> UserGroups { get; set; }
        [Navigate("CreateUserId")]
        public virtual ICollection<UserIdentity> UserIdentitys { get; set; }

        public bool IsActive()
        {
            return Active == (int)UserActive.Active;
        }
    }
}
