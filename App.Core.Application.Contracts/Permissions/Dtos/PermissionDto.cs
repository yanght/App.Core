using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Contracts.Permissions.Dtos
{
    public class PermissionDto : EntityDto<long>
    {
        public PermissionDto(string name, string module)
        {
            Name = name;
            Module = module;
        }

        public string Name { get; set; }
        public string Module { get; set; }
    }
}
