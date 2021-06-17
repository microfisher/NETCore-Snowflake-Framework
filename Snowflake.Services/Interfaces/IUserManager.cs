using System;
using System.Threading.Tasks;
using Snowflake.Core.Responses;
using Snowflake.Services.Dtos;
using Snowflake.Core.Dependency;

namespace Snowflake.Services.Interfaces
{
    public interface IUserManager : IScopeDependency
    {
        Task<IResultObject> SignIn(SignInDto dto);

        Task<IResultObject> SignUp(SignUpDto dto);

        Task<IResultObject> SignOut();

        Task<IResultObject> GetJwtToken();
    }
}
