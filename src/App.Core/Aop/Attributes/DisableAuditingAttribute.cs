using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Aop.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}
