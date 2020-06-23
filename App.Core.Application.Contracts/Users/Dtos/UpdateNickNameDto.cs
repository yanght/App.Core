﻿using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.Contracts.Users.Dtos
{
    public class UpdateNicknameDto
    {
        [Required(ErrorMessage = "请输入昵称")]
        [StringLength(24,ErrorMessage = "昵称应在24个字符内")]
        public string Nickname { get; set; }
    }
}
