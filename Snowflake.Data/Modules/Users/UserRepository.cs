using System;
using System.Threading.Tasks;
using Dapper;
using Snowflake.Data.Repositories;
using Snowflake.Data.Entities;

namespace Snowflake.Data.Modules.Users
{
    public static class UserRepository
    {
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static async Task<bool> IsUserExistAsync(this IRepository<User, long> repository, string account)
        {
            var sql = $"SELECT Id FROM User WHERE Account = @Account;";
            var data = await repository.UnitOfWork.Connection.ExecuteScalarAsync<long>(sql, new { Account = account }, repository.UnitOfWork.Transaction);
            return data > 0;
        }

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static async Task<User> GetUserByAccountAsync(this IRepository<User, long> repository, string account)
        {
            var sql = $"SELECT Id,Level,Account,IsDisable,Password FROM User WHERE Account = @Account;";
            return await repository.UnitOfWork.Connection.QueryFirstOrDefaultAsync<User>(sql, new { Account = account }, repository.UnitOfWork.Transaction);
        }
    }
}
