using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain
{
    /// <summary>
    /// 工资单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 是否提交
        /// </summary>
        bool IsCommitted { get; set; }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
    }
}
