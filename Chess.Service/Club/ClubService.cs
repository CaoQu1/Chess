using Chess.Domain;
using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Service
{
    /// <summary>
    /// 俱乐部服务类
    /// </summary>
    public class ClubService : IClubService
    {
        /// <summary>
        /// 俱乐部数据仓储接口
        /// </summary>
        public IRespositys<Club> _resposity { get; set; }

        /// <summary>
        /// 删除俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Club model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有俱乐部信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<Club> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 根据编号获取俱乐部信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Club GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Club model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新俱乐部信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Club model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询成员信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<Club> GetByWhere(Expression<Func<Club, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }

        /// <summary>
        /// 删除俱乐部和俱乐部成员
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns></returns>
        public int Delete(int? clubId = null)
        {
            StringBuilder stringBuilder = new StringBuilder("delete ClubMembers");
            if (clubId.HasValue && clubId.Value != 0)
            {
                stringBuilder.Append(" where ClubId =@id");
            }
            stringBuilder.Append(";delete Clubs");
            if (clubId.HasValue && clubId.Value != 0)
            {
                stringBuilder.Append(" where ClubId=@id");
            }
            return _resposity.ExecuteSqlCommand(stringBuilder.ToString(), parameters: new SqlParameter("@id", clubId.Value));
        }
    }
}
