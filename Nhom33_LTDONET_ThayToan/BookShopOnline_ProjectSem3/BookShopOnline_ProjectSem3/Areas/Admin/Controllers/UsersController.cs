using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopOnline_ProjectSem3.Common;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Password,FirstName,LastName,Address,Email,Phone,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                string passwordCs = Encryptor.MD5Hash(user.Password);
                user.Password = passwordCs;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public bool KTPassword(string UserName, string Password)
        {
            var result = db.Users.Count(x => x.Username == UserName && x.Password == Password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Password,FirstName,LastName,Address,Email,Phone,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                var kt = KTPassword(user.Username, user.Password);
                if (kt == true)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string passwordUs = Encryptor.MD5Hash(user.Password);
                    user.Password = passwordUs;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //// POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserFunction().DeleteClient(id);
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

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserFunction().ChangeStatuss(id);
            return Json(new
            {
                Status = result
            });
        }

        

    }
}
