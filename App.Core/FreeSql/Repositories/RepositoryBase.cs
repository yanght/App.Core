using App.Core.FreeSql;
using App.Core.FreeSql.UseUnitOfWork;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.FreeSql.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Context"></typeparam>
    public abstract class RepositoryBase<TEntity, Context> : BaseRepository<TEntity>, IRepositoryBase<TEntity, Context>
        where TEntity : class
    {
        public RepositoryBase(IUnitOfWork<Context> unitOfWork) : base(unitOfWork.Orm, null)
        {
            //base.UnitOfWork = uow;
        }
    }

}
