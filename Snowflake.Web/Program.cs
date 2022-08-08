using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Snowflake.Core.Configurations;
using Snowflake.Core.Exceptions;
using Snowflake.Core.Responses;
using Snowflake.Core.Utilities;
using Snowflake.Services.Middlewares;
using Snowflake.Web.Extenstions;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Configuration.UserSecrets;
using MySql.Data.MySqlClient;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("system started");


try
{


    var builder = WebApplication.CreateBuilder(args);

    // 添加配置文件
    var appSections = builder.Configuration.GetSection("AppSettings");
    var appSettings = appSections.Get<AppSetting>();
    builder.Services.Configure<AppSetting>(appSections);

    // 添加系统服务
    builder.Services.AddService(appSettings);

    // 添加自动注入
    builder.Services.AddDenpendency();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();

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
                logger.Error($"系统出现错误：{error.Message}-{error.StackTrace}");
            }

            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonHelper.Serialize(result));
        });
    });

    // 配置https
    //app.UseHttpsRedirection();

    // 配置IP中间件
    app.UseMiddleware<IPAddressMiddleware>();

    // 配置请求中间件
    app.UseMiddleware<RequestTimeMiddleware>();

    // 配置传输压缩
    app.UseResponseCompression();

    // 配置静态文件
    app.UseStaticFiles();

    // 配置Swagger
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", appSettings.AppName);
            c.RoutePrefix = "swagger";
        });
    }


    // 配置路由
    app.UseRouting();

    // 配置跨域
    app.UseCors(options => options.WithOrigins(appSettings.CORS.ToArray()).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

    // 配置授权
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

