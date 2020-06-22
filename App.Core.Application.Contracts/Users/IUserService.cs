using App.Core.Application.Contracts.Cms.Users.Dtos;
using App.Core.Data;
using App.Core.Data.Enum;
using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Contracts.Users
{
    public interface IUserService
    {

        Task ChangePasswordAsync(ChangePasswordDto passwordDto);

        /// <summary>
        /// 根据分组条件查询用户信息
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
       // PagedResultDto<UserDto> GetUserListByGroupId(UserSearchDto searchDto);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userActive"></param>
        Task ChangeStatusAsync(long id, UserActive userActive);

        /// <summary>
        /// 注册-新增一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groupIds"></param>
        Task CreateAsync(UserEntity user, List<long> groupIds, string password);

        //Task UpdateAync(long id, UpdateUserDto updateUserDto);

        Task DeleteAsync(long id);

        /// <summary>
        /// 后台管理员重置用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resetPasswordDto"></param>
        //Task ResetPasswordAsync(long id, ResetPasswordDto resetPasswordDto);

        /// <summary>
        /// 得到当前用户上下文
        /// </summary>
        /// <returns></returns>
        Task<UserEntity> GetCurrentUserAsync();

        Task<UserInformation> GetInformationAsync(long userId);

        Task<List<IDictionary<string, object>>> GetStructualUserPermissions(long userId);

        //Task<List<LinPermission>> GetUserPermissions(long userId);

    }
}
