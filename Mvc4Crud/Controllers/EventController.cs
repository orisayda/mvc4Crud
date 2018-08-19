using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayur.Web.Attributes;
using Mvc4Crud.Models;

namespace Mvc4Crud.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /ToDo/
        public ActionResult Index()
        {
            return View();
        }

        [SessionTimeout]
        [HttpPost]
       
        public ActionResult Add(Tasks add,string start_time,string end_time)
        //public ActionResult Add( collection)
        {
            Tasks newEvent = new Tasks();

            newEvent.title =  add.title; //users.first_name;
            
            //newEvent.location   =  add.location; //users.last_name;
            
            newEvent.start_date =  add.start_date; //users.email;

            if(start_time != null){
                newEvent.start_time = DateTime.Parse(start_time).TimeOfDay; //(TimeSpan)start_time;
            }
            else
            {
                newEvent.start_time = null;
            }

            if (end_time != null)
            {
                newEvent.end_time = DateTime.Parse(end_time).TimeOfDay; //(TimeSpan)end_time;
            }
            else
            {
                newEvent.end_time = null;
            }

            if(newEvent.start_time == null && newEvent.end_time == null){
                newEvent.isTimeSet = 0;
            }
            else {
                newEvent.isTimeSet = 1;
            }
            
            newEvent.end_date =  add.end_date  ;
            
            //addEvent.start_date =  add.all_time  ;
            newEvent.description =  add.description;

            newEvent.priority = add.priority;

            string user_id = string.Empty;
            user_id = Convert.ToString(Session["user_id"]);

            if (newEvent.AddEvent(user_id, newEvent) == 1)
            {
                Tasks EditEvent = new Tasks();

                int event_id = EditEvent.GetLastId();                

                returnEvent returnData = new returnEvent();

                var data = EditEvent.EditEvent(event_id);


                var returnData1 = new addedEvent { success = 1, returnMsg = "Event Created SuccessFully", eventData = data };
                List<addedEvent> listAddedEvent = new List<addedEvent>();
                listAddedEvent.Add(returnData1);
                
                return Json(listAddedEvent, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var returnData = new addedEvent { success = 0, returnMsg = "Event Not Created SuccessFully" };
                List<addedEvent> listAddedEvent = new List<addedEvent>();
                listAddedEvent.Add(returnData);

                return Json(listAddedEvent, JsonRequestBehavior.AllowGet);
            }
            //var addedEvent = { };
            //return View();// Json(addedEvent, JsonRequestBehavior.AllowGet);   
        }

        // GET: All books
        public JsonResult GetAllEvents()
        {
            Tasks AllEvents = new Tasks();

            List<Tasks> ViewEvents = AllEvents.GetAllEvents();
            //List<Events> ViewEvents = AllEvents.GetAllEventsById();

           
            return Json(ViewEvents, JsonRequestBehavior.AllowGet);


         /*   using (BookDBContext contextObj = new BookDBContext())
            {
                var bookList = contextObj.book.ToList();
                return Json(bookList, JsonRequestBehavior.AllowGet);
            }*/
        }

        public JsonResult GetAllEventById(int user_id)
        {
            //int user_id = ((int)Session["user_id"]);
            Tasks AllEvents = new Tasks();
            //List<Events> ViewEvents = AllEvents.GetAllEvents();
            List<Tasks> ViewEvents = AllEvents.GetAllEventsById(user_id);


            return Json(ViewEvents, JsonRequestBehavior.AllowGet);


            /*   using (BookDBContext contextObj = new BookDBContext())
               {
                   var bookList = contextObj.book.ToList();
                   return Json(bookList, JsonRequestBehavior.AllowGet);
               }*/
        }
        public JsonResult GetEvent(int event_id)
        {
            Tasks EditEvent = new Tasks();

            returnEvent returnData = new returnEvent();

            returnData = EditEvent.EditEvent(event_id);

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        public string UpdateEvent(Tasks eventData, string start_time, string end_time)
        {
            if (eventData != null)
            {
                Tasks newEvent = new Tasks();

                newEvent.title = eventData.title; //users.first_name;
                //newEvent.location = eventData.location; //users.last_name;

                newEvent.start_date = eventData.start_date; //users.email;

                if (start_time != null)
                {
                    newEvent.start_time = DateTime.Parse(start_time).TimeOfDay; //(TimeSpan)start_time;
                    
                }
                else
                {
                    newEvent.start_time = null;
                }


                if (end_time != null)
                {
                    newEvent.end_time = DateTime.Parse(end_time).TimeOfDay; //(TimeSpan)end_time;
                    
                }
                else
                {
                    newEvent.end_time = null;
                }

                if (newEvent.start_time == null && newEvent.end_time == null)
                {
                    newEvent.isTimeSet = 0;
                }
                else
                {
                    newEvent.isTimeSet = 1;
                }

                

                newEvent.end_date = eventData.end_date;

                
                //addEvent.start_date =  add.all_time  ;
                newEvent.description = eventData.description;

                newEvent.priority = eventData.priority;

                newEvent.status = eventData.status;

                newEvent.event_id = eventData.event_id;

                eventData.UpdateEvent(newEvent);

                return "Event record updated successfully";
                //return RedirectToAction("ListUsers");
            }
            else
            {
                return "Invalid Event record";
            }
        }

        public string DeleteEvent(int event_id)
        {

            if (event_id != null)
            {
                try
                {
                    //int event_id = Int32.Parse(e_id);

                    {
                        /*var _book = contextObj.book.Find(_bookId);
                        contextObj.book.Remove(_book);
                        contextObj.SaveChanges();*/
                        Tasks deleteEvent = new Tasks();
                        if (deleteEvent.DeleteEventsById(event_id))
                        {
                            return "Selected Event record deleted sucessfully";
                        }
                        else
                        {
                            return "Selected Event record not deleted";
                        }                        
                    }
                }
                catch (Exception)
                {
                    return "Event details not found";
                }
            }
            else
            {
                return "Invalid operation";
            }
        }

	}
}