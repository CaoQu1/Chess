using Chess.Domain;
using Chess.Domain.UnitOfWork;
using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure
{
    /// <summary>
    /// EF工作单元
    /// </summary>
    public class EFUnitOfWork : IEFUnitOfWork
    {
        ///// <summary>
        ///// 数据库上下文接口
        ///// </summary>
        //public readonly IDataContext _dataContext;

        #region ctor
        ///// <summary>
        ///// ctor
        ///// </summary>
        ///// <param name="dataContext"></param>
        //public EFUnitOfWork(IDataContext dataContext)
        //{
        //    this._dataContext = dataContext;
        //}
        #endregion

        /// <summary>
        /// 获取数据库上下文实例
        /// </summary>
        public IDataContext DataContext
        {
            get; set;
        }

        /// <summary>
        /// 是否提交
        /// </summary>
        public bool IsCommitted
        {
            get; set;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {
                int result = DataContext.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                throw e;
            }
            //finally
            //{
            //    this.DataContext.Dispose();
            //}
        }

        /// <summary>
        /// 注册新实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        public void RegisterNew<TEntity>(TEntity model) where TEntity : BaseEntity
        {
            var state = DataContext.Entry(model).State;
            if (state == System.Data.Entity.EntityState.Detached)
            {
                DataContext.Entry(model).State = System.Data.Entity.EntityState.Added;
            }
            IsCommitted = false;
        }

        /// <summary>
        /// 删除实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        public void ResiterDelete<TEntity>(TEntity model) where TEntity : BaseEntity
        {
            DataContext.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        /// 修改实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="model"></param>
        public void ResiterModifed<TEntity>(TEntity model) where TEntity : BaseEntity
        {
            DataContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            IsCommitted = false;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

    }
}
