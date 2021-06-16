using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Snowflake.Core.Configurations;
using Snowflake.Core.Exceptions;
using Snowflake.Core.Responses;
using Snowflake.Core.Utilities;
using Snowflake.Services.Extenstions;
using Snowflake.Services.Middlewares;
using Snowflake.Identity.Extenstions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Snowflake.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            // 配置文件
            var appSections = Configuration.GetSection("AppSettings");
            var appSettings = appSections.Get<AppSetting>();
            services.Configure<AppSetting>(appSections);

            // 添加跨域控制
            services.AddCors();

            // 添加内存缓存
            services.AddMemoryCache();

            // 添加数据压缩
            services.AddHttpClient();

            // 添加压缩组件
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            // 添加系统服务
            services.AddService(appSettings);

            // 添加JWT授权
            services.AddJwtToken(appSettings);

            // 添加控制器
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
            });

            // 添加Swagger
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger, IOptions<AppSetting> appSettings)
        {
            // 全局异常捕获
            app.UseExceptionHandler(errors =>
            {
                errors.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var error = feature?.Error;
                    var result = new ResultObject(999, error.Message);
                    if (!(error is MessageException))
                    {
                        _logger.LogError($"系统出现错误：{error.Message}-{error.StackTrace}");
                    }

                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonHelper.Serialize(result));
                });
            });

            // 配置IP中间件
            app.UseMiddleware<IPAddressMiddleware>();

            // 配置请求中间件
            app.UseMiddleware<RequestTimeMiddleware>();

            // 配置传输压缩
            app.UseResponseCompression();

            // 配置https
            //app.UseHttpsRedirection();

            // 配置静态文件
            app.UseStaticFiles();

            // 配置Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", appSettings.Value.AppName);
                c.RoutePrefix = "swagger";
            });

            // 配置路由
            app.UseRouting();

            // 配置跨域
            app.UseCors(options => options.WithOrigins(appSettings.Value.CORS.ToArray()).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            // 配置授权
            app.UseAuthentication();
            app.UseAuthorization();

            // 配置默认页
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
