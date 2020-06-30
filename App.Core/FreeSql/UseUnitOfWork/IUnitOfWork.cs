using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.FreeSql.UseUnitOfWork
{
    public interface IUnitOfWork<Context> : IUnitOfWork
    {
    }
}
