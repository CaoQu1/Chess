using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Service
{
    /// <summary>
    /// 平台服务接口
    /// </summary>
    public interface IPlatformService
    {
        #region Method
        /// <summary>
        /// 插入平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(Platform model);

        /// <summary>
        /// 更新平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(Platform model);

        /// <summary>
        /// 删除平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(Platform model);

        /// <summary>
        /// 根据编号获取平台信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Platform GetById(int Id);

        /// <summary>
        /// 获取所有平台信息
        /// </summary>
        /// <returns></returns>
        IQueryable<Platform> GetAllList();

        /// <summary>
        /// 根据条件查询平台信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<Platform> GetByWhere(Expression<Func<Platform, bool>> expression);
        #endregion
    }
}
