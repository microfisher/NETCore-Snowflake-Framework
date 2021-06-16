using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snowflake.Core.Dependency;

namespace Snowflake.Data.Repositories
{
    public interface IRepository<TEntity, TKey> : IDenpendency
    {
        IUnitOfWork UnitOfWork { get; set; }

        Task<long> Count();

        Task<long> Create(TEntity entity, bool isUseCustomId = false);

        Task<long> Update(object entity);

        Task<long> Delete(TKey id);

        Task<TEntity> Single(TKey id, bool isForUpdate = false);

        Task<bool> CleanAll();

        Task<List<TEntity>> List();

        Task<List<TEntity>> PageList(long pageIndex, long pageSize);

    }
}
