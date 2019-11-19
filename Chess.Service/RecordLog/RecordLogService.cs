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
    /// 战绩记录服务类
    /// </summary>
    public class RecordLogService : IRecordLogService
    {
        /// <summary>
        /// 战绩数据仓储接口
        /// </summary>
        public IRespositys<RecordLog> _resposity { get; set; }

        /// <summary>
        /// 删除战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(RecordLog model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有战绩信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<RecordLog> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 根据编号获取战绩信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RecordLog GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(RecordLog model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(RecordLog model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询战绩信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<RecordLog> GetByWhere(Expression<Func<RecordLog, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }
    }
}
