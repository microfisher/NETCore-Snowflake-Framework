using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Snowflake.Core.Dependency;

namespace Snowflake.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable, IDenpendency
    {
        DbTransaction Transaction { get; set; }

        DbConnection Connection { get; set; }

        void Begin();

        void Commit();

        void Rollback();

        Task BeginAsync();

        Task CommitAsync();

        Task RollbackAsync();

    }
}

