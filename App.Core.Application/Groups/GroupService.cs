using App.Core.Application.Contracts.Groups;
using App.Core.Application.Contracts.Groups.Dtos;
using App.Core.Common;
using App.Core.Data.Enum;
using App.Core.Entities;
using App.Core.Exceptions;
using App.Core.IRepositories;
using App.Core.Security;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IFreeSql _freeSql;
        private readonly IMapper _mapper;
       // private readonly IPermissionService _permissionService;
        private readonly ICurrentUser _currentUser;
        private readonly IAuditBaseRepository<GroupEntity, long> _groupRepository;
        private readonly IAuditBaseRepository<UserGroupEntity, long> _userGroupRepository;
        public GroupService(IFreeSql freeSql,
            IMapper mapper,
           // IPermissionService permissionService,
            ICurrentUser currentUser,
            IAuditBaseRepository<GroupEntity, long> groupRepository,
            IAuditBaseRepository<UserGroupEntity, long> userGroupRepository
            )
        {
            _freeSql = freeSql;
            _mapper = mapper;
           // _permissionService = permissionService;
            _currentUser = currentUser;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<List<GroupEntity>> GetListAsync()
        {
            List<GroupEntity> linGroups = await _groupRepository.Select
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return linGroups;
        }

        public async Task<GroupDto> GetAsync(long id)
        {
            GroupEntity group = await _groupRepository.Where(r => r.Id == id).FirstAsync();
            GroupDto groupDto = _mapper.Map<GroupDto>(group);
            //groupDto.Permissions = await _permissionService.GetPermissionByGroupIds(new List<long>() { id });
            return groupDto;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateGroupDto inputDto)
        {
            bool exist = await _groupRepository.Select.AnyAsync(r => r.Name == inputDto.Name);
            if (exist)
            {
                throw new AppException("分组已存在，不可创建同名分组", ErrorCode.RepeatField);
            }

            GroupEntity linGroup = _mapper.Map<GroupEntity>(inputDto);

            using var conn = _freeSql.Ado.MasterPool.Get();
            await using DbTransaction transaction = await conn.Value.BeginTransactionAsync();
            try
            {
                long groupId = await _freeSql.Insert(linGroup).WithTransaction(transaction).ExecuteIdentityAsync();
                List<PermissionEntity> allPermissions = await _freeSql.Select<PermissionEntity>().WithTransaction(transaction).ToListAsync();
                List<GroupPermissionEntity> linPermissions = new List<GroupPermissionEntity>();
                inputDto.PermissionIds.ForEach(r =>
                {
                   PermissionEntity pdDto = allPermissions.FirstOrDefault(u => u.Id == r);
                    if (pdDto == null)
                    {
                        throw new AppException($"不存在此权限:{r}", ErrorCode.NotFound);
                    }
                    linPermissions.Add(new GroupPermissionEntity(groupId, pdDto.Id));
                });

                await _freeSql.Insert<GroupPermissionEntity>()
                       .WithTransaction(transaction)
                       .AppendData(linPermissions)
                       .ExecuteAffrowsAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            //_freeSql.Transaction(() =>
            //{
            //    long groupId = _freeSql.Insert(linGroup).ExecuteIdentity();

            //    List<LinPermission> allPermissions = _freeSql.Select<LinPermission>().ToList();

            //    List<LinGroupPermission> linPermissions = new List<LinGroupPermission>();
            //    inputDto.PermissionIds.ForEach(r =>
            //    {
            //        LinPermission pdDto = allPermissions.FirstOrDefault(u => u.Id == r);
            //        if (pdDto == null)
            //        {
            //            throw new AppException($"不存在此权限:{r}", ErrorCode.NotFound);
            //        }
            //        linPermissions.Add(new LinGroupPermission(groupId, pdDto.Id));
            //    });

            //    _freeSql.Insert<LinGroupPermission>().AppendData(linPermissions).ExecuteAffrows();
            //});
        }

        public async Task UpdateAsync(long id, UpdateGroupDto updateGroupDto)
        {
            await _groupRepository.UpdateDiy.Where(r => r.Id == id).Set(a => new GroupEntity()
            {
                Info = updateGroupDto.Info,
                Name = updateGroupDto.Name
            }).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 删除group拥有的权限、删除group表的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            GroupEntity linGroup = await _groupRepository.Where(r => r.Id == id).FirstAsync();

            if (linGroup.IsNull())
            {
                throw new AppException("分组不存在，删除失败", ErrorCode.NotFound, StatusCodes.Status404NotFound);
            }

            if (linGroup.IsStatic)
            {
                throw new AppException("无法删除静态权限组!");
            }

            bool exist = await _userGroupRepository.Select.AnyAsync(r => r.GroupId == id);
            if (exist)
            {
                throw new AppException("分组下存在用户，不可删除", ErrorCode.Inoperable);
            }

            _freeSql.Transaction(() =>
            {
                _freeSql.Delete<GroupPermissionEntity>(new GroupPermissionEntity { GroupId = id }).ExecuteAffrows();
                _freeSql.Delete<GroupEntity>(new GroupEntity { Id = id }).ExecuteAffrows();
            });

        }

        public async Task DeleteUserGroupAsync(long userId)
        {
            await _userGroupRepository.DeleteAsync(r => r.UserId == userId);
        }

        public bool CheckIsRootByUserId(long userId)
        {
            return _currentUser.IsInGroup(AppConsts.Group.Admin);
        }

        public async Task<List<long>> GetGroupIdsByUserIdAsync(long userId)
        {
            return await _userGroupRepository.Where(r => r.UserId == userId).ToListAsync(r => r.GroupId);
        }

        public async Task DeleteUserGroupAsync(long userId, List<long> deleteGroupIds)
        {
            await _userGroupRepository.DeleteAsync(r => r.UserId == userId && deleteGroupIds.Contains(r.GroupId));
        }

        public async Task AddUserGroupAsync(long userId, List<long> addGroupIds)
        {
            if (addGroupIds == null || addGroupIds.IsEmpty())
                return;
            bool valid = await this.CheckGroupExistByIds(addGroupIds);
            if (!valid)
            {
                throw new AppException("cant't add user to non-existent group");
            }
            List<UserGroupEntity> userGroups = new List<UserGroupEntity>();
            addGroupIds.ForEach(groupId => { userGroups.Add(new UserGroupEntity(userId, groupId)); });
            await _userGroupRepository.InsertAsync(userGroups);
        }

        private async Task<bool> CheckGroupExistById(long id)
        {
            return await _groupRepository.Where(r => r.Id == id).AnyAsync();
        }

        private async Task<bool> CheckGroupExistByIds(List<long> ids)
        {
            foreach (var id in ids)
            {
                bool valid = await CheckGroupExistById(id);
                if (!valid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
