using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Services.Permissions
{

    public class SupervisorAttribute : AuthorizeAttribute
    {
        public SupervisorAttribute() : base("Supervisor")
        {

        }
    }
}
