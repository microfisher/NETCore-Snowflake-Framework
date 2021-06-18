using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Snowflake.Core.Dependency;

namespace Snowflake.Data.Repositories
{
    /// <summary>
    /// 数据仓储
    /// </summary>
    /// <typeparam name="TEntity">数据实体对象</typeparam>
    /// <typeparam name="TKey">数据主键类型</typeparam>
    public interface IRepository<TEntity, TKey> : IScopeDependency
    {
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="entity">数据库实体对象</param>
        /// <returns>主键ID</returns>
        Task<long> CreateAsync(TEntity entity, bool isUseCustomId = false);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="entity">实体对象（必须包含Id及一个需要更新的属性字段，字段名必须与数据库一致）</param>
        /// <returns>受影响的行数</returns>
        Task<long> UpdateAsync(object entity);

        /// <summary>
        /// 计算总行数
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <returns>总行数</returns>
        Task<long> GetCountAsync();

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="id">主键Id</param>
        /// <returns>实体对象</returns>
        Task<TEntity> GetSingleAsync(TKey id, bool isForUpdate = false);

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="id">主键Id</param>
        /// <returns>受影响行数</returns>
        Task<long> DeleteAsync(TKey id);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <returns>实体集合</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 清空数据表
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <returns>是否成功</returns>
        Task<bool> CleanAsync();

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <returns>是否存在</returns>
        Task<bool> IsTableExistAsync();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> GetPageListAsync(long pageIndex, long pageSize);

    }
}
