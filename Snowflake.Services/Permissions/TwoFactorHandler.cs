using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace Snowflake.Services.Permissions
{

    public class TwoFactorHandler : AuthorizationHandler<TwoFactorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TwoFactorRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
