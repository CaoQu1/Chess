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
    /// 俱乐部成员服务接口
    /// </summary>
    public interface IClubMemberService
    {
        #region Method
        /// <summary>
        /// 插入成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(ClubMember model);

        /// <summary>
        /// 更新成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(ClubMember model);

        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(ClubMember model);

        /// <summary>
        /// 根据编号获取成员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ClubMember GetById(int Id);

        /// <summary>
        /// 获取所有成员信息
        /// </summary>
        /// <returns></returns>
        IQueryable<ClubMember> GetAllList();

        /// <summary>
        /// 根据条件查询成员信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<ClubMember> GetByWhere(Expression<Func<ClubMember, bool>> expression);
        #endregion
    }
}
