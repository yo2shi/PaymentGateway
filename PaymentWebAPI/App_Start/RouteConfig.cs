using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PaymentWebAPI.Logger;

namespace PaymentWebAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void AppendToLog(string line)
        {
            try
            {
                DefaultLogger logger = new DefaultLogger();
                logger.AppendToLog(line);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                System.Diagnostics.Debug.Write(exp.ToString());
            }

        }

    }
}
