﻿using System;
using App.Core.Common;
using App.Core.Data;
using App.Core.Data.Enum;
using App.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App.Core.Aop.Filter
{
    /// <summary>
    /// 出现异常时，如AppException业务异常，会先执行方法过滤器 （LogActionFilterAttribute）的OnActionExecuted才会执行此异常过滤器。
    /// </summary>
    public class AppExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        public AppExceptionFilter(ILogger<AppExceptionFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException cmsException)
            {
                HandlerException(context,
                    new UnifyResponseDto()
                    {
                        Message = cmsException.Message,
                        Code = cmsException.GetErrorCode()
                    },
                    cmsException.GetCode()
                    );
                return;
            }

            string error = "异常信息：";

            void ReadException(Exception ex)
            {
                error += $"{ex.Message} | {ex.StackTrace} | {ex.InnerException}";
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }
            ReadException(context.Exception);

            _logger.LogError(error);

            UnifyResponseDto apiResponse = new UnifyResponseDto()
            {
                Code = ErrorCode.UnknownError,
                Message = _environment.IsDevelopment() ? error : "服务器正忙，请稍后再试."
            };

            HandlerException(context, apiResponse, StatusCodes.Status500InternalServerError);
        }

        private void HandlerException(ExceptionContext context, UnifyResponseDto apiResponse, int statusCode)
        {
            apiResponse.Request = AppUtils.GetRequest(context.HttpContext);

            context.Result = new JsonResult(apiResponse)
            {
                StatusCode = statusCode,
                ContentType = "application/json",
            };
            context.ExceptionHandled = true;
        }
    }
}
