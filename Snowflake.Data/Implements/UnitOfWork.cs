using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Snowflake.Core.Configurations;
using Snowflake.Data.Interfaces;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Snowflake.Data.Implements
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppSetting _appSettings;

        public DbTransaction Transaction { get; set; }

        public DbConnection Connection { get; set; }

        public UnitOfWork(IOptions<AppSetting> appSettings)
        {
            _appSettings = appSettings.Value;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            Connection = new MySqlConnection(_appSettings.Database.ConnectionString);
            Connection.Open();
        }

        public UnitOfWork(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        /// <summary>
        /// 同步启动事务
        /// </summary>
        public void Begin()
        {
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// 异步启动事务
        /// </summary>
        /// <returns></returns>
        public async Task BeginAsync()
        {
            Transaction = await Connection.BeginTransactionAsync();
        }

        /// <summary>
        /// 同步提交事务
        /// </summary>
        public void Commit()
        {
            Transaction.Commit();
            Dispose();
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
            Dispose();
        }

        /// <summary>
        /// 同步回滚事务
        /// </summary>
        public void Rollback()
        {
            Transaction.Rollback();
            Dispose();
        }

        /// <summary>
        /// 异步回滚事务
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
            Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
            Transaction?.Dispose();
            Transaction = null;
        }

    }
}
