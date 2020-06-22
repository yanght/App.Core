using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.IRepositories
{
    public interface IUserRepository : IAuditBaseRepository<UserEntity>
    {
        /// <summary>
        /// 根据条件得到用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> expression);

        /// <summary>
        /// 根据用户Id更新用户的最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task UpdateLastLoginTimeAsync(long userId);
    }
}
