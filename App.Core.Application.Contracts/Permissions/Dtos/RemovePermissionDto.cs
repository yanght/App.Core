using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Application.Contracts.Permissions.Dtos
{
    public class RemovePermissionDto : IValidatableObject
    {
        public long GroupId { get; set; }
        public List<long> PermissionIds { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GroupId <= 0)
            {
                yield return new ValidationResult("分组id必须大于0", new List<string>() { "GroupId" });
            }
            if (PermissionIds.Count == 0)
            {
                yield return new ValidationResult("请输入Permission字段", new List<string>() { "Permission" });
            }
        }
    }
}
