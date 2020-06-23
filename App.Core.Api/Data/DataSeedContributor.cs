using App.Core.Api.Utils;
using App.Core.Data;
using App.Core.Dependency;
using App.Core.Entities;
using App.Core.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Core.Api.Data
{
    public interface IDataSeedContributor
    {
        Task SeedAsync();

    }
    public class DataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private readonly IAuditBaseRepository<PermissionEntity> _permissionRepository;
        private readonly IAuditBaseRepository<GroupPermissionEntity> _groupPermissionRepository;
        private readonly ILogger<DataSeedContributor> _logger;
        public DataSeedContributor(IAuditBaseRepository<PermissionEntity> permissionRepository, IAuditBaseRepository<GroupPermissionEntity> groupPermissionRepository, ILogger<DataSeedContributor> logger)
        {
            _permissionRepository = permissionRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _logger = logger;
        }

        /// <summary>
        /// 权限标签上的Permission改变时，删除数据库中存在的无效权限，并生成新的权限。
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            List<PermissionDefinition> linCmsAttributes = ReflexHelper.GeAssemblyLinCmsAttributes();

            List<PermissionEntity> insertPermissions = new List<PermissionEntity>();
            List<PermissionEntity> allPermissions = await _permissionRepository.Select.ToListAsync();

            Expression<Func<GroupPermissionEntity, bool>> expression = u => false;
            Expression<Func<PermissionEntity, bool>> permissionExpression = u => false;
            allPermissions.ForEach(permissioin =>
            {
                if (!linCmsAttributes.Any(r => r.Permission == permissioin.Name))
                {
                    expression = expression.Or(r => r.PermissionId == permissioin.Id);
                    permissionExpression = permissionExpression.Or(r => r.Id == permissioin.Id);
                }
            });

            int effectRows = await _permissionRepository.DeleteAsync(permissionExpression);
            effectRows += await _groupPermissionRepository.DeleteAsync(expression);
            _logger.LogInformation($"删除了{effectRows}条数据");

            linCmsAttributes.ForEach(r =>
            {
                bool exist = allPermissions.Any(u => u.Module == r.Module && u.Name == r.Permission);
                if (!exist)
                {
                    insertPermissions.Add(new PermissionEntity(r.Permission, r.Module));
                }
            });
            await _permissionRepository.InsertAsync(insertPermissions);
            _logger.LogInformation($"新增了{insertPermissions.Count}条数据");
        }
    }
}
