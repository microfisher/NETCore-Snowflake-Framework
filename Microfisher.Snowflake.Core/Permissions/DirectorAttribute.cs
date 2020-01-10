using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Core.Permissions
{

    public class DirectorAttribute : AuthorizeAttribute
    {
        public DirectorAttribute() : base("Director")
        {

        }
    }
}
