using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.UnitOfWork
{
    /// <summary>
    /// EF工作单元接口
    /// </summary>
    public interface IEFUnitOfWork : IUnitOfWorkRepositoryContext
    {
        /// <summary>
        /// 数据库上下文接口
        /// </summary>
        IDataContext DataContext { get; }
    }
}
