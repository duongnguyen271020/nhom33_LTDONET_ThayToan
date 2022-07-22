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
    public class NewsManagerController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/NewsManager
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/NewsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/NewsManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Title,Content")] News news, HttpPostedFileBase imageNew)
        {
            if (ModelState.IsValid)
            {
                news.CreatedDate = DateTime.Now;

                db.News.Add(news);
                if (imageNew != null)
                {
                    //products.CreatedDate = DateTime.Now;
                    //db.Products.Add(products);
                    string path = Server.MapPath("~/UploadImageNews");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //string imagesName = imagesProduct.FileName;
                    //imagesProduct.SaveAs(path + "\\" + imagesName);
                    string imagesName = imageNew.FileName;
                    imageNew.SaveAs(path + "\\" + imagesName);
                    //var fileName = Path.GetFileName(file.FileName);
                    news.Image = imagesName;

                    db.News.Add(news);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/NewsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            Session["imgPath"] = news.Image;
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/NewsManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Title,Content,CreatedDate")] News news, HttpPostedFileBase imageNew)
        {
            if (ModelState.IsValid)
            {
                if (imageNew != null)
                {
                    string path = Server.MapPath("~/UploadImageNews"); // Create a link Folder in your project  

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path); // if folder UploadFile is exist, this code will skip
                    string imageName = imageNew.FileName;
                    imageNew.SaveAs(path + "\\" + imageName);
                    news.Image = imageName;

                    db.News.Add(news);
                    db.Entry(news).State = EntityState.Modified;

                    string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    news.Image = Session["imgPath"].ToString();

                    db.News.Add(news);
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(news);
        }

        // GET: Admin/NewsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            Session["imgPath"] = news.Image;
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //// POST: Admin/NewsManager/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    News news = db.News.Find(id);
        //    db.News.Remove(news);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
                News news = db.News.Find(id);
                string path = Server.MapPath("~/UploadImageNews");
                //string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                string currentImg = path + "\\" + news.Image;
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
                db.News.Remove(news);
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
    }
}
