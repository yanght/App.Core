using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Aop.Filter;
using App.Core.Application.Contracts.Groups;
using App.Core.Application.Contracts.Groups.Dtos;
using App.Core.Data;
using App.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("all")]
        [AppAuthorize("查询所有权限组", "管理员")]
        public Task<List<GroupEntity>> GetListAsync()
        {
            return _groupService.GetListAsync();
        }

        [HttpGet("{id}")]
        [AppAuthorize("查询一个权限组及其权限", "管理员")]
        public async Task<GroupDto> GetAsync(long id)
        {
            GroupDto groupDto = await _groupService.GetAsync(id);
            return groupDto;
        }

        [HttpPost]
        [AppAuthorize("新建权限组", "管理员")]
        public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateGroupDto inputDto)
        {
            await _groupService.CreateAsync(inputDto);
            return UnifyResponseDto.Success("新建分组成功");
        }

        [HttpPut("{id}")]
        [AppAuthorize("更新一个权限组", "管理员")]
        public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] UpdateGroupDto updateGroupDto)
        {
            await _groupService.UpdateAsync(id, updateGroupDto);
            return UnifyResponseDto.Success("更新分组成功");
        }

        [HttpDelete("{id}")]
        [AppAuthorize("删除一个权限组", "管理员")]
        public async Task<UnifyResponseDto> DeleteAsync(long id)
        {
            await _groupService.DeleteAsync(id);
            return UnifyResponseDto.Success("删除分组成功");
        }
    }
}
