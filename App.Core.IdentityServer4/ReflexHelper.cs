using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using FreeSql.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.IdentityServer4
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflexHelper
    {
        public static List<T> GetAssemblies<T>() where T : Attribute
        {
            List<T> listT = new List<T>();
            List<Type> assembly = typeof(Program).Assembly.GetTypes().AsEnumerable()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToList();

            assembly.ForEach(d =>
            {
                T appAuthorize = d.GetCustomAttribute<T>();
                listT.Add(appAuthorize);
            });
            return listT;
        }


        /// <summary>
        /// 扫描 IEntity类所在程序集，反射得到所有类上有特性标签为TableAttribute
        /// </summary>
        /// <returns></returns>
        public static Type[] GetEntityTypes(Type type)
        {
            List<Type> tableAssembies = new List<Type>();
            Assembly.GetAssembly(type).GetExportedTypes().ForEach(o =>
            {
                foreach (Attribute attribute in o.GetCustomAttributes())
                {
                    if (attribute is TableAttribute appAuthorize)
                    {
                        tableAssembies.Add(o);
                    }
                }
            });
            return tableAssembies.ToArray();
        }
    }
}
