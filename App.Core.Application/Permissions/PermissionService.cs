using App.Core.Application.Contracts.Permissions;
using App.Core.Application.Contracts.Permissions.Dtos;
using App.Core.Data;
using App.Core.Entities;
using App.Core.IRepositories;
using App.Core.Security;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IAuditBaseRepository<PermissionEntity, long> _permissionRepository;
        private readonly IAuditBaseRepository<GroupPermissionEntity, long> _groupPermissionRepository;
        private readonly IMapper _mapper;
        public PermissionService(ICurrentUser currentUser, IAuditBaseRepository<PermissionEntity, long> permissionRepository, IMapper mapper, IAuditBaseRepository<GroupPermissionEntity, long> groupPermissionRepository)
        {
            _currentUser = currentUser;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _groupPermissionRepository = groupPermissionRepository;
        }

        public IDictionary<string, List<PermissionDto>> GetAllStructualPermissions()
        {
            return _permissionRepository.Select.ToList()
                .GroupBy(r => r.Module)
                .ToDictionary(
                    group => group.Key,
                    group =>
                        _mapper.Map<List<PermissionDto>>(group.ToList())
                );

        }

        /// <summary>
        /// 检查当前登录的用户的分组权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(string permission)
        {
            long[] groups = _currentUser.Groups;

            PermissionEntity linPermission = await _permissionRepository.Where(r => r.Name == permission).FirstAsync();

            bool existPermission = await _groupPermissionRepository.Select
                .AnyAsync(r => groups.Contains(r.GroupId) && r.PermissionId == linPermission.Id);

            return existPermission;
        }


        public async Task DeletePermissionsAsync(RemovePermissionDto permissionDto)
        {
            await _groupPermissionRepository.DeleteAsync(r =>
                    permissionDto.PermissionIds.Contains(r.PermissionId) && r.GroupId == permissionDto.GroupId);
        }

        public async Task DispatchPermissions(DispatchPermissionsDto permissionDto, List<PermissionDefinition> permissionDefinitions)
        {
            List<GroupPermissionEntity> linPermissions = new List<GroupPermissionEntity>();
            permissionDto.PermissionIds.ForEach(permissionId =>
            {
                linPermissions.Add(new GroupPermissionEntity(permissionDto.GroupId, permissionId));
            });
            await _groupPermissionRepository.InsertAsync(linPermissions);
        }

        public async Task<List<PermissionEntity>> GetPermissionByGroupIds(List<long> groupIds)
        {
            List<long> permissionIds = _groupPermissionRepository
                .Where(a => groupIds.Contains(a.GroupId))
                .ToList(r => r.PermissionId);

            List<PermissionEntity> listPermissions = await _permissionRepository
                .Where(a => permissionIds.Contains(a.Id))
                .ToListAsync();

            return listPermissions;

        }

        public List<IDictionary<string, object>> StructuringPermissions(List<PermissionEntity> permissions)
        {
            var groupPermissions = permissions.GroupBy(r => r.Module).Select(r => new
            {
                r.Key,
                Children = r.Select(u => u.Name).ToList()
            }).ToList();

            List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();

            foreach (var groupPermission in groupPermissions)
            {
                IDictionary<string, object> moduleExpandoObject = new ExpandoObject();
                List<IDictionary<string, object>> perExpandList = new List<IDictionary<string, object>>();
                groupPermission.Children.ForEach(permission =>
                {
                    IDictionary<string, object> perExpandObject = new ExpandoObject();
                    perExpandObject["module"] = groupPermission.Key;
                    perExpandObject["permission"] = permission;
                    perExpandList.Add(perExpandObject);
                });

                moduleExpandoObject[groupPermission.Key] = perExpandList;
                list.Add(moduleExpandoObject);
            }

            return list;
        }

        public async Task<PermissionEntity> GetAsync(string permissionName)
        {
            return await _permissionRepository.Where(r => r.Name == permissionName).FirstAsync();
        }

    }
}
