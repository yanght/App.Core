using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Aop.Filter;
using App.Core.Api.Utils;
using App.Core.Application.Contracts.Permissions;
using App.Core.Application.Contracts.Permissions.Dtos;
using App.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// 查询所有可分配的权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AppAuthorize("查询所有可分配的权限", "管理员")]
        public IDictionary<string, List<PermissionDto>> GetAllPermissions()
        {
            return _permissionService.GetAllStructualPermissions();
        }

        /// <summary>
        /// 删除某个组别的权限
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        [HttpPost("remove")]
        [AppAuthorize("删除多个权限", "管理员")]
        public async Task<UnifyResponseDto> RemovePermissions(RemovePermissionDto permissionDto)
        {
            await _permissionService.DeletePermissionsAsync(permissionDto);
            return UnifyResponseDto.Success("删除权限成功");
        }

        /// <summary>
        /// 分配多个权限
        /// </summary>
        /// <param name="permissionDto"></param>
        /// <returns></returns>
        [HttpPost("dispatch/batch")]
        [AppAuthorize("分配多个权限", "管理员")]
        public async Task<UnifyResponseDto> DispatchPermissions(DispatchPermissionsDto permissionDto)
        {
            List<PermissionDefinition> permissionDefinitions = ReflexHelper.GeAssemblyLinCmsAttributes();
            await _permissionService.DispatchPermissions(permissionDto, permissionDefinitions);
            return UnifyResponseDto.Success("添加权限成功");
        }
    }
}
