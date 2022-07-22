using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class OrderDetailsManagementController : Controller
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/OrderDetailsManagement
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Book).Include(o => o.Employee).Include(o => o.Order).Include(o => o.User);
            return View(orderDetails.ToList());
        }

        // GET: Admin/OrderDetailsManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetailsManagement/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Username");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Admin/OrderDetailsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailsID,ProductName,BookID,Quantity,Price,OrderID,EmployeeID,UserID")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                Book book = db.Books.Find(orderDetail.BookID);
                orderDetail.ProductName = book.BookName;
                orderDetail.Price = book.Price;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Username", orderDetail.EmployeeID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", orderDetail.UserID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetailsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Username", orderDetail.EmployeeID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", orderDetail.UserID);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetailsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailsID,ProductName,BookID,Quantity,Price,OrderID,EmployeeID,UserID")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookName", orderDetail.BookID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Username", orderDetail.EmployeeID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Address", orderDetail.OrderID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", orderDetail.UserID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetailsManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetailsManagement/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
