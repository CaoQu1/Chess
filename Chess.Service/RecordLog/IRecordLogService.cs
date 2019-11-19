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
    /// 战绩记录服务接口
    /// </summary>
    public interface IRecordLogService
    {
        #region Method
        /// <summary>
        /// 插入战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(RecordLog model);

        /// <summary>
        /// 更新战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(RecordLog model);

        /// <summary>
        /// 删除战绩信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Delete(RecordLog model);

        /// <summary>
        /// 根据编号获取战绩信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        RecordLog GetById(int Id);

        /// <summary>
        /// 获取所有战绩信息
        /// </summary>
        /// <returns></returns>
        IQueryable<RecordLog> GetAllList();

        /// <summary>
        /// 根据条件查询战绩信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<RecordLog> GetByWhere(Expression<Func<RecordLog, bool>> expression);
        #endregion
    }
}
