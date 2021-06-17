using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snowflake.Core.Dependency;

namespace Snowflake.Data.Interfaces
{
    public interface IRepository<TEntity, TKey> : IScopeDependency
    {
        IUnitOfWork UnitOfWork { get; set; }

        Task<long> CountAsync();

        Task<long> CreateAsync(TEntity entity, bool isUseCustomId = false);

        Task<long> UpdateAsync(object entity);

        Task<long> DeleteAsync(TKey id);

        Task<TEntity> SingleAsync(TKey id, bool isForUpdate = false);

        Task<bool> CleanAsync();

        Task<List<TEntity>> AllAsync();

        Task<List<TEntity>> PageListAsync(long pageIndex, long pageSize);

    }
}
