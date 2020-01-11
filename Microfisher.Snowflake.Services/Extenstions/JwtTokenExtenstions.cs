using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microfisher.Snowflake.Core.Configurations;
using Microfisher.Snowflake.Core.Enums;
using Microfisher.Snowflake.Services.Permissions;
using Microfisher.Snowflake.Core.Responses;
using Microfisher.Snowflake.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Microfisher.Snowflake.Services.Extenstions
{
    public static class JwtTokenExtenstions
    {
        public static void AddJwtToken(this IServiceCollection services, AppSetting appSettings)
        {
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddSingleton<IAuthorizationHandler, CoreHandler>();
            services.AddSingleton<IAuthorizationHandler, TwoFactorHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TwoFactor", policy => policy.Requirements.Add(new TwoFactorRequirement()));

                options.AddPolicy("Supervisor", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Supervisor)));

                options.AddPolicy("Manager", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Manager)));

                options.AddPolicy("Director", policy => policy.Requirements.Add(new ManagerRequirement(LevelEnum.Director)));

                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new CoreRequirement(LevelEnum.Beginner)).Build();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = appSettings.Jwt.Issuer,
                    //ValidAudience = appSettings.Jwt.Audience,
                    ClockSkew = TimeSpan.FromMinutes(0),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Secret)),
                };

                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = StatusCodes.Status200OK;
                        c.Response.ContentType = "application/json; charset=utf-8";
                        c.Response.WriteAsync(JsonHelper.Serialize(new ResultObject(800, c.Exception.Message))).Wait();
                        return Task.CompletedTask;
                    },
                    OnForbidden = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = StatusCodes.Status200OK;
                        c.Response.ContentType = "application/json; charset=utf-8";
                        c.Response.WriteAsync(JsonHelper.Serialize(new ResultObject(801, "没有权限执行操作"))).Wait();
                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = StatusCodes.Status200OK;
                        c.Response.ContentType = "application/json; charset=utf-8";
                        c.Response.WriteAsync(JsonHelper.Serialize(new ResultObject(802, "账户授权认证失败")));
                        return Task.CompletedTask;
                    }
                };
            });

            // 使用Cookies 
            //.AddCookie(options =>
            // {
            //     options.Cookie.Name = "Token";
            //     options.Cookie.Domain = ".cg.com";
            //     options.Events.OnRedirectToLogin = BuildRedirectToLogin;//是认证失败跳转
            //     options.Events.OnSigningOut = BuildSigningOut;//注销跳转
            //     options.Cookie.HttpOnly = true;//防止xss攻击
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //     options.LoginPath = "/Account/Login";
            //     options.LogoutPath = "/Account/Logout";
            //     options.SlidingExpiration = true;
            //     options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(@"D:\sso\key"));
            //     options.TicketDataFormat = new TicketDataFormat(new AesDataProtector());
            // });
        }
    }
}
