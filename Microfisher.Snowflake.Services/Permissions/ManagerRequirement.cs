using System;
using Microfisher.Snowflake.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Microfisher.Snowflake.Services.Permissions
{
    public class ManagerRequirement : IAuthorizationRequirement
    {
        public LevelEnum UserLevel { get; set; }

        public ManagerRequirement(LevelEnum userLevel)
        {
            UserLevel = userLevel;
        }
    }
}
