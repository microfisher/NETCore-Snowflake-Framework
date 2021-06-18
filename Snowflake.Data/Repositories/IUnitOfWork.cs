using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Snowflake.Core.Dependency;

namespace Snowflake.Data.Repositories
{
    /// <summary>
    /// 事务工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable, IScopeDependency
    {
        /// <summary>
        /// 事务对象
        /// </summary>
        DbTransaction Transaction { get; set; }

        /// <summary>
        /// 数据连接
        /// </summary>
        DbConnection Connection { get; set; }

        /// <summary>
        /// 同步启动事务
        /// </summary>
        void Begin();

        /// <summary>
        /// 同步提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 同步回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 异步启动事务
        /// </summary>
        Task BeginAsync();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// 异步回滚事务
        /// </summary>
        Task RollbackAsync();

    }
}

