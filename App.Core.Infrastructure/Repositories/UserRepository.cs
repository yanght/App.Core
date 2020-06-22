using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Infrastructure.Repositories;
using App.Core.IRepositories;
using App.Core.Security;
using FreeSql;
namespace App.Infrastructure.Repositories
{
    public class UserRepository : AuditBaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        /// <summary>
        /// 根据条件得到用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
        {
            // return Select.Where(expression).IncludeMany(r => r.LinGroups).ToOneAsync();
            return null;
        }

        /// <summary>
        /// 根据用户Id更新用户的最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task UpdateLastLoginTimeAsync(long userId)
        {
            return UpdateDiy.Set(r => new UserEntity()
            {
                LastLoginTime = DateTime.Now
            }).Where(r => r.Id == userId).ExecuteAffrowsAsync();
        }
    }
}
