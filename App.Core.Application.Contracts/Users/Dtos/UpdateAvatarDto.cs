using System.ComponentModel.DataAnnotations;

namespace App.Core.Application.Contracts.Users.Dtos
{
    public class UpdateAvatarDto
    {
        [Required(ErrorMessage = "请输入头像url")]
        public string Avatar { get; set; }
    }
}
