using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class DashboardController : BaseController
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // count product
            var products = (from p in db.Books select p).Count();
            ViewBag.CountProduct = products;
            // count customer
            var client = (from c in db.Users select c).Count();
            ViewBag.CountCustomer = client;
            // count employees
            var emp = (from e in db.Employees select e).Count();
            ViewBag.CountEmployee = emp;
            // count order
            var ord = (from o in db.Orders select o).Count();
            ViewBag.CountOrder = ord;


            return View();
        }





    }
}