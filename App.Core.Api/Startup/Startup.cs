using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using App.Core.FreeSql;
using App.Core.FreeSql.Config;
using App.Core.FreeSql.DbContext;
using App.Core.FreeSql.UseUnitOfWork;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace App.Core.Api.Startup
{
    public class Startup
    {
        private static string basePath => AppContext.BaseDirectory;
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<FreeSqlCollectionConfig>(_configuration.GetSection("SqlConfig"));
            services.AddFreeSql<AdminContext>();
            services.AddFreeSql<LinCmsContext>();

            //services.AddSingleton(typeof(IFreeSqlUnitOfWorkManager), typeof(FreeSqlUnitOfWorkManager));
            #region Swagger
            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                string ApiName = "App.Core";
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = ApiName + RuntimeInformation.FrameworkDescription,
                    Version = "v1"
                });

                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                };
                options.AddSecurityRequirement(security); //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                try
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlPath, true);
                    //实体层的xml文件名
                    //string xmlEntityPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IEntity).Assembly.GetName().Name}.xml");
                    //options.IncludeXmlComments(xmlEntityPath);
                    //Dto所在类库
                    //string applicationPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IApplicationService).Assembly.GetName().Name}.xml");
                    //options.IncludeXmlComments(applicationPath);
                }
                catch (Exception ex)
                {

                }
            });

            #endregion

            services.AddControllers();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger Api文档
            if (_env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.Core");
                    //c.RoutePrefix = string.Empty;
                    //c.OAuthClientId(Configuration["Service:ClientId"]);//客服端名称
                    //c.OAuthAppName(Configuration["Service:Name"]); // 描述
                });
            }
            #endregion

        }
    }
}
