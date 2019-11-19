using Chess.Domain;
using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Security;

namespace Chess.Web.Framework.Attibute
{
    /// <summary>
    /// 用户认证过滤器
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 跳转类型
        /// </summary>
        private RedirectResultType redirectType;

        /// <summary>
        /// ctor
        /// </summary>
        public UserAuthorizeAttribute()
        {
            this.redirectType = RedirectResultType.Redirect;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="result"></param>
        public UserAuthorizeAttribute(RedirectResultType result)
        {
            this.redirectType = result;
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            LogHelper.Info($"UserAuthorizeAttribute>>>>>>>,OnAuthorization,RUN TO Authorization CHECK");
            if (!IsAuthorize())
            {
                switch (this.redirectType)
                {
                    case RedirectResultType.Json:
                        filterContext.Result = new JsonResult
                        {
                            Data = "未登录!",
                            ContentType = "application/json"
                        };
                        break;
                    case RedirectResultType.Redirect:
                        var controller = filterContext.RouteData.Values["controller"];
                        var action = filterContext.RouteData.Values["action"];
                        var parameter = filterContext.HttpContext.Request.QueryString;
                        var clubId = string.Empty;
                        var roomId = string.Empty;
                        var returnUrl = string.Format("/{0}/{1}", controller, action);
                        LogHelper.Info($"UserAuthorizeAttribute>>>>>>>,OnAuthorization,controller=>{controller},action=>{action}");
                        if (parameter != null)
                        {
                            if (parameter["roomId"] != null && parameter["clubId"] != null)
                            {
                                roomId = parameter["roomId"].ToString();
                                clubId = parameter["clubId"].ToString();
                                returnUrl = HttpUtility.UrlEncode($"{returnUrl}?clubId={clubId}&roomId={roomId}");
                                LogHelper.Info($"UserAuthorizeAttribute>>>>>>>,OnAuthorization.returnUrl=>{returnUrl}");
                            }
                        }
                        LogHelper.Info($"UserAuthorizeAttribute>>>>>>>,OnAuthorization,controller=>{controller},action=>{action}");
                        filterContext.Result = new RedirectResult("/User/Register?returnUrl=" + returnUrl);
                        break;
                    default:
                        break;
                }
                return;
            }
            else
            {
                LogHelper.Info($"UserAuthorizeAttribute>>>>>>>,has Authorized");
            }
            //base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        private bool IsAuthorize()
        {
            var webContext = EngineContext.Current.Resove<IWebContext>();
            if (webContext.CurrentUser != null)
                LogHelper.Info($"webContext.CurrentUser.Id=>{webContext.CurrentUser.Id},webContext.InstanceId=>{webContext.InstanceId},current_user.AgentId=>{webContext.CurrentUser.AgentId}");
            return webContext != null && webContext.CurrentUser != null;
        }
    }

    /// <summary>
    /// 跳转类型
    /// </summary>
    public enum RedirectResultType
    {
        Json = 1,
        Redirect = 2
    }
}
