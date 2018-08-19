using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc4Crud
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           


            routes.MapRoute(
                name: "signIn",
                url: "angularRoute/User/SignIn",
                defaults: new
                {
                    controller = "angularRoute",
                    action = "ViewSignIn"
                    //,id = UrlParameter.Optional
                }
            );

          

            routes.MapRoute(
               name: "header",
               url: "angularRoute/Header",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "Header"
                   //,id = UrlParameter.Optional
               }
           );

            

            routes.MapRoute(
               name: "register",
               url: "angularRoute/User/Register",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "Register"
                   //,id = UrlParameter.Optional
               }
           );

            
            routes.MapRoute(
               name: "dashboard",
               url: "angularRoute/User/Dashboard",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "DashBoard"
                   //,id = UrlParameter.Optional
               }
           );

            routes.MapRoute(
               name: "create_event",
               url: "angularRoute/Task/Create",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "createEvent"
                   //,id = UrlParameter.Optional
               }
           );

               routes.MapRoute(
               name: "list_event",
               url: "angularRoute/Task/List",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "listEvent"
                   //,id = UrlParameter.Optional
               }
           );


               routes.MapRoute(
               name: "edit_event",
               url: "angularRoute/Task/Edit/{event_id}",
               defaults: new
               {
                   controller = "angularRoute",
                   action = "EditEvent",    
                   event_id = ""// UrlParameter.Optional
               }
           );

               routes.MapRoute(
                   name:    "login",
                   url:     "User/Guest",
                   defaults: new
                   {
                       controller = "User",
                       action ="Guest"
                   }
                   
               );

               routes.MapRoute(
                   name: "appStart",
                   url: "User",
                   defaults: new
                       {
                           controller = "User",
                           action = "Auth"
                       }                   
                   );

             routes.MapRoute(
                  name: "Default",
                  url: "{controller}/{action}/{id}",
                  defaults: new
                  {
                      controller = "User",
                      action = "Auth",
                      id = UrlParameter.Optional
                  }
              );
          
        }
    }
}
