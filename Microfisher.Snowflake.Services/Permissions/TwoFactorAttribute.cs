using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Services.Permissions
{

    public class TwoFactorAttribute : AuthorizeAttribute
    {
        public TwoFactorAttribute() : base("TwoFactor")
        {

        }
    }
}
