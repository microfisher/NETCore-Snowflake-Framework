using System;
using System.Threading.Tasks;
using Dapper;
using Microfisher.Snowflake.Core.Repositories;
using Microfisher.Snowflake.Data.Entities;

namespace Microfisher.Snowflake.Data.Extenstions
{
    public static class UserExtenstions
    {
        public static async Task<bool> IsUserExist(this IRepository<User, long> repository, string account)
        {
            var sql = $"SELECT Id FROM User WHERE Account = @Account;";
            var data = await repository.UnitOfWork.Connection.ExecuteScalarAsync<long>(sql, new { Account = account }, repository.UnitOfWork.Transaction);
            return data > 0;
        }

        public static async Task<User> GetUserByAccount(this IRepository<User, long> repository, string account)
        {
            var sql = $"SELECT Id,Level,Account,IsDisable,Password FROM User WHERE Account = @Account;";
            return await repository.UnitOfWork.Connection.QueryFirstOrDefaultAsync<User>(sql, new { Account = account }, repository.UnitOfWork.Transaction);
        }
    }
}
