using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRespositys<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 插入泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(TEntity model);

        /// <summary>
        /// 更新泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(TEntity model);

        /// <summary>
        /// 删除泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(TEntity model);

        /// <summary>
        /// 根据ID获取泛型实例
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntity GetById(int Id);

        /// <summary>
        /// 延迟加载泛型实体集
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 延迟加载泛型实体集(无跟踪实体状态|无法更新)
        /// </summary>
        IQueryable<TEntity> TableAsNoTrack { get; }

        /// <summary>
        /// 根据条件延迟加载泛型实体集
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> Query);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
    }
}
