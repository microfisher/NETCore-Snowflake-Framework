using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Snowflake.Core.Configurations;
using Snowflake.Data.Repositories;
using Microsoft.Extensions.Options;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Snowflake.Data
{
    /// <summary>
    /// 事务工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 系统配置文件
        /// </summary>
        private readonly AppSetting _appSettings;

        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据连接
        /// </summary>
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
        /// 同步提交事务
        /// </summary>
        public void Commit()
        {
            Transaction.Commit();
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
        /// 异步启动事务
        /// </summary>
        public async Task BeginAsync()
        {
            Transaction = await Connection.BeginTransactionAsync();
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
            Dispose();
        }

        /// <summary>
        /// 异步回滚事务
        /// </summary>
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
