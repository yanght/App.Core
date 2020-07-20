using App.Core.Data.Enums;
using App.Core.Data.Output;
using App.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Aop.Middleware
{
    /// <summary>
    /// 自定义异常中间件
    /// </summary>
    public class CustomExceptionMiddleWare : IMiddleware
    {
        private readonly ILogger<CustomExceptionMiddleWare> _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public CustomExceptionMiddleWare(ILogger<CustomExceptionMiddleWare> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); //调用管道执行下一个中间件
            }
            catch (Exception ex)
            {
                try
                {
                    await HandlerExceptionAsync(context, ex);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, "处理异常再出异常");
                }
            }
        }

        async Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex is AppException cmsException) //自定义业务异常
            {
                await JsonHandle(context, cmsException.Message, cmsException.GetErrorCode(),
                    cmsException.GetCode());
            }
            else
            {

                _logger.LogError(ex, "系统异常信息");
                if (_environment.IsDevelopment())
                {
                    string errorMsg = "异常信息：";

                    void ReadException(Exception ex)
                    {
                        errorMsg += $"{ex.Message} | {ex.StackTrace}";
                        if (ex.InnerException != null)
                        {
                            ReadException(ex.InnerException);
                        }
                    }
                    ReadException(ex);
                    await JsonHandle(context, errorMsg, ErrorCode.UnknownError, 500);
                }
                else
                {
                    await JsonHandle(context, "服务器正忙，请稍后再试!", ErrorCode.UnknownError, 500);
                }
            }
        }

        /// <summary>
        /// 处理方式：返回Json格式
        /// </summary>
        /// <returns></returns>
        private async Task JsonHandle(HttpContext context, string errorMsg, ErrorCode errorCode, int statusCode)
        {
            var result = ResponseOutput.NotOk(errorCode, errorMsg);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result, settings), Encoding.UTF8);
        }

    }
}
