using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chess.Web.Framework.Attibute
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="exceptionContext"></param>
        public void OnException(ExceptionContext exceptionContext)
        {
            try
            {
                exceptionContext.ExceptionHandled = true;
                LogHelper.Error(exceptionContext.Exception);
                var routeData = new RouteData();
                routeData.Values["controller"] = "Common";
                routeData.Values["action"] = "HttpException";
                routeData.Values["error"] = new CustomException(exceptionContext.Exception.Message, exceptionContext.Exception);
                var requestContext = exceptionContext.RequestContext;
                requestContext.RouteData = routeData;
                //var requestContext = new RequestContext(new HttpContextWrapper(), routeData);
                var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
                var controller = controllerFactory.CreateController(requestContext, "Common");
                controller.Execute(requestContext);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}
