using App.Core.Aop.Attributes;
using App.Core.Data.Output;
using App.Core.Extensions;
using Castle.DynamicProxy;
using FreeSql;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Core.Aop
{
    public class TransactionInterceptor : IInterceptor
    {
        IUnitOfWork _unitOfWork;
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly ILogger<TransactionInterceptor> _logger;
        public TransactionInterceptor(ILogger<TransactionInterceptor> logger, UnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if (method.HasAttribute<TransactionalAttribute>())
            {
                InterceptTransaction(invocation, method);
            }
            else
            {
                invocation.Proceed();
            }
        }

        private async void InterceptTransaction(IInvocation invocation, MethodInfo method)
        {
            try
            {
                var transaction = method.GetAttribute<TransactionalAttribute>();
                _unitOfWork = _unitOfWorkManager.Begin(transaction.Propagation, transaction.IsolationLevel);
                invocation.Proceed();

                dynamic returnValue = invocation.ReturnValue;
                if (returnValue is Task)
                {
                    returnValue = await returnValue;
                }

                if (returnValue is IResponseOutput res && !res.Success)
                {
                    _unitOfWork.Rollback();
                    _logger.LogError($"{method.Name}: 事务执行失败，回滚成功。{res.Msg}");                    
                }
                else
                {
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.LogError(ex, $"{method.Name}: 事务执行失败，回滚成功。");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
