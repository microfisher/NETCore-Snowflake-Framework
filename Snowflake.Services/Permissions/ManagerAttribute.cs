using System;
using Microsoft.AspNetCore.Authorization;

namespace Snowflake.Services.Permissions
{

    public class ManagerAttribute : AuthorizeAttribute
    {
        public ManagerAttribute() : base("Manager")
        {

        }
    }
}
