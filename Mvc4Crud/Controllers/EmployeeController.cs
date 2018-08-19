
using Mvc4Crud.Entities;
using Mvc4Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;  

namespace Mvc4Crud.Controllers
{
    public class EmployeeController : Controller
    {
        //  
        // GET: /Employee/  
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int pageSize = 1;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
            IPagedList<EmployeeMaster> employees = null;
            switch (sortOrder)
            {
                case "Name":
                    if (sortOrder.Equals(CurrentSort))
                        employees = db.Employees.OrderByDescending
                                (m => m.Name).ToPagedList(pageIndex, pageSize);
                    else
                        employees = db.Employees.OrderBy
                                (m => m.Name).ToPagedList(pageIndex, pageSize);
                    break;
                case "Email":
                    if (sortOrder.Equals(CurrentSort))
                        employees = db.Employees.OrderByDescending
                                (m => m.Email).ToPagedList(pageIndex, pageSize);
                    else
                        employees = db.Employees.OrderBy
                                (m => m.Email).ToPagedList(pageIndex, pageSize);
                    break;
                case "Phone":
                    if (sortOrder.Equals(CurrentSort))
                        employees = db.Employees.OrderByDescending
                                (m => m.PhoneNumber).ToPagedList(pageIndex, pageSize);
                    else
                        employees = db.Employees.OrderBy
                                (m => m.PhoneNumber).ToPagedList(pageIndex, pageSize);
                    break;
                case "Salary":
                    if (sortOrder.Equals(CurrentSort))
                        employees = db.Employees.OrderByDescending
                                (m => m.Salary).ToPagedList(pageIndex, pageSize);
                    else
                        employees = db.Employees.OrderBy
                                (m => m.Salary).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    employees = db.Employees.OrderBy
                        (m => m.Name).ToPagedList(pageIndex, pageSize);
                    break;
            }
            return View(employees);
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EmployeeMaster emp)
        {
            emp.ID = Guid.NewGuid().ToString();
            ApplicationDbContext db = new ApplicationDbContext();
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }  
	}
}