using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Chess.DtoModel;
using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Chess.Web.Framework
{
    public class Engine : IEngine
    {
        /// <summary>
        /// 依赖容器管理
        /// </summary>
        private IContainerManger _container;

        /// <summary>
        /// 依赖注入
        /// </summary>
        private void RegisterDependencies()
        {
            Autofac.ContainerBuilder container_builder = new Autofac.ContainerBuilder();

            container_builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            IDependenceRegistration register = new DependenceRegistration();
            var assembies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0].Equals("Chess")).ToArray();

            register.Register(container_builder, assembies);
            var container = container_builder.Build();
            _container = new ContainerManger(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// AutoMapper配置
        /// </summary>
        private void Configure()
        {
            Func<Type, bool> func = t => t.BaseType != null && t.BaseType == typeof(Profile) && t.FullName.EndsWith("Profile");
            Mapper.Initialize(c =>
            {
                //var assemblys = AppDomain.CurrentDomain.GetAssemblies()
                //   .Where(x => x.ExportedTypes.Where(func).Any()).ToList();
                //assemblys.ForEach(t =>
                //{
                //    t.GetTypes().Where(func).ToList().ForEach(p =>
                //    {
                //        c.AddProfile(p);
                //    });
                //});
                c.AddProfile(typeof(ClubProfile));
                c.CreateMap<string, int>().ConvertUsing<ValueTypeConverter>();

            });

            Mapper.AssertConfigurationIsValid();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            //初始化日志
            LogHelper.Init();

            //注册依赖
            RegisterDependencies();

            //AutoMapper启动
            Configure();
        }

        /// <summary>
        /// 获取容器管理
        /// </summary>
        public IContainerManger Container
        {
            get
            {
                return _container;
            }
        }

        /// <summary>
        /// 获取类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resove<T>()
        {
            return this.Container.Resove<T>();
        }
    }
}
