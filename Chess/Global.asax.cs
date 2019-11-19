using Chess.Utility;
using Chess.Web.Framework;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Chess
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        ///应用程序启动
        /// </summary>
        protected void Application_Start()
        {
            EngineContext.Init(false);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var isGLobalDebug = true;
            //全局设置参数，将被储存到 Senparc.CO2NET.Config.SenparcSetting
            //设置redis、mongo、namespace、cachename、isdebug
            var senparcSetting = SenparcSetting.BuildFromWebConfig(isGLobalDebug);
            //也可以通过这种方法在程序任意位置设置全局 Debug 状态：
            //Senparc.CO2NET.Config.IsDebug = isGLobalDebug;


            //CO2NET 全局注册，必须！！
            IRegisterService register = RegisterService.Start(senparcSetting).UseSenparcGlobal();//启动线程读取队列数据、查找所有缓存类型实例化


            var isWeixinDebug = true;
            //全局设置参数，将被储存到 Senparc.Weixin.Config.SenparcWeixinSetting
            //设置微信相关配置
            var senparcWeixinSetting = SenparcWeixinSetting.BuildFromWebConfig(isWeixinDebug);
            //也可以通过这种方法在程序任意位置设置微信的 Debug 状态：
            //Senparc.Weixin.Config.IsDebug = isWeixinDebug;

            //微信全局注册，必须！！
            //查找所有缓存类型实例化、添加以api|apis结尾的程序集中所有方法和特性为apibind到apibindCollection
            //注册appid、appsercrent到缓存中
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting)   //注册公众号（可注册多个）                                         
                .RegisterMpAccount(senparcWeixinSetting, "分享平台");// PDBMARK_END

            //日志初始化
            register.RegisterTraceLog(TraceLog);
        }

        /// <summary>
        /// 应用程序发送错误
        /// </summary>
        protected void Application_Error()
        {
            var error = Server.GetLastError();
            if (error != null)
            {
                LogHelper.Error("Global.asax>>>>>>>,进入全局错误~");
                Server.ClearError();
                var routeData = new RouteValueDictionary();
                routeData["controller"] = "Common";
                routeData["action"] = "HttpException";
                routeData["error"] = new CustomException(error.Message, error);
                LogHelper.Error(error);
                Response.RedirectToRoute(routeData);
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        private void TraceLog()
        {
            Senparc.Weixin.WeixinTrace.SendCustomLog("全局日志", "日志开始");
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {

            };

            Senparc.Weixin.WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                var email = EmailConfig.Instance;
                email.body = string.Format(email.body, ex.Message);
                EmailHelper.SendMailAsync(email);
            };
        }
    }
}
