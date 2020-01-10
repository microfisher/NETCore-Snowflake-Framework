using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Core.Permissions
{

    public class SupervisorAttribute : AuthorizeAttribute
    {
        public SupervisorAttribute() : base("Supervisor")
        {

        }
    }
}
