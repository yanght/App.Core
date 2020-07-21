using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Core.Aop.Attributes;
using App.Core.Application.Contracts.Account.Input;
using App.Core.Exceptions;
using AutoMapper;
using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using App.Core.Data.Enums;
using IdentityModel;
using App.Core.Security;

namespace App.Core.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICurrentUser _currentUser;
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, IMapper mapper, IHttpClientFactory httpClientFactory, ICurrentUser currentUser)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _currentUser = currentUser;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginInputDto">用户名/密码：admin/123qwe</param>
        [DisableAuditing]
        [HttpPost("login")]
        public async Task<JObject> Login(LoginInputDto loginInputDto)
        {
            _logger.LogInformation("login");

            HttpClient client = _httpClientFactory.CreateClient();

            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["Service:Authority"],
                Policy = {
                        RequireHttps = false
                    }
            });

            if (disco.IsError)
            {
                throw new AppException(disco.Error);
            }

            TokenResponse response = await client.RequestTokenAsync(new PasswordTokenRequest()
            {
                Address = disco.TokenEndpoint,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientId = _configuration["Service:ClientId"],
                ClientSecret = _configuration["Service:ClientSecret"],
                Parameters = { { "UserName", loginInputDto.Username },
                        { "Password", loginInputDto.Password }
                    },
                Scope = _configuration["Service:Name"],
            });

            if (response.IsError)
            {
                throw new AppException(response.ErrorDescription);
            }
            return response.Json;
        }

        /// <summary>
        /// 刷新用户的token
        /// </summary>
        /// <returns></returns>
        [HttpGet("refresh")]
        public async Task<JObject> GetRefreshToken()
        {
            string refreshToken;

            string authHeader = Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Bearer"))
            {
                refreshToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            else
            {
                throw new AppException(" 请先登录.", ErrorCode.RefreshTokenError);
            }

            HttpClient client = _httpClientFactory.CreateClient();

            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _configuration["Service:Authority"],
                Policy = {
                        RequireHttps = true
                    }
            });

            if (disco.IsError)
            {
                throw new AppException(disco.Error);
            }

            TokenResponse response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = OidcConstants.GrantTypes.RefreshToken,

                ClientId = _configuration["Service:ClientId"],
                ClientSecret = _configuration["Service:ClientSecrets"],

                Parameters = new Dictionary<string, string>
                    { { OidcConstants.TokenRequest.RefreshToken, refreshToken }
                    }
            });

            if (response.IsError)
            {
                throw new AppException("请重新登录", ErrorCode.RefreshTokenError);
            }

            return response.Json;
        }

        [HttpGet("userinfo")]
        public ICurrentUser GetUserInfo()
        {
            return _currentUser;
        }

    }
}
