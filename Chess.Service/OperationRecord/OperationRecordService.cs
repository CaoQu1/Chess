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
    /// 战绩操作记录服务类
    /// </summary>
    public class OperationRecordService : IOperationRecordService
    {
        /// <summary>
        /// 战绩操作记录仓储接口
        /// </summary>
        public IRespositys<OperationRecord> _resposity { get; set; }

        /// <summary>
        /// 删除操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(OperationRecord model)
        {
            return _resposity.Delete(model);
        }

        /// <summary>
        /// 获取所有操作信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<OperationRecord> GetAllList()
        {
            return _resposity.Table;
        }

        /// <summary>
        /// 根据编号获取操作信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OperationRecord GetById(int Id)
        {
            return _resposity.GetById(Id);
        }

        /// <summary>
        /// 插入操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(OperationRecord model)
        {
            return _resposity.Insert(model);
        }

        /// <summary>
        /// 更新操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(OperationRecord model)
        {
            return _resposity.Update(model);
        }

        /// <summary>
        /// 根据条件查询操作信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<OperationRecord> GetByWhere(Expression<Func<OperationRecord, bool>> expression)
        {
            return _resposity.GetByWhere(expression);
        }
    }
}
