using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Dependency;

namespace App.Core.FreeSql.Repositories
{
    public interface IRepositoryBase<TEntity, TContext> : IBaseRepository<TEntity>, IScopeDependency
    where TEntity : class
    {
       
    }
}
