﻿using App.Core.Aop.Attributes;
using App.Core.Data.Output;
using App.Core.Entitys.LinCms;
using Masuit.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.Core.Aop.Filter
{
    /// <summary>
    /// 全局日志记录
    /// </summary>
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 操作类型CRUD
        /// </summary>
        //public LogEnum LogType { get; set; }
        private string ActionArguments { get; set; }
        private Stopwatch Stopwatch { get; set; }
        private readonly ILogger<LogActionFilterAttribute> _logger;
        //private readonly ICurrentUser _currentUser;
        public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger/*, ICurrentUser currentUser*/)
        {
            _logger = logger;
            //_currentUser = currentUser;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ActionArguments = JsonConvert.SerializeObject(context.ActionArguments);
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Stopwatch.Stop();
            //当方法或控制器上存在DisableAuditingAttribute特性标签时，不记录日志 
            if (context.ActionDescriptor is ControllerActionDescriptor d && d.MethodInfo.IsDefined(typeof(DisableAuditingAttribute), true) ||
                context.Controller.GetType().IsDefined(typeof(DisableAuditingAttribute), true)
                )
            {
                base.OnActionExecuted(context);
                return;
            }

            LinLogModel linLog = new LinLogModel()
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                StatusCode = context.HttpContext.Response.StatusCode,
                OtherMessage = $"参数：{ActionArguments}\n耗时：{Stopwatch.Elapsed.TotalMilliseconds} 毫秒"
            };

            ControllerActionDescriptor auditActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            //AuditingLogAttribute auditingLogAttribute = auditActionDescriptor.GetCustomAttribute<AuditingLogAttribute>();
            //if (auditingLogAttribute != null)
            //{
            //    linLog.Message = auditingLogAttribute.Template;
            //}

            //LinCmsAuthorizeAttribute linCmsAttribute = auditActionDescriptor.GetCustomAttribute<LinCmsAuthorizeAttribute>();
            //if (linCmsAttribute != null)
            //{
            //    linLog.Authority = linCmsAttribute.Permission;
            //}


            base.OnActionExecuted(context);

            if (context.Result is ObjectResult objectResult && objectResult.Value != null)
            {
                if (objectResult.Value is IResponseOutput response && response.Success == false)
                {
                    if (linLog.Message.IsNullOrEmpty())
                    {
                        linLog.Message = response.Msg?.ToString();
                    }
                }
            }

            linLog.Message += $" 访问{context.HttpContext.Request.Path},耗时：{Stopwatch.Elapsed.TotalMilliseconds} 毫秒";

            //记录文本日志
            _logger.LogInformation(JsonConvert.SerializeObject(linLog));
        }
    }
}
