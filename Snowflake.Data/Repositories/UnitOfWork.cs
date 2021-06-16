using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Snowflake.Core.Configurations;
using Snowflake.Data.Repositories;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Snowflake.Data.Repositories
{
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

        public void Begin()
        {
            Transaction = Connection.BeginTransaction();
        }

        public async Task BeginAsync()
        {
            Transaction = await Connection.BeginTransactionAsync();
        }

        public void Commit()
        {
            Transaction.Commit();
            Dispose();
        }

        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
            Dispose();
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Dispose();
        }

        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
            Dispose();
        }

        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
            Transaction?.Dispose();
            Transaction = null;
        }

    }
}
