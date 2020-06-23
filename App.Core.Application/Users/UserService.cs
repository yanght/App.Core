using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Application.Contracts.Groups;
using App.Core.Application.Contracts.Groups.Dtos;
using App.Core.Application.Contracts.Users;
using App.Core.Application.Contracts.Users.Dtos;
using App.Core.Common;
using App.Core.Data.Enum;
using App.Core.Entities;
using App.Core.Exceptions;
using App.Core.IRepositories;
using App.Core.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace App.Core.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IUserIdentityService _userIdentityService;
        //private readonly IPermissionService _permissionService;
        private readonly IGroupService _groupService;
        //private readonly IFileRepository _fileRepository;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IUserIdentityService userIdentityService,
            IGroupService groupService
          )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _userIdentityService = userIdentityService;
            _groupService = groupService;
        }
        public async Task CreateAsync(UserEntity user, List<long> groupIds, string password)
        {
            if (!string.IsNullOrEmpty(user.Username))
            {
                bool isRepeatName =await _userRepository.Select.AnyAsync(r => r.Username == user.Username);

                if (isRepeatName)
                {
                    throw new AppException("用户名重复，请重新输入", ErrorCode.RepeatField);
                }
            }

            if (!string.IsNullOrEmpty(user.Email.Trim()))
            {
                var isRepeatEmail = await _userRepository.Select.AnyAsync(r => r.Email == user.Email.Trim());
                if (isRepeatEmail)
                {
                    throw new AppException("注册邮箱重复，请重新输入", ErrorCode.RepeatField);
                }
            }

            user.Active = UserActive.Active.GetHashCode();

            user.UserGroups = new List<UserGroupEntity>();
            groupIds?.ForEach(groupId =>
            {
                user.UserGroups.Add(new UserGroupEntity()
                {
                    GroupId = groupId
                });
            });

            user.UserIdentitys = new List<UserIdentity>()
            {
                new UserIdentity()
                {
                    IdentityType = UserIdentity.Password,
                    Credential = EncryptUtil.Encrypt(password),
                    Identifier = user.Username,
                    CreateTime = DateTime.Now,
                }
            };
            await _userRepository.InsertAsync(user);
        }



        public async Task ChangeStatusAsync(long id, UserActive userActive)
        {
           UserEntity user = await _userRepository.Select.Where(r => r.Id == id).ToOneAsync();

            if (user == null)
            {
                throw new AppException("用户不存在", ErrorCode.NotFound);
            }

            if (user.IsActive() && userActive == UserActive.Active)
            {
                throw new AppException("当前用户已处于禁止状态");
            }

            if (!user.IsActive() && userActive == UserActive.NotActive)
            {
                throw new AppException("当前用户已处于激活状态");
            }

            await _userRepository.UpdateDiy.Where(r => r.Id == id)
                .Set(r => new {Active = userActive.GetHashCode()})
                .ExecuteUpdatedAsync();
        }

        public async Task<UserEntity> GetCurrentUserAsync()
        {
            if (_currentUser.Id != null)
            {
                long userId = (long) _currentUser.Id;
                return await _userRepository.Select.Where(r => r.Id == userId).ToOneAsync();
            }

            return null;
        }

        public Task ChangePasswordAsync(ChangePasswordDto passwordDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInformation> GetInformationAsync(long userId)
        {
            UserEntity linUser = await _userRepository.GetUserAsync(r => r.Id == userId);
            if (linUser == null) return null;
           // linUser.Avatar = _fileRepository.GetFileUrl(linUser.Avatar);

            UserInformation userInformation = _mapper.Map<UserInformation>(linUser);
            userInformation.Groups = linUser.Groups.Select(r => _mapper.Map<GroupDto>(r)).ToList();
            userInformation.Admin = _currentUser.IsInGroup(AppConsts.Group.Admin);

            return userInformation;
        }

        public Task<List<IDictionary<string, object>>> GetStructualUserPermissions(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAync(long id, UpdateUserDto updateUserDto)
        {
            UserEntity linUser = await _userRepository.Where(r => r.Id == id).ToOneAsync();
            if (linUser == null)
            {
                throw new AppException("用户不存在", ErrorCode.NotFound);
            }

            List<long> existGroupIds = await _groupService.GetGroupIdsByUserIdAsync(id);

            //删除existGroupIds有，而newGroupIds没有的
            List<long> deleteIds = existGroupIds.Where(r => !updateUserDto.GroupIds.Contains(r)).ToList();

            //添加newGroupIds有，而existGroupIds没有的
            List<long> addIds = updateUserDto.GroupIds.Where(r => !existGroupIds.Contains(r)).ToList();

            _mapper.Map(updateUserDto, linUser);
            await _userRepository.UpdateAsync(linUser);
            await _groupService.DeleteUserGroupAsync(id, deleteIds);
            await _groupService.AddUserGroupAsync(id, addIds);
        }
    }
}