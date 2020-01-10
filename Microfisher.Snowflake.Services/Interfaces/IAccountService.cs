using System;
using System.Threading.Tasks;
using Microfisher.Snowflake.Core.Responses;
using Microfisher.Snowflake.Services.Dtos;
using Microfisher.Snowflake.Core.Dependency;

namespace Microfisher.Snowflake.Services.Interfaces
{
    public interface IAccountService : IDenpendency
    {
        Task<IResultObject> SignIn(SignInDto dto);

        Task<IResultObject> SignUp(SignUpDto dto);

        Task<IResultObject> SignOut();

        Task<IResultObject> GetJwtToken();
    }
}
