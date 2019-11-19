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
    /// 俱乐部服务接口
    /// </summary>
    public interface IClubService
    {
        #region Method
        /// <summary>
        /// 插入俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(Club model);

        /// <summary>
        /// 更新俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(Club model);

        /// <summary>
        /// 删除俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(Club model);

        /// <summary>
        /// 删除俱乐部成员
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Delete(int? clubId = null);

        /// <summary>
        /// 根据编号获取俱乐部信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Club GetById(int Id);

        /// <summary>
        /// 获取所有俱乐部信息
        /// </summary>
        /// <returns></returns>
        IQueryable<Club> GetAllList();

        /// <summary>
        /// 根据条件查询操作信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<Club> GetByWhere(Expression<Func<Club, bool>> expression);
        #endregion
    }
}
