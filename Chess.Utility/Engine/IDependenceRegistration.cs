using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 依赖注入扩展接口
    /// </summary>
    public interface IDependenceRegistration
    {
        /// <summary>
        /// 注入顺序
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="container"></param>
        /// <param name="Assemblys"></param>
        void Register(Autofac.ContainerBuilder container, params Assembly[] Assemblys);
    }
}
