using System;
using Microfisher.Snowflake.Core.Configurations;
using Microfisher.Snowflake.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Microfisher.Snowflake.Services.Permissions
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
