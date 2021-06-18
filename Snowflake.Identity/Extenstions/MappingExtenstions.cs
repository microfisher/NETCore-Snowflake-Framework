using System;
using System.Linq;
using System.Reflection;
using Snowflake.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Snowflake.Identity.Extenstions
{

    public static class MappingExtenstions
    {
        /// <summary>
        /// 自动注入依赖关系
        /// </summary>
        public static IServiceCollection AddDenpendency(this IServiceCollection services)
        {
            var baseType = typeof(IDependency);
            var scopeType = typeof(IScopeDependency);
            var transientType = typeof(ITransientDependency);
            var singletonType = typeof(ISingletonDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();

            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface && x != scopeType && x != transientType && x != singletonType).ToArray();
            foreach (var implementType in implementTypes)
            {
                if (!implementType.IsGenericType)
                {
                    if (typeof(IScopeDependency).IsAssignableFrom(implementType))
                    {
                        var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                        if (interfaceType != null)
                        {
                            services.AddScoped(interfaceType, implementType);
                        }
                    }
                    else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
                    {
                        var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                        if (interfaceType != null)
                        {
                            services.AddSingleton(interfaceType, implementType);
                        }
                    }
                    else
                    {
                        var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                        if (interfaceType != null)
                        {
                            services.AddTransient(interfaceType, implementType);
                        }
                    }
                }
                else
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsGenericAssignableFrom(implementType));
                    services.AddTransient(interfaceType, implementType);
                }
            }


            return services;
        }

        /// <summary>
        /// 检查范型类型匹配
        /// </summary>
        /// <param name="generic">范型接口</param>
        /// <param name="type">范型类型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type generic, Type type)
        {
            return type.GetInterfaces().Any(x => generic == (x.IsGenericType ? x.GetGenericTypeDefinition() : x));
        }

    }
}
