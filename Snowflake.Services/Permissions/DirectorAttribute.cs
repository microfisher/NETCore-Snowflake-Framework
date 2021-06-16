using System;
using Microsoft.AspNetCore.Authorization;

namespace Snowflake.Services.Permissions
{

    public class DirectorAttribute : AuthorizeAttribute
    {
        public DirectorAttribute() : base("Director")
        {

        }
    }
}
