using System;
using Snowflake.Core.Configurations;
using Snowflake.Services.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Snowflake.Services.Permissions
{
    public class CoreRequirement : IAuthorizationRequirement
    {
        public LevelEnum UserLevel { get; set; }

        public CoreRequirement(LevelEnum userLevel)
        {
            UserLevel = userLevel;
        }
    }
}
