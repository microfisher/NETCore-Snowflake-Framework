using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microfisher.Snowflake.Core.Dependency;

namespace Microfisher.Snowflake.Core.Repositories
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

