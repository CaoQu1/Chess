using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain
{
    /// <summary>
    /// 仓储工作单元接口
    /// </summary>
    public interface IUnitOfWorkRepositoryContext : IUnitOfWork
    {
        /// <summary>
        /// 注册新实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        void RegisterNew<TEntity>(TEntity model) where TEntity : BaseEntity;

        /// <summary>
        /// 修改实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        void ResiterModifed<TEntity>(TEntity model) where TEntity : BaseEntity;

        /// <summary>
        /// 删除实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        void ResiterDelete<TEntity>(TEntity model) where TEntity : BaseEntity;
    }
}
