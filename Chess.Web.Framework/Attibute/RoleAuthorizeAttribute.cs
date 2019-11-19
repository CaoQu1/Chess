using Chess.Domain;
using Chess.DtoModel;
using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Chess.Web.Framework.Attibute
{
    /// <summary>
    /// 角色认证过滤器
    /// </summary>
    public class RoleAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 角色
        /// </summary>
        public EnumRole Role { get; set; }

        public RedirectResultType ResultType { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public RoleAuthorizeAttribute()
        {
            this.Role = EnumRole.Member;
            this.ResultType = RedirectResultType.Redirect;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public RoleAuthorizeAttribute(EnumRole role)
        {
            this.Role = role;
            this.ResultType = RedirectResultType.Redirect;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="role"></param>
        /// <param name="redirectResultType"></param>
        public RoleAuthorizeAttribute(EnumRole role, RedirectResultType redirectResultType)
        {
            this.Role = role;
            this.ResultType = redirectResultType;
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            if (!IsAuthorization())
            {
                switch (this.ResultType)
                {
                    case RedirectResultType.Json:
                        filterContext.Result = new JsonResult
                        {
                            Data = "权限不足!",
                            ContentType = "application/json",
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        break;
                    case RedirectResultType.Redirect:
                        filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        private bool IsAuthorization()
        {
            var webContext = EngineContext.Current.Resove<IWebContext>();
            var isAuthorization = webContext.CurrentUser != null && this.Role == webContext.CurrentUser.UserIdentity;
            if (webContext.CurrentUser != null)
                LogHelper.Info($"RoleAuthorizeAttribute>>>>>>>>>>>,IsAuthorization=>{isAuthorization},webContext.CurrentUser.UserIdentity=>{webContext.CurrentUser.UserIdentity.ToString()},webContext.CurrentUser.AgentId=>{webContext.CurrentUser.AgentId},webContext.CurrentUser.Id=>{webContext.CurrentUser.Id}");
            return isAuthorization;
        }
    }
}
