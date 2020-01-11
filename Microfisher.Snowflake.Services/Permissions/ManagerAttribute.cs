using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Services.Permissions
{

    public class ManagerAttribute : AuthorizeAttribute
    {
        public ManagerAttribute() : base("Manager")
        {

        }
    }
}
