﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Core.Application.Contracts.Groups.Dtos
{
    public class CreateGroupDto : UpdateGroupDto, IValidatableObject
    {
        public List<long> PermissionIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PermissionIds.Count == 0)
            {
                yield return new ValidationResult("请选择权限", new List<string> { "PermissionIds" });
            }
        }
    }
}
