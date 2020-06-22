using App.Core.Entities;
using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.IdentityServer4
{
    public static class DependencyInjectionExtensions
    {
        #region FreeSql
        /// <summary>
        /// FreeSql
        /// </summary>
        /// <param name="services"></param>
        public static void AddContext(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetSection("ConnectionStrings:MySql");

            IFreeSql fsql = new FreeSqlBuilder()
                   .UseConnectionString(DataType.MySql, configurationSection.Value)
                   .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                   .UseAutoSyncStructure(true)
                   .UseMonitorCommand(cmd =>
                   {
                       Console.WriteLine(cmd.CommandText);
                   }
                   )
                   .Build();
            fsql.CodeFirst.IsSyncStructureToLower = true;
            services.AddSingleton(fsql);
            services.AddScoped<UnitOfWorkManager>();

            fsql.CodeFirst.SyncStructure(ReflexHelper.GetEntityTypes(typeof(IEntity)));

            services.AddFreeRepository(filter =>
            {
                filter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false);
            });
        }
        #endregion
    }
}
