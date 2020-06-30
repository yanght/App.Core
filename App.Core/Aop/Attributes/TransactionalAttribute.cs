using FreeSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Core.Aop.Attributes
{
    /// <summary>
    /// 启用事物
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Requierd;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }
}
