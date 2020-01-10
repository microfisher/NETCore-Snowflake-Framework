using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Core.Permissions
{

    public class TwoFactorAttribute : AuthorizeAttribute
    {
        public TwoFactorAttribute() : base("TwoFactor")
        {

        }
    }
}
