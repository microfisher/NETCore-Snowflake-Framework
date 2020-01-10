using System;
using System.Linq;
using System.Reflection;
using Microfisher.Snowflake.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Microfisher.Snowflake.Services.Extenstions
{
    public static class AutomaticExtenstions
    {
        public static IServiceCollection AddDenpendency(this IServiceCollection services)
        {
            var baseType = typeof(IDenpendency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();

            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();

            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                    services.AddScoped(interfaceType, implementType);
            }

            return services;
        }
    }
}
