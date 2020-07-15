using App.Core.FreeSql.Config;
using App.Core.FreeSql.UseUnitOfWork;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Core.FreeSql
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 多库注册 直接从配置文件读取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        public static void AddFreeSql<T>(this IServiceCollection service)
        {
            service.AddSingleton(f =>
            {
                var current = f.GetRequiredService<IOptions<FreeSqlCollectionConfig>>().Value.FreeSqlCollections.FirstOrDefault(x => x.Key == typeof(T).Name);
                var builder = new FreeSqlBuilder()
                    .UseConnectionString(current.DataType, current.MasterConnetion)
                    .UseAutoSyncStructure(current.IsSyncStructure)
                    .UseMonitorCommand(executing =>
                    {
                        executing.CommandTimeout = current.CommandTimeout;

                        if (current.DebugShowSql)
                        {

                            //Console.ForegroundColor = newFontColor;
                            //Console.WriteLine("\n=================================================================================\n");
                            //Console.WriteLine(executed.CommandText + "\n");

                            string parametersValue = "";
                            if (current.DebugShowSqlPparameters)
                            {
                                for (int i = 0; i < executing.Parameters.Count; i++)
                                {
                                    parametersValue += $"{executing.Parameters[i].ParameterName}:{executing.Parameters[i].Value}" + ";\n";
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(parametersValue))
                            {
                                //Console.WriteLine(parametersValue);
                                Log.Debug
                             (
                                  "\n=================================================================================\n\n"
                                                              + executing.CommandText + "\n\n"
                                                              + "\n" + parametersValue +
                                  "\n=================================================================================\n\n"
                              );
                            }
                            else
                            {
                                Log.Debug
                              (
                                    "\n=================================================================================\n\n"
                                                                    + executing.CommandText +
                                    "\n\n=================================================================================\n"
                                );
                            }
                            //Console.WriteLine("=================================================================================\n");
                            //Console.ResetColor();
                        }
                    });
                if (current.SlaveConnections.Count > 0)//判断是否存在从库
                {
                    builder.UseSlave(current.SlaveConnections.Select(x => x.ConnectionString).ToArray());
                }
                var fsql = builder.Build<T>();

                #region //使用FreeSql AOP做对应的业务拓展，有需要自行实现

                //res.GlobalFilter.Apply<IDeleted>(SysConsts.IsDeletedDataFilter, x => !x.IsDeleted);
                //res.GlobalFilter.Apply<IEnabled>(SysConsts.IsEnabledDataFilter, x => x.Enabled == true);
                //res.Aop.ConfigEntity += new EventHandler<ConfigEntityEventArgs>((_, e) =>
                //{
                //    var attrs = e.EntityType.GetCustomAttributes(typeof(IndexAttribute), false);
                //    foreach (var attr in attrs)
                //    {
                //        var temp = attr as IndexAttribute;
                //        e.ModifyIndexResult.Add(new FreeSql.DataAnnotations.IndexAttribute(temp.Name, temp.Fields, temp.IsUnique));
                //    }
                //});

                #endregion //使用FreeSql AOP做对应的业务拓展，有需要自行实现
                return fsql;
            });

            service.AddScoped<IUnitOfWork<T>, UnitOfWork<T>>();
        }

        /// <summary>
        /// 简单注册默认数据库
        /// </summary>
        /// <param name="service"></param>
        public static void AddSimpleFreeSql(this IServiceCollection service)
        {
            service.AddSingleton(f =>
            {
                var current = f.GetRequiredService<IOptions<FreeSqlCollectionConfig>>().Value.FreeSqlCollections.FirstOrDefault(x => x.Default);
                var builder = new FreeSqlBuilder()
                    .UseConnectionString(current.DataType, current.MasterConnetion)
                    .UseAutoSyncStructure(current.IsSyncStructure)
                    .UseMonitorCommand(executing =>
                    {
                        executing.CommandTimeout = current.CommandTimeout;

                        if (current.DebugShowSql)
                        {
                            //Console.ForegroundColor = newFontColor;
                            //Console.WriteLine("\n=================================================================================\n");
                            //Console.WriteLine(executed.CommandText + "\n");

                            string parametersValue = "";
                            if (current.DebugShowSqlPparameters)
                            {
                                for (int i = 0; i < executing.Parameters.Count; i++)
                                {
                                    parametersValue += $"{executing.Parameters[i].ParameterName}:{executing.Parameters[i].Value}" + ";\n";
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(parametersValue))
                            {
                                //Console.WriteLine(parametersValue);
                                Log.Debug
                              (
                                  "\n=================================================================================\n\n"
                                                              + executing.CommandText + "\n\n"
                                                              + "\n" + parametersValue +
                                  "\n=================================================================================\n\n"
                              );
                            }
                            else
                            {
                                Log.Debug
                                (
                                    "\n=================================================================================\n\n"
                                                                    + executing.CommandText +
                                    "\n\n=================================================================================\n"
                                );
                            }
                            //Console.WriteLine("=================================================================================\n");
                            //Console.ResetColor();
                        }
                    });
                if (current.SlaveConnections.Count > 0)//判断是否存在从库
                {
                    builder.UseSlave(current.SlaveConnections.Select(x => x.ConnectionString).ToArray());
                }
                var fsql = builder.Build();

                #region //使用FreeSql AOP做对应的业务拓展，有需要自行实现

                //res.GlobalFilter.Apply<IDeleted>(SysConsts.IsDeletedDataFilter, x => !x.IsDeleted);
                //res.GlobalFilter.Apply<IEnabled>(SysConsts.IsEnabledDataFilter, x => x.Enabled == true);
                //res.Aop.ConfigEntity += new EventHandler<ConfigEntityEventArgs>((_, e) =>
                //{
                //    var attrs = e.EntityType.GetCustomAttributes(typeof(IndexAttribute), false);
                //    foreach (var attr in attrs)
                //    {
                //        var temp = attr as IndexAttribute;
                //        e.ModifyIndexResult.Add(new FreeSql.DataAnnotations.IndexAttribute(temp.Name, temp.Fields, temp.IsUnique));
                //    }
                //});

                #endregion //使用FreeSql AOP做对应的业务拓展，有需要自行实现

                return fsql;
            });
            service.AddScoped<UnitOfWorkManager>();
        }
    }
}
