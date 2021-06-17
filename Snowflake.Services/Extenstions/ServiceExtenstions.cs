using System;
using System.IO.Compression;
using CSRedis;
using Snowflake.Core.Configurations;
using Snowflake.Data.Interfaces;
using Snowflake.Data.Implements;
using Snowflake.Data;
using Snowflake.Data.Entities;
using Snowflake.Services;
using Snowflake.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Snowflake.Services.Extenstions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        public static void AddService(this IServiceCollection services, AppSetting appSettings)
        {
            // 添加数据仓储
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User, long>, Repository<User, long>>();
            services.AddScoped<IRepository<UserBalance, long>, Repository<UserBalance, long>>();

            // 添加业务服务
            services.AddScoped<IUserManager, UserManager>();

            // 添加其他服务
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 初始缓存组件
            var redisClient = new CSRedisClient(appSettings.Redis.ConnectionString);
            RedisHelper.Initialization(redisClient);

        }
    }
}
