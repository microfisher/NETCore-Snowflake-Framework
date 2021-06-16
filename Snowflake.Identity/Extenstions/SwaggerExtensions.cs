using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Snowflake.Identity.Extenstions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Snowflake",
                    Description = "身份认证微服务",
                    Contact = new OpenApiContact
                    {
                        Name = "Microfisher",
                        Url = new Uri("https://github.com/microfisher"),
                    },
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求Header中需要添加的JWT授权代码：Bearer JXDJBV....",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                //c.EnableAnnotations();

                //c.IgnoreObsoleteActions(); // 忽略过时的接口[Obsolete]

                //c.(api => api.HttpMethod); // 对action根据Http请求进行分组

                //c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}"); // 指定接口排序规则
                // System.AppDomain.CurrentDomain.FriendlyName

                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.Core.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.Data.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.Services.xml"));
                c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.Identity.xml"));

            });
        }
    }
}
