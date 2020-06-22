using System;

namespace App.Core.Aop.Log
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
