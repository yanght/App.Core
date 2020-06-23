using App.Core.Application.Contracts.Permissions.Dtos;
using App.Core.Data;
using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.Contracts.Permissions
{
    public interface IPermissionService
    {
        IDictionary<string, List<PermissionDto>> GetAllStructualPermissions();
        Task<bool> CheckPermissionAsync(string permission);
        Task DeletePermissionsAsync(RemovePermissionDto permissionDto);

        Task DispatchPermissions(DispatchPermissionsDto permissionDto, List<PermissionDefinition> permissionDefinition);

        Task<List<PermissionEntity>> GetPermissionByGroupIds(List<long> groupIds);

        List<IDictionary<string, object>> StructuringPermissions(List<PermissionEntity> permissions);

        Task<PermissionEntity> GetAsync(string permissionName);
    }
}
