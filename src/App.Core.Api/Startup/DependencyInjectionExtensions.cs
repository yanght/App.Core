using App.Core.Aop.Middleware;
using App.Core.Application.Contracts.Admin.AdDocument;
using App.Core.FreeSql;
using App.Core.FreeSql.Config;
using App.Core.FreeSql.DbContext;
using AutoMapper;
using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Api.Startup
{
    public static class DependencyInjectionExtensions
    {
        #region 初始化 Redis配置
        /// <summary>
        /// 注册redis客户端
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCsRedisCore(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection csRediSection = configuration.GetSection("ConnectionStrings:CsRedis");
            CSRedisClient csRedisClient = new CSRedisClient(csRediSection.Value);
            //初始化 RedisHelper
            RedisHelper.Initialization(csRedisClient);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
        }
        #endregion

        /// <summary>
        /// 注册数据库连接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FreeSqlCollectionConfig>(configuration.GetSection("SqlConfig"));
            services.AddSimpleFreeSql();//注册默认数据库连接
            services.AddFreeSql<AdminContext>();
            services.AddFreeSql<LinCmsContext>();
        }
        /// <summary>
        /// 注入实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<CustomExceptionMiddleWare>();
            services.AddAutoMapper(typeof(MapConfig).Assembly);
        }
    }
}
