using Chess.Domain;
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
    /// 用户服务接口
    /// </summary>
    public interface ISystemUserService
    {
        #region Method
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(SystemUser model);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(SystemUser model);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(SystemUser model);

        /// <summary>
        /// 根据编号获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        SystemUser GetById(int Id);

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        IQueryable<SystemUser> GetAllList();

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        IQueryable<SystemUser> GetAllListAsNoTrack();

        /// <summary>
        /// 根据条件查询用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<SystemUser> GetByWhere(Expression<Func<SystemUser, bool>> expression);
        #endregion
    }
}
