using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using Chess.Utility;

namespace Chess.Web.Framework
{
    /// <summary>
    /// 注入容器管理类
    /// </summary>
    public class ContainerManger : IContainerManger
    {
        /// <summary>
        /// 容器
        /// </summary>
        private IContainer _container;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="container"></param>
        public ContainerManger(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// 获取容器实例
        /// </summary>
        public IContainer Container
        {
            get
            {
                return _container;
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resove<T>()
        {
            //ILifetimeScope lifetimesocpe = null;
            //if (HttpContext.Current != null)
            //{
            //    lifetimesocpe = AutofacDependencyResolver.Current.RequestLifetimeScope;
            //}
            //else
            //{
            //    lifetimesocpe = this.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            //}
            return this.Container.Resolve<T>();
        }
    }
}
