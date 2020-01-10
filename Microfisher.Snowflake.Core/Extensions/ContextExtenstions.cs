using System;
using System.Security.Claims;
using Microfisher.Snowflake.Core.Utilities;

namespace Microfisher.Snowflake.Core.Extensions
{
    public static class ContextExtenstions
    {
        public static long GetUserId(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst("sub");
            if (claim == null || claim.Value == null)
            {
                ExceptionHelper.Throw("获取授权Id失败");
            }
            return Convert.ToInt64(claim.Value);
        }

        public static int GetUserLevel(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst("typ");
            if (claim == null || claim.Value == null)
            {
                ExceptionHelper.Throw("获取授权Level失败");
            }
            return Convert.ToInt32(claim.Value);
        }

        public static string GetUserToken(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst("jti");
            if (claim == null || claim.Value == null)
            {
                ExceptionHelper.Throw("获取授权Token失败");
            }
            return claim.Value;
        }
    }
}
