using App.Core.Application.Contracts.Permissions.Dtos;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Permissions
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionEntity, PermissionDto>();
        }
    }
}
