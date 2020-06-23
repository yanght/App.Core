using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Application.Contracts.Groups.Dtos
{
    public class UpdateGroupDto
    {
        [Required(ErrorMessage = "请输入分组名称")]
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
