

using System.Threading.Tasks;
using Snowflake.Core.Responses;
using Snowflake.Services.Dtos;
using Snowflake.Core.Dependency;

namespace Snowflake.Services.Modules.Settings
{
    public interface ISettingManager : IScopeDependency
    {
        /// <summary>
        /// 做点啥吧
        /// </summary>
        Task<IResultObject> DoSomething(dynamic dto);
    }
}