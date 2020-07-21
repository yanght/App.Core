using App.Core.Aop.Filter;
using App.Core.Aop.Middleware;
using App.Core.Application.Contracts;
using App.Core.Data.Enums;
using App.Core.Data.Output;
using App.Core.Entitys;
using Autofac;
using DotNetCore.Security;
using Masuit.Tools;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Api.Startup
{
    public class Startup
    {
        private static string BasePath => AppContext.BaseDirectory;
        private readonly IWebHostEnvironment Environment;
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSecurity(Configuration);
            services.AddCsRedisCore(Configuration);
            services.AddContext(Configuration);
            services.AddDIServices(Configuration);

            #region Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
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
                options.AddSecurityRequirement(security); //���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������Bearer��
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) �����ṹ: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", //jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header, //jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                try
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlPath, true);
                    //ʵ����xml�ļ���
                    string xmlEntityPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IEntity).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlEntityPath);
                    //Dto�������
                    string applicationPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(IApplicationService).Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(applicationPath);
                }
                catch (Exception ex)
                {

                }
            });

            #endregion

            #region Api Version
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVVV";//api������ʽ
                option.AssumeDefaultVersionWhenUnspecified = true;//�Ƿ��ṩAPI�汾����
            });
            #endregion

            services.AddControllers(options =>
            {
                options.Filters.Add<LogActionFilterAttribute>();// ������󷽷�ʱ����־��¼������

            }).AddNewtonsoftJson(options =>
            {
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //ʹ���շ� ����ĸСд                             
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

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
                    options.LoginPath = "/app/oauth2/signin";
                    options.LogoutPath = "/app/oauth2/signout";
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //identityserver4 ��ַ 
                    options.Authority = Configuration["Service:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["Service:Name"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("app-dotnetcore-SecurityKey")),

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
                    //    ClockSkew = TimeSpan.Zero   //ƫ������Ϊ��0s,���ڲ��Թ��ڲ���,��ȫ����access_token�Ĺ���ʱ����ԣ�Ĭ��ԭ��Ϊ5����
                    //};


                    //ʹ��Authorize����Ϊ��Ҫ��¼ʱ������json��ʽ���ݡ�
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
                            //�˴�����Ϊ��ֹ.Net CoreĬ�ϵķ������ͺ����ݽ�����������ҪŶ
                            context.HandleResponse();

                            string message;
                            ErrorCode errorCode;
                            int statusCode = StatusCodes.Status401Unauthorized;

                            if (context.Error == "invalid_token" &&
                                context.ErrorDescription == "The token is expired")
                            {
                                message = "���ƹ���";
                                errorCode = ErrorCode.TokenExpired;
                                statusCode = StatusCodes.Status422UnprocessableEntity;
                            }
                            else if (context.Error == "invalid_token" && context.ErrorDescription.IsNullOrEmpty())
                            {
                                message = "����ʧЧ";
                                errorCode = ErrorCode.TokenInvalidation;
                            }

                            else
                            {
                                message = "���ȵ�¼" + context.ErrorDescription; //""��֤ʧ�ܣ���������ͷ�������µ�¼";
                                errorCode = ErrorCode.AuthenticationFailed;
                            }

                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = statusCode;
                            context.Response.WriteAsync(ResponseOutput.NotOk(errorCode, message).ToJsonString());

                            return Task.FromResult(0);
                        }
                    };
                });

            #endregion
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //�쳣�м��Ӧ����MVCִ��������м����ǰ�棬�����쳣ʱUnitOfWorkMiddleware�޷�catch�쳣
            app.UseMiddleware(typeof(CustomExceptionMiddleWare));

            #region Swagger Api�ĵ�
            if (Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }
            #endregion

            app.UseCors(builder =>
            {
                string[] withOrigins = Configuration.GetSection("WithOrigins").Get<string[]>();
                builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(withOrigins);
            });

            app.UseAuthentication();//��֤

            app.UseRouting()
                .UseAuthorization()//��Ȩ
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
