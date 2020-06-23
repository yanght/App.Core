using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Aop.Filter;
using App.Core.Aop.Log;
using App.Core.Application.Contracts.Users;
using App.Core.Application.Contracts.Users.Dtos;
using App.Core.Data;
using App.Core.Entities;
using App.Core.IRepositories;
using App.Core.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IFreeSql _freeSql;
        private readonly IMapper _mapper;
        private readonly IUserService _userSevice;
        private readonly ICurrentUser _currentUser;
        private readonly IUserRepository _userRepository;
        public UserController(IFreeSql freeSql, IMapper mapper, IUserService userSevice, ICurrentUser currentUser, IUserRepository userRepository)
        {
            _freeSql = freeSql;
            _mapper = mapper;
            _userSevice = userSevice;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }
        [HttpGet("get")]
        public JsonResult Get()
        {            
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        /// <summary>
        /// 新增用户-不是注册，注册不可能让用户选择gourp_id
        /// </summary>
        /// <param name="userInput"></param>
        [AuditingLog("管理员新建了一个用户")]
        [HttpPost("register")]
        public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUserDto userInput)
        {
            await _userSevice.CreateAsync(_mapper.Map<UserEntity>(userInput), userInput.GroupIds, userInput.Password);
            return UnifyResponseDto.Success("用户创建成功");
        }

        /// <summary>
        /// 得到当前登录人信息
        /// </summary>
        [HttpGet("information")]
        public async Task<UserInformation> GetInformationAsync()
        {
            UserInformation userInformation = await _userSevice.GetInformationAsync(_currentUser.Id ?? 0);
            return userInformation;
        }

    }
}
