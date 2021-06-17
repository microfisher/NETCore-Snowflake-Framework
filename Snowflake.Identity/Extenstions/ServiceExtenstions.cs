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
using Snowflake.Services.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;


namespace Snowflake.Identity.Extenstions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注册其他服务
        /// </summary>
        public static void AddService(this IServiceCollection services, AppSetting appSettings)
        {

            // 添加JWT授权
            services.AddJwtToken(appSettings);

            // 添加Swagger
            services.AddSwagger();

            // 添加跨域控制
            services.AddCors();

            // 添加内存缓存
            services.AddMemoryCache();

            // 添加数据压缩
            services.AddHttpClient();

            // 添加HTTP服务
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 添加压缩组件
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            // 添加控制器
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
            });

            // 初始缓存组件
            var redisClient = new CSRedisClient(appSettings.Redis.ConnectionString);
            RedisHelper.Initialization(redisClient);

        }
    }
}
