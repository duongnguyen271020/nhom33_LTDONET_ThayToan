using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    public class ProfileEmployeeController : BaseController
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/ProfileEmployee
        public ActionResult Index()
        {
            int EmployeeID = Convert.ToInt32(Session["EmployeeId"]);
            var user = db.Employees.Find(EmployeeID);
            if (EmployeeID == 0)// if user is not log in
            {
                RedirectToAction("Index", "LoginEmployee");
            }
            return View("Index", user);
        }

        // GET: Admin/Employees/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        // POST: Admin/ProfileEmployee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee emp)
        {

            var UpdateProfile = db.Employees.SingleOrDefault(a => a.EmployeeID.Equals(id));
            if (UpdateProfile != null)
            {
                //set properties you want to update
                UpdateProfile.Name = emp.Name;
                UpdateProfile.Email = emp.Email;
                UpdateProfile.Phone = emp.Phone;

                //db.Entry(UpdateProfile).State = EntityState.Modified;
                db.Employees.Attach(UpdateProfile);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
            //var UpdateProfile = db.Employees.Find(emp.EmployeeID == id);
            //UpdateProfile.Name = emp.Name;
            //UpdateProfile.Email = emp.Email;
            //UpdateProfile.Phone = emp.Phone;
            //db.Employees.Attach(UpdateProfile);
            //db.SaveChanges();
            //ViewBag.msg = "Done";
            //return View("Index");
        }


    }
}
