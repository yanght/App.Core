using App.Core.Aop;
using App.Core.Dependency;
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



            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(r => r.FullName.Contains("App.Core")).ToArray();

            //每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
            Type transientDependency = typeof(ITransientDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => transientDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerDependency();

            //同一个Lifetime生成的对象是同一个实例
            Type scopeDependency = typeof(IScopedDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => scopeDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象；
            Type singletonDependency = typeof(ISingletonDependency);
            builder.RegisterAssemblyTypes(currentAssemblies)
                .Where(t => singletonDependency.GetTypeInfo().IsAssignableFrom(t) && t.IsClass && !t.IsAbstract &&
                            !t.IsGenericType)
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}
