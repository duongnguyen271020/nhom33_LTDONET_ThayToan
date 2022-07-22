using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class NewsController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: News
        public ActionResult Index()
        {
            return View("Index", db.News);
        }

        
    }
}