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
    /// 战绩操作记录接口
    /// </summary>
    public interface IOperationRecordService
    {
        #region Method
        /// <summary>
        /// 插入操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(OperationRecord model);

        /// <summary>
        /// 更新操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(OperationRecord model);

        /// <summary>
        /// 删除操作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(OperationRecord model);

        /// <summary>
        /// 根据编号获取操作信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationRecord GetById(int Id);

        /// <summary>
        /// 获取所有操作信息
        /// </summary>
        /// <returns></returns>
        IQueryable<OperationRecord> GetAllList();

        /// <summary>
        /// 根据条件查询操作信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<OperationRecord> GetByWhere(Expression<Func<OperationRecord, bool>> expression);
        #endregion
    }
}
