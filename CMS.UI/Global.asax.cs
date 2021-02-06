using AutoMapper;
using CMS.Services.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }

        protected void Application_Start()
        {

            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg => {
                cfg.AllowNullCollections = true;
                cfg.AddProfile<AutoMapperConfig>();
            });


        }
    }
}
