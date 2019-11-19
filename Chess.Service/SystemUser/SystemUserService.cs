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
    /// 用户服务类
    /// </summary>
    public class SystemUserService : ISystemUserService
    {
        /// <summary>
        /// 用户数据仓储接口
        /// </summary>
        public IRespositys<SystemUser> _resposity { get; set; }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(SystemUser model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<SystemUser> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<SystemUser> GetAllListAsNoTrack()
        {
            return _resposity.TableAsNoTrack;
        }

        /// <summary>
        /// 根据编号获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SystemUser GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(SystemUser model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(SystemUser model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<SystemUser> GetByWhere(Expression<Func<SystemUser, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }
    }
}
