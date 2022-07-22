using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class BookMangementController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/BookManagement
        public ActionResult Index()
        {
            var book = db.Books.Include(b => b.Author).Include(b => b.Genre);
            return View(book.ToList());
        }

        // GET: Admin/BookManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/BookManagement/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            return View();
        }

        // POST: Admin/BookManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BookName,Price,Description,Status,GenreID,AuthorID")] Book book, IEnumerable<HttpPostedFileBase> ImageProduct)
        {
            if (ModelState.IsValid)
            {
                book.CreatedDate = DateTime.Now;
                db.Books.Add(book);
                string path = Server.MapPath("~/UploadFile"); // Create a Folder in your project  

                if (!Directory.Exists(path)) Directory.CreateDirectory(path); // if folder UploadFile is exist, this code will skip
                foreach (var file in ImageProduct)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        //book.Image = fileName;
                        //db.Employee.Add(book);
                        var pathh = Path.Combine(Server.MapPath("~/UploadFile"), fileName);

                        file.SaveAs(pathh);

                        FILE f = new FILE();
                        f.FileName = fileName;
                        f.Path = "\\UploadFile\\" + fileName;
                        f.BookID = book.BookID;

                        db.FILES.Add(f);
                        db.SaveChanges();
                    }
                }
                //string imageName = ImageProduct.FileName;
                //ImageProduct.SaveAs(path + "\\" + imageName);
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Admin/BookManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // POST: Admin/BookManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BookName,Price,Description,CreatedDate,Status,GenreID,AuthorID")] Book book, IEnumerable<HttpPostedFileBase> ImageProduct)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/UploadFile"); // Create a Folder in your project  

                if (!Directory.Exists(path)) Directory.CreateDirectory(path); // if folder UploadFile is exist, this code will skip

                var goods = db.FILES.Where(b => b.BookID == book.BookID).AsEnumerable();
                //string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                foreach (var file in ImageProduct)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        foreach (var bk in goods)
                        {
                            db.FILES.Remove(bk);
                            if (System.IO.File.Exists(path + "\\" + bk.FileName))
                            {
                                System.IO.File.Delete(path + "\\" + bk.FileName);
                            }
                        }
                    }
                }
                foreach (var file in ImageProduct)
                {

                    if (file != null && file.ContentLength > 0)
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        //book.Image = fileName;
                        //db.Employee.Add(book);
                        var pathh = Path.Combine(Server.MapPath("~/UploadFile"), fileName);

                        file.SaveAs(pathh);

                        FILE f = new FILE();
                        f.FileName = fileName;
                        f.Path = "\\UploadFile\\" + fileName;
                        f.BookID = book.BookID;
                        db.FILES.Add(f);
                        db.Books.Add(book);

                        //db.Entry(f).State = EntityState.Modified;
                        db.Entry(book).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Books.Add(book);
                        db.Entry(book).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Admin/BookManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/BookManagement/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            DeleteBool(id);
            return RedirectToAction("Index");
        }

        public bool DeleteBool(int id)
        {
            try
            {
                //Book books = db.Books.Find(id);
                string path = Server.MapPath("~/UploadFile");
                var goods = db.FILES.Where(b => b.BookID == id).AsEnumerable();
                //string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                foreach (var bk in goods)
                {
                    db.FILES.Remove(bk);
                    if (System.IO.File.Exists(path + "\\" + bk.FileName))
                    {
                        System.IO.File.Delete(path + "\\" + bk.FileName);
                    }
                }
                Book book = db.Books.Find(id);
                db.Books.Remove(book);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            var result = new UserFunction().ChangeStatusBook(id);
            return Json(new
            {
                Status = result
            });
        }
    }
}
