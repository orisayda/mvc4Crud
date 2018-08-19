using Mvc4Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Mayur.Web.Attributes;

namespace Mvc4Crud.Controllers
{
    public class angularRouteController : Controller
    {
        //
        // GET: /angularRoute/
        public ActionResult Index()
        {
            return View();
        }

        [SessionTimeout]
        public ActionResult Header()
        {
            //Notify getNotifications = new Notify();
            int user_id = ((int)Session["user_id"]);

            Tasks getEvents = new Tasks();

            List<Tasks> list = new List<Tasks>();

            list = getEvents.getNotifications(user_id);

            //ViewData["notifications"] = list;
            //ViewData["total_notifications"] = list.Count();

            ViewBag.notifications = list;
            Session["notification_count"] = list.Count();

            //if (Session["notification_count"] == null)
            //{
            //    Session["notification_count"] = list.Count();

            //}
            //else
            //{
            //    Session["notification_count"] = "used";
            //}
            



            return View();
        }
        public ActionResult ViewSignIn(){

           /* if(Session["first_name"] == null){
                return View();
           }*/
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [SessionTimeout]
        public ActionResult DashBoard()
        {
            return View();
        }

        [SessionTimeout]
        public ActionResult createEvent()
        {
            return View();
        }

        [SessionTimeout]
        public ActionResult listEvent(string CurrentSort, int? page,string search=null,string sortBy = null,string status = null)
        {
           
            int user_id = ((int)Session["user_id"]);

            string role = (string)Session["role"];
           
            ApplicationDbContext db = new ApplicationDbContext();

            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            string searchEvent;
            searchEvent = String.IsNullOrEmpty(search) ? null : Convert.ToString(search);
            string taskStatus;
            taskStatus = String.IsNullOrEmpty(status) ? null : Convert.ToString(status);
            DateTime today = DateTime.Today;

            //ViewBag.CurrentSort = sortBy;
            sortBy = String.IsNullOrEmpty(sortBy) ? "event_id" : sortBy;
            IPagedList<Tasks> events = null;

            switch (sortBy)
            {
                case "low":
                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if(taskStatus == "completed"){

                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                                

                            } else if (taskStatus == "un-completed") {

                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);

                            }
                            else
                            {

                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);

                            }


                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.title).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date >= today).OrderBy(m => m.title).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.title).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "Low")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;

                case "high":

                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);

                            }


                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date >= today).OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "Medium")).ThenBy(e => (e.priority == "High")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;

                case "medium":

                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);

                            }


                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date >= today).OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.title).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(e => (e.priority == "Low")).ThenBy(e => (e.priority == "High")).ThenBy(e => (e.priority == "Medium")).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;

                case "title":
                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);

                            }


                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(m => m.title).ToPagedList(pageIndex, pageSize);

                            }

                            


                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;               

                case "start_date":
                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);

                            }


                        }

                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(m => m.start_date < today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(m => m.start_date >= today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(m => m.start_date).ToPagedList(pageIndex, pageSize);

                            }
                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;

                case "end_date":
                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).Where(e => e.title.Contains(searchEvent)).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);

                            }


                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = sortBy;

                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.start_date < today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date >= today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderBy(m => m.end_date).ToPagedList(pageIndex, pageSize);

                            }


                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = sortBy;
                    }
                    break;  
           
                case "event_id":

                    if (searchEvent != null)
                    {

                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date < today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent) && e.start_date >= today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.title.Contains(searchEvent)).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);

                            }
                        }
                        else
                        {
                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).Where(e => e.title.Contains(searchEvent)).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).Where(e => e.title.Contains(searchEvent)).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).Where(e => e.title.Contains(searchEvent)).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);

                            }
                        }


                        ViewData["search"] = searchEvent;
                        ViewData["sortBy"] = "event_id";



                    }
                    else
                    {
                        if (role.Equals("admin"))
                        {
                            if (taskStatus == "completed")
                            {                                
                                events = db.Events.Where(e=> e.start_date < today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);

                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.start_date >= today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }
                            else
                            {
                                events = db.Events.OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);
                            }

                        }
                        else
                        {

                            if (taskStatus == "completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date < today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);                                

                            }
                            else if (taskStatus == "un-completed")
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id) && e.start_date >= today).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);                                

                            }
                            else
                            {
                                events = db.Events.Where(e => e.user_id.Equals(user_id)).OrderByDescending(m => m.event_id).ToPagedList(pageIndex, pageSize);

                            }



                        }
                        ViewData["search"] = "";
                        ViewData["sortBy"] = "event_id";
                        
                    }
                    break;
            }

            ViewData["status"] = taskStatus;

            //ViewData["jsonEvents"] = Json(events, JsonRequestBehavior.AllowGet);     

            return View(events);

           
        }

        [SessionTimeout]
        public ActionResult EditEvent(int event_id)
        {
            Tasks EditEvent = new Tasks();

            returnEvent returnData = new returnEvent();

            returnData = EditEvent.EditEvent(event_id);

            //return Json(returnData, JsonRequestBehavior.AllowGet);

            return View(returnData);
        }
	}
}