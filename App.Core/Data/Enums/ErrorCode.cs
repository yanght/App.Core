using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.Core.Data.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success = 0,
        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("未知错误")]
        UnknownError = 1007,
        /// <summary>
        /// 服务器未知错误
        /// </summary>
        [Description("服务器未知错误")]
        ServerUnknownError = 999,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 1000,
        /// <summary>
        /// 认证失败
        /// </summary>
        [Description("认证失败")]
        AuthenticationFailed = 10000,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermission = 10001,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 9999,
        /// <summary>
        /// refreshToken异常
        /// </summary>
        [Description("refreshToken异常")]
        RefreshTokenError = 10100,
        /// <summary>
        /// 资源不存在
        /// </summary>
        [Description("资源不存在")]
        NotFound = 10020,
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParameterError = 10030,
        /// <summary>
        /// 令牌失效
        /// </summary>
        [Description("令牌失效")]
        TokenInvalidation = 10040,
        /// <summary>
        /// 令牌过期
        /// </summary>
        [Description("令牌过期")]
        TokenExpired = 10050,
        /// <summary>
        /// 字段重复
        /// </summary>
        [Description("字段重复")]
        RepeatField = 10060,
        /// <summary>
        /// 禁止操作
        /// </summary>
        [Description("禁止操作")]
        Inoperable = 10070,
        //10080 请求方法不允许

        //10110 文件体积过大

        //10120 文件数量过多

        //10130 文件扩展名不符合规范

        //10140 请求过于频繁，请稍后重试
        ManyRequests = 10140
    }
}
