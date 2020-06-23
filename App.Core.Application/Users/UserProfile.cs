using App.Core.Application.Contracts.Account;
using App.Core.Application.Contracts.Users.Dtos;
using App.Core.Entities;
using AutoMapper;

namespace App.Core.Application.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<UpdateUserDto, UserEntity>();
            CreateMap<UserEntity, UserInformation>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEntity, OpenUserDto>();
            CreateMap<UserEntity, UserNoviceDto>();
            CreateMap<RegisterDto, UserEntity>();
        }
    }
}
