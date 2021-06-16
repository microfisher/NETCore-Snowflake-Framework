using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Snowflake.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Snowflake.Core.Extensions;
using System.Text;
using Microsoft.Extensions.Options;
using Snowflake.Core.Configurations;

namespace Snowflake.Services.Permissions
{

    public class CoreHandler : AuthorizationHandler<CoreRequirement>
    {
        private readonly AppSetting _appSetting;

        public CoreHandler(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CoreRequirement requirement)
        {
            if (!context.User.HasClaim(m => m.Type == JwtRegisteredClaimNames.Sub) || !context.User.HasClaim(m => m.Type == JwtRegisteredClaimNames.Typ) || !context.User.HasClaim(m => m.Type == JwtRegisteredClaimNames.Jti))
            {
                context.Fail();
            }
            else
            {
                if (context.Requirements.Any(o => o is TwoFactorRequirement))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var type = Convert.ToInt32(context.User.FindFirst(JwtRegisteredClaimNames.Typ)?.Value);
                    if (type >= (int)requirement.UserLevel)
                    {
                        var id = context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                        var token = context.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                        var redisValue = RedisHelper.Get<string>($"{_appSetting.AppName}:Identity:Verify:{id}");
                        if (redisValue == token)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                        }
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }


}
