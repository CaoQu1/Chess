using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chess
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            string contrller = "user", action = "register";
#if !DEBUG
            action="register";
#endif
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = contrller, action = action, id = UrlParameter.Optional }
            );
        }
    }
}
