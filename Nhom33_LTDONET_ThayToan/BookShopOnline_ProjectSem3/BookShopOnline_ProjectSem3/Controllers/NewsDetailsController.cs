using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class NewsDetailsController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: NewsDetails
        public ActionResult Index(int id)
        {
            var more = db.News.Find(id);
            ViewBag.NewsDetail = more;
            return View();
        }
    }
}