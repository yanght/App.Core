using App.Core.Aop;
using App.Core.FreeSql;
using App.Core.FreeSql.DbContext;
using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Core.Api.Startup
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesDllFile = Assembly.Load("App.Core.Application");
            Assembly assemblysRepository = Assembly.Load("App.Core.Infrastructure");

            List<Type> interceptorServiceTypes = new List<Type>();
            builder.RegisterType<TransactionInterceptor>();
            interceptorServiceTypes.Add(typeof(TransactionInterceptor));
            //builder.RegisterType<UnitOfWorkInterceptor>();
            //builder.RegisterType<UnitOfWorkAsyncInterceptor>();
            //interceptorServiceTypes.Add(typeof(UnitOfWorkInterceptor));

            builder.RegisterAssemblyTypes(servicesDllFile)
                    .Where(a => a.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .InterceptedBy(interceptorServiceTypes.ToArray())
                    .EnableInterfaceInterceptors();

            builder.RegisterAssemblyTypes(assemblysRepository)
                        .Where(a => a.Name.EndsWith("Repository"))
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();

            
        }
    }
}
