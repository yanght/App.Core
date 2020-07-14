using App.Core.Data.Input;
using App.Core.Data.Output;
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
    public abstract class SimpleRepositoryBase<TEntity, TKey> : BaseRepository<TEntity, TKey> where TEntity : class, new()
    {
        //private readonly IUser _user;
        protected SimpleRepositoryBase(UnitOfWorkManager uowm) : base(uowm.Orm, null, null)
        {
            uowm.Binding(this);
            // _user = user;
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







        //public async Task<bool> SoftDeleteAsync(TKey id)
        //{
        //    await UpdateDiy
        ////        .SetDto(new
        ////        {
        ////            IsDeleted = true,
        ////            ModifiedUserId = _user.Id,
        ////            ModifiedUserName = _user.Name
        ////        })
        ////        .WhereDynamic(id)
        ////        .ExecuteAffrowsAsync();
        ////    return true;
        ////}

        //public async Task<bool> SoftDeleteAsync(TKey[] ids)
        //{
        //    await UpdateDiy
        //        .SetDto(new
        //        {
        //            IsDeleted = true,
        //            ModifiedUserId = _user.Id,
        //            ModifiedUserName = _user.Name
        //        })
        //        .WhereDynamic(ids)
        //        .ExecuteAffrowsAsync();
        //    return true;
        //}
    }

    public abstract class SimpleRepositoryBase<TEntity> : SimpleRepositoryBase<TEntity, long> where TEntity : class, new()
    {
        protected SimpleRepositoryBase(UnitOfWorkManager uowm) : base(uowm)
        {
        }
    }

}
