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
    public class GenresController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/Genres
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Admin/Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Admin/Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Admin/Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Admin/Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreID,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Admin/Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        //// POST: Admin/Genres/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {

        //        //find an genre I wanted to delete.
        //        Genre catagories = db.Genres.Where(a => a.GenreID == id).SingleOrDefault();
        //        // find all the books that related to the genre
        //        var goods = db.Books.Where(b => b.GenreID == catagories.GenreID).AsEnumerable();
        //        foreach (var bk in goods)
        //        {
        //            var b = bk;
        //            catagories.Books.Remove(b);
        //        }
        //        // delete the genre
        //        db.Genres.Remove(catagories);
        //        db.SaveChanges(); //a SQL DELETE command is generated
        //    }
        //    catch (DataException/* dex */)
        //    {
        //        //Log the error (uncomment dex variable name and add a line here to write a log.
        //        return RedirectToAction("Delete", new { id = id, saveChangesError = true });
        //    }
        //    //Genre genre = db.Genres.Find(id);
        //    //db.Genres.Remove(genre);
        //    //db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserFunction().DeleteGenre(id);
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
