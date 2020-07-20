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
    public abstract class RepositoryBase<TEntity, Context> : RepositoryBase<TEntity, long, Context>
        where TEntity : class
    {
        public RepositoryBase(IUnitOfWork<Context> unitOfWork) : base(unitOfWork)
        {
            //base.UnitOfWork = uow;
        }
    }


    public abstract class RepositoryBase<TEntity, TKey, Context> : BaseRepository<TEntity, TKey>
       where TEntity : class
    {
        public RepositoryBase(IUnitOfWork<Context> unitOfWork) : base(unitOfWork.Orm, null)
        {
            //base.UnitOfWork = uow;
        }
        #region 查询单个对象
        public virtual TDto Get<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOne<TDto>();
        }
        public virtual Task<TDto> GetAsync<TDto>(TKey id)
        {
            return Select.WhereDynamic(id).ToOneAsync<TDto>();
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOne();
        }
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync();
        }

        public virtual TDto Get<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOne<TDto>();
        }
        public virtual Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToOneAsync<TDto>();
        }

        #endregion

        #region 查询列表

        public virtual List<TEntity> GetList()
        {
            return Select.ToList();
        }

        public virtual Task<List<TEntity>> GetListAsync()
        {
            return Select.ToListAsync();
        }

        public virtual List<TDto> GetList<TDto>()
        {
            return Select.ToList<TDto>();
        }
        public virtual Task<List<TDto>> GetListAsync<TDto>()
        {
            return Select.ToListAsync<TDto>();
        }


        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToList();
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToListAsync();
        }

        public virtual List<TDto> GetList<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToList<TDto>();
        }

        public virtual Task<List<TDto>> GetListAsync<TDto>(Expression<Func<TEntity, bool>> exp)
        {
            return Select.Where(exp).ToListAsync<TDto>();
        }

        #endregion
    }

}
