using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Snowflake.Services.Permissions
{

    public class ManagerHandler : AuthorizationHandler<ManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRequirement requirement)
        {
            var type = Convert.ToInt32(context.User.FindFirst(JwtRegisteredClaimNames.Typ).Value);
            if (type >= (int)requirement.UserLevel)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
