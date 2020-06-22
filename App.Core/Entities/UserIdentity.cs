using System;
using App.Core.Entities;
using FreeSql.DataAnnotations;

namespace App.Core.Entities
{
    /// <summary>
    /// 用户身份认证登录表
    /// </summary>
    [Table(Name = "user_identity")]
    public class UserIdentity : FullAduitEntity<Guid>
    {
        public const string GitHub = "GitHub";
        public const string Password = "Password";
        public const string QQ = "QQ";
        public const string WeiXin = "WeiXin";

        /// <summary>
        ///认证类型， Password，GitHub、QQ、WeiXin等
        /// </summary>
        [Column(DbType = "varchar(20)")]
        public string IdentityType { get; set; }
      
        /// <summary>
        /// 认证者，例如 用户名,手机号，邮件等，
        /// </summary>
        [Column(DbType = "varchar(24)")]
        public string Identifier { get; set; }

        /// <summary>
        ///  凭证，例如 密码,存OpenId、Id，同一IdentityType的OpenId的值是唯一的
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string Credential { get; set; }
    }
}
