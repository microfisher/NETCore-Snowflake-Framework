using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microfisher.Snowflake.Core.Repositories;
using Microfisher.Snowflake.Core.Utilities;

namespace Microfisher.Snowflake.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        /// <summary>
        /// 数据连接上下文
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="entity">数据库实体对象</param>
        /// <returns>主键ID</returns>
        public virtual async Task<long> Create(TEntity entity, bool isUseCustomId = false)
        {
            var tableName = entity.GetType().Name;
            var parameters = DataHelper.GetParamNames(entity);
            if (isUseCustomId == false)
            {
                parameters.Remove("Id");
            }
            var columns = string.Join(",", parameters);
            var values = string.Join(",", parameters.Select(p => "@" + p));
            var sql = $"INSERT {tableName} ({columns}) VALUES ({values});SELECT LAST_INSERT_ID() AS id";

            return await UnitOfWork.Connection.ExecuteScalarAsync<long>(sql, entity, UnitOfWork.Transaction);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="entity">实体对象（必须包含Id及一个需要更新的属性字段，字段名必须与数据库一致）</param>
        /// <returns>受影响的行数</returns>
        public virtual async Task<long> Update(object entity)
        {
            var parameters = DataHelper.GetParamNames(entity);
            if (!parameters.Any(m => m.ToLower().Equals("id")))
            {
                ExceptionHelper.Throw("更新数据必须传入实体Id");
            }
            var builder = new StringBuilder();
            builder.Append("UPDATE ").Append(typeof(TEntity).Name).Append(" SET ");
            builder.AppendLine(string.Join(",", parameters.Where(n => n != "Id").Select(p => p + "= @" + p)));
            builder.Append("WHERE Id = @Id");

            return await UnitOfWork.Connection.ExecuteAsync(builder.ToString(), entity, UnitOfWork.Transaction);
        }

        /// <summary>
        /// 计算总行数
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <returns>总行数</returns>
        public virtual async Task<long> Count()
        {
            var sql = $"SELECT COUNT(*) FROM {typeof(TEntity).Name};";
            return await UnitOfWork.Connection.ExecuteScalarAsync<long>(sql, new { }, UnitOfWork.Transaction);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="id">主键Id</param>
        /// <returns>实体对象</returns>
        public virtual async Task<TEntity> Single(TKey id, bool isForUpdate = false)
        {
            var sql = $"SELECT * FROM {typeof(TEntity).Name} WHERE Id = @Id {(isForUpdate ? " FOR UPDATE " : "")};";
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id }, UnitOfWork.Transaction);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="id">主键Id</param>
        /// <returns>受影响行数</returns>
        public virtual async Task<long> Delete(TKey id)
        {
            var sql = $"DELETE FROM {typeof(TEntity).Name} WHERE Id = @Id;";
            return await UnitOfWork.Connection.ExecuteAsync(sql, new { Id = id }, UnitOfWork.Transaction);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> List()
        {
            var sql = $"SELECT * FROM {typeof(TEntity).Name};";
            var data = await UnitOfWork.Connection.QueryAsync<TEntity>(sql, null, UnitOfWork.Transaction);
            return data.AsList();
        }

        /// <summary>
        /// 清空数据表
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <returns>是否成功</returns>
        public async Task<bool> CleanAll()
        {
            var sql = $"TRUNCATE {typeof(TEntity).Name};";
            var data = await UnitOfWork.Connection.ExecuteAsync(sql, null, UnitOfWork.Transaction);
            return data == 0;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TEntity">数据库实体类</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数</param>
        /// <returns>数据列表</returns>
        public virtual async Task<List<TEntity>> PageList(long pageIndex, long pageSize)
        {
            var sql = $"SELECT * FROM {typeof(TEntity).Name} a INNER JOIN (SELECT Id FROM {typeof(TEntity).Name} ORDER BY ID DESC LIMIT @PageIndex,@PageSize) b ON a.Id=b.Id;";
            var data = await UnitOfWork.Connection.QueryAsync<TEntity>(sql, new { PageIndex = ((pageIndex - 1) * pageSize), PageSize = pageSize }, UnitOfWork.Transaction);
            return data.AsList();
        }


    }
}
