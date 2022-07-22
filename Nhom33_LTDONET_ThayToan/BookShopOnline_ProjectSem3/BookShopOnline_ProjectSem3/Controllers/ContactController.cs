using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class ContactController : Controller
    {
        public BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Contact
        public ActionResult Index()
        {
            var contact = db.Contacts.ToList();
            ViewBag.viewC = contact;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.CreatedDate = DateTime.Now;
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");

        }
    }
}