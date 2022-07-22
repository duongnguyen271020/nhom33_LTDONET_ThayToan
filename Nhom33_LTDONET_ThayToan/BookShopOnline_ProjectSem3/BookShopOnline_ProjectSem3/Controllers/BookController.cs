using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class BookController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Book
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }
        public ActionResult Genre(int id)
        {
            var genreId = db.Genres.Where(x => x.GenreID == id).ToList();
            ViewBag.genreId = genreId;
            var genreBook = db.Books.Where(x => x.GenreID == id).ToList();
            ViewBag.genreBook = genreBook;            
            return View();
        }


        public JsonResult ListName(string q)
        {
            var data = new UserFunction().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword)
        {
            int totalRecord = 0;
            var model = new UserFunction().Search(keyword, ref totalRecord);

            ViewBag.Total = totalRecord;
            ViewBag.Keyword = keyword;

            return View(model);
        }


    }
}