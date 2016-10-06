using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ToDo.Presentation.Bootstrapper;

namespace ToDo.Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityWebActivator.Start();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            UnityWebActivator.Shutdown();
        }
    }
}