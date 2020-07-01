using App.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Exceptions
{
    [Serializable]
    public class AppException : ApplicationException
    {
        /// <summary>
        /// 状态码
        /// </summary>
        private readonly int _code;
        /// <summary>
        /// 错误码，当为0时，代表正常
        /// </summary>

        private readonly ErrorCode _errorCode;
        /// <summary>
        /// 
        /// </summary>
        public AppException() : base("服务器繁忙，请稍后再试!")
        {
            _errorCode = ErrorCode.Fail;
            _code = 400;
        }

        public AppException(string message = "服务器繁忙，请稍后再试!", ErrorCode errorCode = ErrorCode.Fail, int code = 400) : base(message)
        {
            this._errorCode = errorCode;
            _code = code;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCode()
        {
            return _code;
        }

        public ErrorCode GetErrorCode()
        {
            return _errorCode;
        }
    }
}
