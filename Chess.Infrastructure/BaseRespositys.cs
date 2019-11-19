using Chess.Domain;
using Chess.Domain.UnitOfWork;
using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Chess.Infrastructure
{
    /// <summary>
    /// 泛型仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRespositys<TEntity> : IRespositys<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// EF工作单元接口
        /// </summary>
        public IEFUnitOfWork UnitOfWork { get; set; }

        #region ctor
        ///// <summary>
        ///// ctor
        ///// </summary>
        ///// <param name="unitOfWork"></param>
        //public BaseRespositys(IEFUnitOfWork unitOfWork)
        //{
        //    this.UnitOfWork = unitOfWork;
        //}
        #endregion

        /// <summary>
        /// 延迟加载泛型实体集
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return UnitOfWork.DataContext.Set<TEntity>();
            }
        }

        /// <summary>
        /// 延迟加载泛型实体集(无跟踪实体状态|无法更新)
        /// </summary>
        public IQueryable<TEntity> TableAsNoTrack
        {
            get
            {
                return Table.AsNoTracking() as IQueryable<TEntity>;
            }
        }

        /// <summary>
        /// 根据ID获取泛型实例
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity GetById(int Id)
        {
            return this.Table.FirstOrDefault(x => x.Id == Id);
        }

        /// <summary>
        /// 根据条件延迟加载泛型实体集
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> Query)
        {
            return this.Table.Where(Query);
        }

        /// <summary>
        /// 插入泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(TEntity model)
        {
            UnitOfWork.RegisterNew(model);
            return UnitOfWork.Commit();
        }

        /// <summary>
        /// 更新泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(TEntity model)
        {
            UnitOfWork.ResiterModifed(model);
            return UnitOfWork.Commit();
        }

        /// <summary>
        /// 删除泛型实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(TEntity model)
        {
            UnitOfWork.ResiterDelete(model);
            return UnitOfWork.Commit();
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            return UnitOfWork.DataContext.ExecuteSqlCommand(sql, doNotEnsureTransaction, timeout, parameters);
        }
    }
}
