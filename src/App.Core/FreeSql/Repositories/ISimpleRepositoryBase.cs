using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace App.Core.FreeSql.Repositories
{
    public interface ISimpleRepositoryBase<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        #region 查询单个对象

        TDto Get<TDto>(TKey id);
        Task<TDto> GetAsync<TDto>(TKey id);

        TEntity Get(Expression<Func<TEntity, bool>> exp);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        TDto Get<TDto>(Expression<Func<TEntity, bool>> exp);
        Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp);

        #endregion

        #region 查询列表

        List<TEntity> GetList();
        Task<List<TEntity>> GetListAsync();

        List<TDto> GetList<TDto>();
        Task<List<TDto>> GetListAsync<TDto>();


        List<TEntity> GetList(Expression<Func<TEntity, bool>> exp);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp);

        List<TDto> GetList<TDto>(Expression<Func<TEntity, bool>> exp);

        Task<List<TDto>> GetListAsync<TDto>(Expression<Func<TEntity, bool>> exp);

        #endregion

        ///// <summary>
        ///// 软删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> SoftDeleteAsync(TKey id);

        ///// <summary>
        ///// 批量删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> SoftDeleteAsync(TKey[] id);
    }

    public interface ISimpleRepositoryBase<TEntity> : ISimpleRepositoryBase<TEntity, long> where TEntity : class
    {
    }
}
