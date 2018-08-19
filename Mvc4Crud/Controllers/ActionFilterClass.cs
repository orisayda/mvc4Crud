using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mayur.Web.Attributes
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["first_name"] == null)
            {
                filterContext.Result = new RedirectResult("~/User/Guest");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}