using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 注入容器管理接口
        /// </summary>
        IContainerManger Container { get; }
    }
}
