using System;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Services.Permissions
{

    public class DirectorAttribute : AuthorizeAttribute
    {
        public DirectorAttribute() : base("Director")
        {

        }
    }
}
