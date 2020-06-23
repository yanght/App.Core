using App.Core.Application.Contracts.Groups.Dtos;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.Groups
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<CreateGroupDto, GroupEntity>();
            CreateMap<UpdateGroupDto, GroupEntity>();
            CreateMap<GroupEntity, GroupDto>();
        }
    }
}
