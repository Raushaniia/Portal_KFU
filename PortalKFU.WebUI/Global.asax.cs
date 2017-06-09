using System.Web.Mvc;
using System.Web.Routing;
using PortalKFU.Domain.Entities;
using PortalKFU.WebUI.Infrastructure.Binders;

namespace PortalKFU.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Library), new LibraryModelBinder());
        }
    }
}