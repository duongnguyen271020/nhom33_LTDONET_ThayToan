using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class HomeController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Home
        public ActionResult Index()
        {
            //
            var genrebanner = db.Genres.ToList();
            ViewBag.genreB = genrebanner;

            //var genreHome = db.Genres.ToList();
            var genreHome = db.Genres.Take(3).ToList();
            ViewBag.genreHome = genreHome;
            //
            var book1 = db.Books.OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            ViewBag.book1 = book1;

            return View( db.Books.OrderByDescending(x => x.CreatedDate).ToList());
        }
    }
}