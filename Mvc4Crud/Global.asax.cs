using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using WebMatrix.Data;
using System.Data.Entity;
using Mvc4Crud.Models;

namespace Mvc4Crud
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //custom added function
            //DataBase.SetInitializer<ApplicationDbContext>(null);
            //Database.SetInitializer<MyDbContext>(null);
            //Database.SetInitializer<MyContext>(null);
            Database.SetInitializer<ApplicationDbContext>(null);
            
        }
    }   
}
