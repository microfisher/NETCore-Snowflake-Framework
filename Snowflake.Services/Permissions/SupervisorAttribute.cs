using System;
using Microsoft.AspNetCore.Authorization;

namespace Snowflake.Services.Permissions
{

    public class SupervisorAttribute : AuthorizeAttribute
    {
        public SupervisorAttribute() : base("Supervisor")
        {

        }
    }
}
