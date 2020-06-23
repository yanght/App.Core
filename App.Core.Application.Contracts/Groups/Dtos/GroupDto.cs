using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Contracts.Groups.Dtos
{
    public class GroupDto : Entity
    {
        public List<PermissionEntity> Permissions { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool IsStatic { get; set; }

    }
}
