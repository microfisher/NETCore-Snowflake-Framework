using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Core.Permissions
{

    public class ManagerAttribute : AuthorizeAttribute
    {
        public ManagerAttribute() : base("Manager")
        {

        }
    }
}
