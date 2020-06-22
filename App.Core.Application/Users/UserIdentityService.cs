using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Data.Enum;
using App.Core.Entities;
using App.Core.IRepositories;
using AspNet.Security.OAuth.GitHub;
using AspNet.Security.OAuth.QQ;
using App.Core.Application.Contracts.Cms.Users;

namespace App.Core.Application.Cms.Users
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditBaseRepository<UserIdentity> _userIdentityRepository;

        public UserIdentityService(IAuditBaseRepository<UserIdentity> userIdentityRepository,
            IUserRepository userRepository)
        {
            _userIdentityRepository = userIdentityRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 记录授权成功后的信息
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<long> SaveGitHubAsync(ClaimsPrincipal principal, string openId)
        {
            string email = principal.FindFirst(ClaimTypes.Email)?.Value;
            string name = principal.FindFirst(ClaimTypes.Name)?.Value;
            string gitHubName = principal.FindFirst(GitHubAuthenticationConstants.Claims.Name)?.Value;
            string gitHubApiUrl = principal.FindFirst(GitHubAuthenticationConstants.Claims.Url)?.Value;
            string avatarUrl = principal.FindFirst(AppConsts.Claims.AvatarUrl)?.Value;
            string bio = principal.FindFirst(AppConsts.Claims.BIO)?.Value;
            string blogAddress = principal.FindFirst(AppConsts.Claims.BlogAddress)?.Value;
            Expression<Func<UserIdentity, bool>> expression = r =>
                r.IdentityType == UserIdentity.GitHub && r.Credential == openId;

            UserIdentity linUserIdentity = await _userIdentityRepository.Where(expression).FirstAsync();

            long userId = 0;
            if (linUserIdentity == null)
            {
                UserEntity user = new UserEntity
                {
                    Active = (int) UserActive.Active,
                    Avatar = avatarUrl,
                    CreateTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    Email = email,
                    Introduction = bio,
                    //LinUserGroups = new List<LinUserGroup>()
                    //{
                    //    new LinUserGroup()
                    //    {
                    //        GroupId = LinConsts.Group.User
                    //    }
                    //},
                    Nickname = gitHubName,
                    Username = "",
                    BlogAddress = blogAddress,
                    UserIdentitys = new List<UserIdentity>()
                    {
                        new UserIdentity
                        {
                            CreateTime = DateTime.Now,
                            Credential = openId,
                            IdentityType = UserIdentity.GitHub,
                            Identifier = name,
                        }
                    }
                };
                await _userRepository.InsertAsync(user);
                userId = user.Id;
            }
            else
            {
                userId = linUserIdentity.CreateUserId;
                await _userRepository.UpdateLastLoginTimeAsync(linUserIdentity.CreateUserId);
            }

            return userId;
        }

        /// <summary>
        /// qq快速登录的信息，唯一值openid,昵称(nickname)，性别(gender)，picture（30像素）,picture_medium(50像素），picture_full 100 像素，avatar（40像素），avatar_full(100像素）
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<long> SaveQQAsync(ClaimsPrincipal principal, string openId)
        {
            string nickname = principal.FindFirst(ClaimTypes.Name)?.Value;
            string gender = principal.FindFirst(ClaimTypes.Gender)?.Value;
            string picture = principal.FindFirst(QQAuthenticationConstants.Claims.PictureUrl)?.Value;
            string picture_medium = principal.FindFirst(QQAuthenticationConstants.Claims.PictureMediumUrl)?.Value;
            string picture_full = principal.FindFirst(QQAuthenticationConstants.Claims.PictureFullUrl)?.Value;
            string avatarUrl = principal.FindFirst(QQAuthenticationConstants.Claims.AvatarUrl)?.Value;
            string avatarFullUrl = principal.FindFirst(QQAuthenticationConstants.Claims.AvatarFullUrl)?.Value;

            Expression<Func<UserIdentity, bool>> expression = r =>
                r.IdentityType == UserIdentity.QQ && r.Credential == openId;

            UserIdentity linUserIdentity = await _userIdentityRepository.Where(expression).FirstAsync();

            long userId = 0;
            if (linUserIdentity == null)
            {
                UserEntity user = new UserEntity
                {
                    Active = (int) UserActive.Active,
                    Avatar = avatarFullUrl,
                    CreateTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    Email = "",
                    Introduction = "",
                    //LinUserGroups = new List<LinUserGroup>()
                    //{
                    //    new LinUserGroup()
                    //    {
                    //        GroupId = LinConsts.Group.User
                    //    }
                    //},
                    Nickname = nickname,
                    Username = "",
                    BlogAddress = "",
                   UserIdentitys = new List<UserIdentity>()
                    {
                        new UserIdentity
                        {
                            CreateTime = DateTime.Now,
                            Credential = openId,
                            IdentityType = UserIdentity.QQ,
                            Identifier = nickname,
                        }
                    }
                };
                await _userRepository.InsertAsync(user);
                userId = user.Id;
            }
            else
            {
                userId = linUserIdentity.CreateUserId;
                await _userRepository.UpdateLastLoginTimeAsync(linUserIdentity.CreateUserId);
            }

            return userId;
        }

        public async Task<bool> VerifyUserPasswordAsync(long userId, string password)
        {
            UserIdentity userIdentity = await this.GetFirstByUserIdAsync(userId);

            return userIdentity != null && EncryptUtil.Verify(userIdentity.Credential, password);
        }

        public async Task ChangePasswordAsync(long userId, string newpassword)
        {
            var linUserIdentity = await _userIdentityRepository.Where(a => a.CreateUserId == userId&& a.IdentityType==UserIdentity.Password).FirstAsync();
            await this.ChangePasswordAsync(linUserIdentity, newpassword);
        }
        
        public async Task ChangePasswordAsync(UserIdentity linUserIdentity,string newpassword)
        {
            string encryptPassword = EncryptUtil.Encrypt(newpassword);
            if (linUserIdentity == null)
            {
                linUserIdentity=new UserIdentity()
                {
                    IdentityType = UserIdentity.Password,
                    Identifier = "",
                    Credential = encryptPassword
                };
                await _userIdentityRepository.InsertAsync(linUserIdentity);
            }
            else
            {
                linUserIdentity.Credential = encryptPassword;
                await _userIdentityRepository.UpdateAsync(linUserIdentity);
            }
      
        }

        public async Task DeleteAsync(long userId)
        {
            await _userIdentityRepository.Where(r => r.CreateUserId == userId).ToDelete().ExecuteAffrowsAsync();
        }

        public async Task<UserIdentity> GetFirstByUserIdAsync(long userId)
        {
            return await _userIdentityRepository
                .Where(r => r.CreateUserId == userId && r.IdentityType == UserIdentity.Password)
                .ToOneAsync();
        }
    }
}