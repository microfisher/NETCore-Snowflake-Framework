using System;
using Microsoft.AspNetCore.Authorization;

namespace Snowflake.Services.Permissions
{

    public class TwoFactorAttribute : AuthorizeAttribute
    {
        public TwoFactorAttribute() : base("TwoFactor")
        {

        }
    }
}
