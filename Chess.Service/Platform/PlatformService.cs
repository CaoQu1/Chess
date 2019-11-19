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
    /// 平台服务类
    /// </summary>
    public class PlatformService : IPlatformService
    {
        /// <summary>
        /// 平台数据仓储接口
        /// </summary>
        public IRespositys<Platform> _resposity { get; set; }

        /// <summary>
        /// 删除平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Platform model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有平台信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<Platform> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 根据编号获取平台信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Platform GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Platform model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Platform model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<Platform> GetByWhere(Expression<Func<Platform, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }
    }
}
