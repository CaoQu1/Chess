using Chess.Web.Framework.Attibute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Chess.Web.Framework.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [UserAuthorize]
    [Compression]
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取模型绑定错误
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var errors = ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors);
            if (errors.Count() > 0)
            {
                var methodInfos = filterContext.Controller.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                var methodInfo = methodInfos.FirstOrDefault(x => x.Name.ToLower() == filterContext.ActionDescriptor.ActionName.ToLower() && x.GetParameters().Count() == filterContext.ActionParameters.Count);
                if (methodInfo != null)
                {
                    if (methodInfo.ReturnType == typeof(JsonResult))
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = string.Join(",", errors.Select(x => x.ErrorMessage)),
                            ContentType = "application/json"
                        };
                    }
                    else if (methodInfo.ReturnType == typeof(ActionResult) || methodInfo.ReturnType == typeof(ContentResult))
                    {
                        filterContext.Result = new ContentResult
                        {
                            Content = string.Join(",", errors.Select(x => x.ErrorMessage)),
                        };
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
