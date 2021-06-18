

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Snowflake.Data.Entities;
using Snowflake.Core.Responses;
using Snowflake.Data.Repositories;
using Snowflake.Core.Configurations;

namespace Snowflake.Services.Modules.Settings
{
    /// <summary>
    /// Setting服务
    /// </summary>
    public class SettingManager : ISettingManager
    {
        private readonly AppSetting _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SettingManager> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Setting, long> _settingRepository;

        public SettingManager(IOptions<AppSetting> appSettings, ILogger<SettingManager> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IRepository<Setting, long> settingRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _settingRepository = settingRepository;
        }
        
        /// <summary>
        /// 做点啥吧
        /// </summary>
        public Task<IResultObject> DoSomething(dynamic dto)
        {
            throw new System.NotImplementedException();
        }
    }
}

