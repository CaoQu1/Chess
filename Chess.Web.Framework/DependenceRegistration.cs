using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Chess.Domain;
using Chess.Service;
using Chess.Infrastructure;
using Chess.Domain.UnitOfWork;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace Chess.Web.Framework
{
    /// <summary>
    /// 依赖注入扩展类
    /// </summary>
    public class DependenceRegistration : IDependenceRegistration
    {
        /// <summary>
        /// 注入顺序
        /// </summary>
        public int Order
        {
            get { return 1; }
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        /// <param name="container"></param>
        /// <param name="assembies"></param>
        public void Register(ContainerBuilder container, params Assembly[] assembies)
        {
            #region 代码

            #region 构造函数注入
            //container.RegisterType<CacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            //container.RegisterType<WebContext>().As<IWebContext>().InstancePerLifetimeScope();
            //container.RegisterType<DataContext>().As<IDataContext>().InstancePerLifetimeScope();
            //container.RegisterType<EFUnitOfWork>().As<IEFUnitOfWork>().InstancePerLifetimeScope();
            //container.RegisterGeneric(typeof(BaseRespositys<>)).As(typeof(IRespositys<>));
            //container.RegisterControllers(assembies);
            //container.RegisterType<SystemUserService>().As<ISystemUserService>().InstancePerLifetimeScope();
            #endregion

            #region 属性注入
            container.RegisterType<EncryptionService>().As<IEncryptionService>().SingleInstance().PropertiesAutowired();
            container.RegisterType<CacheManager>().As<IMemoryCacheManager>().SingleInstance().PropertiesAutowired();
            container.RegisterType<CookieManager>().As<ICookieCacheManager>().SingleInstance().PropertiesAutowired();
            container.RegisterType<DataContext>().As<IDataContext>().PropertiesAutowired();
            container.RegisterType<EFUnitOfWork>().As<IEFUnitOfWork>().PropertiesAutowired();
            container.RegisterGeneric(typeof(BaseRespositys<>)).As(typeof(IRespositys<>)).PropertiesAutowired();
            container.RegisterType<WebContext>().As<IWebContext>().PropertiesAutowired();
            container.RegisterControllers(assembies).PropertiesAutowired();
            container.RegisterType<SystemUserService>().As<ISystemUserService>().InstancePerLifetimeScope().PropertiesAutowired();
            container.RegisterType<ClubService>().As<IClubService>().InstancePerLifetimeScope().PropertiesAutowired();
            container.RegisterType<PlatformService>().As<IPlatformService>().InstancePerLifetimeScope().PropertiesAutowired();
            container.RegisterType<ClubMemberService>().As<IClubMemberService>().InstancePerLifetimeScope().PropertiesAutowired();
            container.RegisterType<RecordLogService>().As<IRecordLogService>().InstancePerLifetimeScope().PropertiesAutowired();
            container.RegisterType<OperationRecordService>().As<IOperationRecordService>().InstancePerLifetimeScope().PropertiesAutowired();
            #endregion

            #endregion

            #region 配置文件
            //var config = new ConfigurationBuilder();
            //config.AddXmlFile("Config/autofac.config");
            //var module = new ConfigurationModule(config.Build());
            //container.RegisterModule(module);
            //container.RegisterControllers(assembies).PropertiesAutowired();
            #endregion
        }
    }
}
