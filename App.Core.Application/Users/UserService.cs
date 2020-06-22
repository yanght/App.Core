using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Application.Contracts.Cms.Users;
using App.Core.Application.Contracts.Cms.Users.Dtos;
using App.Core.Application.Contracts.Users;
using App.Core.Common;
using App.Core.Data.Enum;
using App.Core.Entities;
using App.Core.Exceptions;
using App.Core.IRepositories;
using App.Core.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace App.Core.Application.Cms.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IUserIdentityService _userIdentityService;
        //private readonly IPermissionService _permissionService;
        //private readonly IGroupService _groupService;
        //private readonly IFileRepository _fileRepository;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IUserIdentityService userIdentityService
          )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _userIdentityService = userIdentityService;
          
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

            //user.LinUserGroups = new List<LinUserGroup>();
            //groupIds?.ForEach(groupId =>
            //{
            //    user.LinUserGroups.Add(new LinUserGroup()
            //    {
            //        GroupId = groupId
            //    });
            //});

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

        public Task<UserInformation> GetInformationAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IDictionary<string, object>>> GetStructualUserPermissions(long userId)
        {
            throw new NotImplementedException();
        }
    }
}