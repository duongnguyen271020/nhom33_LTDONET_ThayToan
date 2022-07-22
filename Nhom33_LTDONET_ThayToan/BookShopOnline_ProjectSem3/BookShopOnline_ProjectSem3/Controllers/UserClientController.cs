using BookShopOnline_ProjectSem3.Common;
using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class UserClientController : Controller
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(string FirstName, string LastName, string Mail, string PhoneNumber, string Address, string UserName, string Password, string ConfirmPassword)
        {
            User user = new User();
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Mail;
            user.Phone = PhoneNumber;
            user.Address = Address;
            user.Username = UserName;
            if (Password == ConfirmPassword)
            {
                user.Password = Encryptor.MD5Hash(Password);
            }
            else
            {
                return View("Index");
            }

            user.Status = false;
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
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
                }
                else
                {
                    string passwordUs = Encryptor.MD5Hash(user.Password);
                    user.Password = passwordUs;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}