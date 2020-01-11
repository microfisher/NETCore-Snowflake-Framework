using System;
using System.IO.Compression;
using CSRedis;
using Microfisher.Snowflake.Core.Configurations;
using Microfisher.Snowflake.Data.Repositories;
using Microfisher.Snowflake.Data;
using Microfisher.Snowflake.Data.Entities;
using Microfisher.Snowflake.Data.Repositories;
using Microfisher.Snowflake.Services;
using Microfisher.Snowflake.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Microfisher.Snowflake.Services.Extenstions
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
            services.AddScoped<IAccountService, AccountService>();

            // 添加其他服务
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 初始缓存组件
            var redisClient = new CSRedisClient(appSettings.Redis.ConnectionString);
            RedisHelper.Initialization(redisClient);

        }
    }
}
