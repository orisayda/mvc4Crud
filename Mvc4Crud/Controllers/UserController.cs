using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Mvc4Crud.Models;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Net;
using Mvc4Crud.ViewModel.User;
using Mayur.Web.Attributes;


namespace Mvc4Crud.Controllers
{
    public class UserController : Controller
    {

        /* public UserController()
             : this(new Usermanage<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
         {
         }

         [SessionTimeout]
         public ActionResult ListUsers()
         {
             UserViewModel  viewAll = new UserViewModel();

             List<Users> ViewUsers = viewAll.GetAllUsers();
             TempData["Message"] = null;
             return View(ViewUsers);
         }

          [SessionTimeout]
         public ActionResult Detail(int Id)
         {
             UserViewModel userVm = new UserViewModel();
             UpdateUser userData = userVm.GetUser(Id);
             Session["user_id"] = userData.user_id;
             Session["first_name"] = userData.first_name;
             Session["email"] = userData.email;   
             return View(userData);
         }

         [SessionTimeout]
         [HttpGet]
         public ActionResult Edit(int Id)
         {
             UserViewModel userVm = new UserViewModel();
             UpdateUser userData = userVm.GetUser(Id);
             return View(userData);
         }

         [SessionTimeout]
         [HttpPost]
         public ActionResult EditUser(Users user){

             if(ModelState.IsValid){
                 UserViewModel userVm = new UserViewModel();
                 userVm.UpdateUser(user);
                 return RedirectToAction("ListUsers");
             }
            
             return RedirectToAction("ListUsers");

         }

         [SessionTimeout]
         public ActionResult Delete(int Id)
         {
             if (Id != null)
             {
                 UserViewModel userVm = new UserViewModel();
                 userVm.Delete(Id);
                 TempData["Message"] = "User is Deleted SuccessFull";
                 return RedirectToAction("ListUsers");
             }
             else
             {
                 TempData["Message"] = "Wrong Request";
                 return RedirectToAction("ListUsers");
             }
           
         }*/


        //view sign up page
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            TempData["shortMessage"] = null;
            ViewBag.title = "Web Project";            
            return View();
        }

    
        public ActionResult Auth()
        {
           
            if(Session["first_name"] == null){
                TempData["shortMessage"] = null;
                return RedirectToAction("Guest", "User");
            }

           //return Redirect("/User/Test#/app/dashboard-v2");  
            ViewBag.title = "ToDo List";       
            return View();          
        }

        public ActionResult Guest()
        {
            
            if(Session["first_name"] == null){
                TempData["shortMessage"] = null;
                ViewBag.title = "Web Project";       
                return View();
            }

         //  return Redirect("/User/Test#/app/dashboard-v2");  
            return RedirectToAction("Auth", "User");     

        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogIn()
        {
            TempData["shortMessage"] = null;
            ViewBag.title = "Web Project";       
            return View();

        }
                       
        //post registration data
                
        [HttpPost]        
        public ActionResult Register(Users user)
        {
            Users addUser = new Users();

          //  if (ModelState.IsValid )
            //{
            if (!addUser.IsValidEmailAddress(user.email))
                {
                   // TempData["shortMessage"] = "In Valid Email Pattern";
                   // return RedirectToAction("SignUp");
                   // return View("SignUp");
                    var not_added_user = new List<object>();
                    not_added_user.Add(new { success = 0, errormsg = "In-Valid Email!" });
                    //TempData["shortMessage"] = "Sign Process Fail.";
                    //return View("SignUp");
                    return Json(not_added_user, JsonRequestBehavior.AllowGet);     
                }

            if (addUser.IsUsedEmailAddress(user.email))
            {
                var not_added_user = new List<object>();
                not_added_user.Add(new { success = 0, errormsg = "Email is Already Exists!" });
                //TempData["shortMessage"] = "Sign Process Fail.";
                //return View("SignUp");
                return Json(not_added_user, JsonRequestBehavior.AllowGet);  
            }


                    addUser.first_name = user.first_name; //users.first_name;
                    addUser.last_name = user.last_name; //users.last_name;
                    addUser.email = user.email; //users.email;
                    addUser.password = addUser.encryption(user.password); //users.password;



                 if (addUser.AddUser(addUser) == 1)
                 {
                     //TempData["shortMessage"] = "created.";                       

                     //HttpSessionState.Session["user_id"] = addUser.user_id;
                     //Context.Session.SetString("Name", "Anuraj");

                     UserLogin login = new UserLogin();
                     login.email = user.email;
                     login.password = user.password;

                     var result = login.isAuth(login);
                     if (result.Rows.Count > 0)
                     {

                         var logInUSer = new List<object>();
                         logInUSer.Add(new { success = 1 });                        

                         

                         //return Json(logInUSer, JsonRequestBehavior.AllowGet);

                         Session["user_id"] = result.Rows[0]["user_id"];
                         Session["first_name"] = result.Rows[0]["first_name"];
                         Session["email"] = result.Rows[0]["email"];
                         Session["role"] = result.Rows[0]["role"];
                         var added_user = new List<object>();
                         added_user.Add(new { success = 1 });
                         added_user.Add(new { user_id = Session["user_id"] });
                         added_user.Add(addUser);

                         return Json(added_user, JsonRequestBehavior.AllowGet);

                     }
                     else
                     {
                         var not_added_user = new List<object>();
                         not_added_user.Add(new { success = 0, errormsg = "Login Process Fials" });
                        
                         return Json(not_added_user, JsonRequestBehavior.AllowGet); 
                     }                   
                    
                 }
                 else
                 {
                     var not_added_user = new List<object>();
                     not_added_user.Add(new { success = 0, errormsg = "Process Fails" });
                    
                     return Json(not_added_user, JsonRequestBehavior.AllowGet);      
                 }
            }


        [HttpPost]
        [AllowAnonymous]        
        public ActionResult Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {

                if (!login.IsValidEmailAddress(login.email))
                {
                    //TempData["shortMessage"] = "In Valid Email Pattern";
                    //return RedirectToAction("LogIn");
                    //return View("LogIn");
                    var notLogin = new List<object>();
                    notLogin.Add(new { success = 0, errormsg = "In Valid Email!" });
                    //TempData["shortMessage"] = "Sign Process Fail.";
                    //return View("SignUp");
                    //return RedirectToAction("SignUp");
                    return Json(notLogin, JsonRequestBehavior.AllowGet);      
                }

               

                login.email = login.email;
                login.password = login.password;
                var result = login.isAuth(login);
                if(result.Rows.Count > 0){

                    //Session["user_id"] = result.Rows[0]["user_id"];
                    //Session["first_name"] = result.Rows[0]["first_name"];
                    //Session["email"] = result.Rows[0]["email"];                   
                    //return RedirectToAction("UserDashBoard");
                    var logInUSer = new List<object>();
                    logInUSer.Add(new { success = 1 });
                   

                    //login.GetUserId(login);

                    Session["user_id"] = result.Rows[0]["user_id"];
                    Session["first_name"] = result.Rows[0]["first_name"];
                    Session["email"] = result.Rows[0]["email"];
                    Session["role"] = result.Rows[0]["role"];

                    logInUSer.Add(new { user_id = Session["user_id"] });
           
                    return Json(logInUSer, JsonRequestBehavior.AllowGet);

                }else{
                    //TempData["shortMessage"] = "Sorry Your Email Or Password Is Wrong";
                    //return View("LogIn");
                    var notLogin = new List<object>();
                    notLogin.Add(new { success = 0, errormsg = "Sorry Your Email Or Password Is Incorrect!" });
                    return Json(notLogin, JsonRequestBehavior.AllowGet);
                }     
            }
            else
            {
                //TempData["shortMessage"] = "Sorry Your Email Or Password Is Wrong";
               // return View("LogIn");
                var notLogin = new List<object>();
                notLogin.Add(new { success = 0, errormsg = "Sorry Your Email Or Password Is Incorrect!" });
                return Json(notLogin, JsonRequestBehavior.AllowGet);
            }
            //return View("LogIn");
        }

        /*public async Task<ActionResult> Details(UserLogin user, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return new HttpStatusCodeResult(HttpStatusCodeResult.badRe                    )
            }

            // Commenting out original code to show how to use a raw SQL query.
            //Department department = await db.Departments.FindAsync(id);

            // Create and execute raw SQL query.
            string query = "SELECT * FROM Department WHERE DepartmentID = @p0";
            //            Users user = await UserManager.Departments.SqlQuery(query, id).SingleOrDefaultAsync();
            Users user = await UserManager.Users.sqlQuery(query, id).SingleOrDefaultAsync();

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        */

        //private UserDBContext db = new UserDBContext();


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Guest");
            //return View();

        }
/*
        public ActionResult UserDashBoard(Users userData)
        {
            

            int user_id;
            if (Session["User_id"] != null)
            {
                user_id = (int)System.Web.HttpContext.Current.Session["User_id"];
            }
            else
            {
                //return RedirectToAction("LogIn");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           // userData = db.User.Find(user_id);
            //string username = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(ID).UserName;

            if (userData == null)
            {
                return HttpNotFound();
            
            }

            return View(userData);

            //u.user_id = user_id;
            //var userData = user.getUser(1);
            //userData = new List<string> {  user.getUser(user_id) };
            //Users user = db.Users.Find(id);
            //return View(model.ToList());

            //return View(userData);
        }
        */

        public ActionResult Users()
        {

            return View();
        }
	}
}