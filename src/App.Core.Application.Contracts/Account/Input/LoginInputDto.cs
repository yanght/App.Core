using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Application.Contracts.Account.Input
{
    public class LoginInputDto
    {
        /// <summary>
        /// 登录名:admin
        /// </summary>
        [Required(ErrorMessage = "登录名为必填项")]
        public string Username { get; set; }
        /// <summary>
        /// 密码：123qwe
        /// </summary>
        [Required(ErrorMessage = "密码为必填项")]
        public string Password { get; set; }
    }
}
