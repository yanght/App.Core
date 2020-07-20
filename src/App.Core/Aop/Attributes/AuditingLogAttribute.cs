using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Aop.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditingLogAttribute : Attribute
    {
        public string Template { get; }

        public AuditingLogAttribute(string template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }
    }
}
