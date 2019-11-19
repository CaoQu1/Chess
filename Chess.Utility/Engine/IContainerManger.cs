using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 注入容器管理接口
    /// </summary>
    public interface IContainerManger
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resove<T>();

        /// <summary>
        /// 容器接口
        /// </summary>
        IContainer Container { get; }
    }
}
