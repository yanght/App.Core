using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Core.Aop.Log;
using App.Core.Application.Contracts.Account;
using App.Core.Application.Contracts.Users;
using App.Core.Data;
using App.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace App.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userSevice;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, IUserService userSevice, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _userSevice = userSevice;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerDto"></param>
        [AuditingLog("用户注册")]
        [HttpPost("account/register")]
        public UnifyResponseDto Post([FromBody] RegisterDto registerDto)
        {
            UserEntity user = _mapper.Map<UserEntity>(registerDto);
            _userSevice.CreateAsync(user, new List<long>(), registerDto.Password);
            return UnifyResponseDto.Success("注册成功");
        }
    }
}
