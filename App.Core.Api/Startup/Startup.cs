using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using App.Core.Aop.Filter;
using App.Core.Aop.Middleware;
using App.Core.Api.SnakeCaseQuery;
using App.Core.Application.Contracts;
using App.Core.Application.Contracts.Users;
using App.Core.Application.Users;
using App.Core.Common;
using App.Core.Data;
using App.Core.Data.Enum;
using App.Core.Entities;
using App.Core.Infrastructure.Repositories;
using App.Core.IRepositories;
using App.Core.Security;
using App.Infrastructure.Repositories;
using Autofac;
using AutoMapper;
using DotNetCore.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace App.Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddContext(Configuration);
            services.AddSecurity(Configuration);
            services.AddAutoMapper(typeof(UserProfile).Assembly);

            #region Swagger

            //Swagger重写PascalCase，改成SnakeCase模式
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, ApiDescriptionProvider>());

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                string ApiName = "App.Core.Api";
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = ApiName + RuntimeInformation.FrameworkDescription,
                    Version = "v1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "luoyunchong@foxmail.com", Url = new Uri("https://www.cnblogs.com/igeekfan/") },
                    License = new OpenApiLicense { Name = ApiName + " 官方文档", Url = new Uri("https://luoyunchong.github.io/vovo-docs/dotnetcore/lin-cms/dotnetcore-start.html") }
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
                    string xmlEntityPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IEntity).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlEntityPath);
                    //Dto所在类库
                    string applicationPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IApplicationService).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(applicationPath);
                }
                catch (Exception ex)
                {
                   // Log.Logger.Warning(ex.Message);
                }
            });

            #endregion

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserIdentityService, UserIdentityService>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient(typeof(IAuditBaseRepository<>), typeof(AuditBaseRepository<>));
            services.AddTransient(typeof(IAuditBaseRepository<,>), typeof(AuditBaseRepository<,>));
            services.AddTransient<CustomExceptionMiddleWare>();

            services.AddControllers(options =>
            {
                options.ValueProviderFactories.Add(new ValueProviderFactory()); //设置SnakeCase形式的QueryString参数
                options.Filters.Add<LogActionFilterAttribute>(); // 添加请求方法时的日志记录过滤器
                                                                 // options.Filters.Add<LinCmsExceptionFilter>(); // 添加请求方法时的日志记录过滤器
            });

            services.AddDIServices();

            #region AddJwtBearer
            var jsonWebTokenSettings = services.BuildServiceProvider().GetRequiredService<JsonWebTokenSettings>();
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/cms/oauth2/signin";
                    options.LogoutPath = "/cms/oauth2/signout";
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //identityserver4 地址 也就是本项目地址
                    options.Authority = Configuration["Service:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["Service:Name"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebTokenSettings.Key)),

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = jsonWebTokenSettings.Issuer,

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = jsonWebTokenSettings.Audience,

                        // Validate the token expiry
                        ValidateLifetime = true,

                        // If you want to allow a certain amount of clock drift, set thatValidIssuer  here
                        //ClockSkew = TimeSpan.Zero
                    };

                    //options.TokenValidationParameters = new TokenValidationParameters()
                    //{
                    //    ClockSkew = TimeSpan.Zero   //偏移设置为了0s,用于测试过期策略,完全按照access_token的过期时间策略，默认原本为5分钟
                    //};


                    //使用Authorize设置为需要登录时，返回json格式数据。
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //Token expired
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦
                            context.HandleResponse();

                            string message;
                            ErrorCode errorCode;
                            int statusCode = StatusCodes.Status401Unauthorized;

                            if (context.Error == "invalid_token" &&
                                context.ErrorDescription == "The token is expired")
                            {
                                message = "令牌过期";
                                errorCode = ErrorCode.TokenExpired;
                                statusCode = StatusCodes.Status422UnprocessableEntity;
                            }
                            else if (context.Error == "invalid_token" && context.ErrorDescription.IsNullOrEmpty())
                            {
                                message = "令牌失效";
                                errorCode = ErrorCode.TokenInvalidation;
                            }

                            else
                            {
                                message = "请先登录" + context.ErrorDescription; //""认证失败，请检查请求头或者重新登录";
                                errorCode = ErrorCode.AuthenticationFailed;
                            }

                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = statusCode;
                            context.Response.WriteAsync(new UnifyResponseDto(errorCode, message, context.HttpContext)
                                .ToString());

                            return Task.FromResult(0);
                        }
                    };
                })
                .AddGitHub(options =>
                {
                    options.ClientId = Configuration["Authentication:GitHub:ClientId"];
                    options.ClientSecret = Configuration["Authentication:GitHub:ClientSecret"];
                    options.Scope.Add("user:email");
                    options.ClaimActions.MapJsonKey(AppConsts.Claims.AvatarUrl, "avatar_url");
                    //登录成功后可通过  authenticateResult.Principal.FindFirst(ClaimTypes.Uri)?.Value;  得到GitHub头像
                    options.ClaimActions.MapJsonKey(AppConsts.Claims.BIO, "bio");
                    options.ClaimActions.MapJsonKey(AppConsts.Claims.BlogAddress, "blog");
                })
                .AddQQ(options =>
                {
                    options.ClientId = Configuration["Authentication:QQ:ClientId"];
                    options.ClientSecret = Configuration["Authentication:QQ:ClientSecret"];
                });

            #endregion
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.Core.Api");
                //c.RoutePrefix = string.Empty;
                //c.OAuthClientId(Configuration["Service:ClientId"]);//客服端名称
                //c.OAuthAppName(Configuration["Service:Name"]); // 描述
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            //将身份验证中间件添加到管道中，以便对主机的每次调用都将自动执行身份验证。
            app.UseAuthentication();
            //授权中间件，以确保匿名客户端无法访问我们的API端点。
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
