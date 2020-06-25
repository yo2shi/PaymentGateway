using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PaymentWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            PaymentWebAPI.Logger.DefaultLogger logger = new Logger.DefaultLogger();

            logger.AppendToLog("Run Started on : " + System.DateTime.Now.ToString("dd/MM/yyyy") + "   at : " + System.DateTime.Now.ToString("HH:mm:ss"));
            logger.AppendToLog("Global.asax loading...");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
