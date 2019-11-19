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
    /// 俱乐部成员服务类
    /// </summary>
    public class ClubMemberService : IClubMemberService
    {
        /// <summary>
        ///俱乐部成员数据仓储接口
        /// </summary>
        public IRespositys<ClubMember> _resposity { get; set; }

        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(ClubMember model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有成员信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClubMember> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 根据编号获取成员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ClubMember GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ClubMember model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(ClubMember model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询成员信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<ClubMember> GetByWhere(Expression<Func<ClubMember, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }
    }
}
