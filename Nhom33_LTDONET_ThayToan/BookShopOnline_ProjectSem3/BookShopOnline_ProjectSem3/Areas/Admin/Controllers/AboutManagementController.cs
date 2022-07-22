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
    public class AboutManagementController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/AboutManagement
        public ActionResult Index()
        {
            return View(db.Abouts.ToList());
        }

        // GET: Admin/AboutManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: Admin/AboutManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AboutManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AboutID,Title,Content")] About about, HttpPostedFileBase imageAboutUs)
        {
            if (ModelState.IsValid)
            {
                if (imageAboutUs != null)
                {
                    //products.CreatedDate = DateTime.Now;
                    //db.Products.Add(products);
                    string path = Server.MapPath("~/UploadImageAbout");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //string imagesName = imagesProduct.FileName;
                    //imagesProduct.SaveAs(path + "\\" + imagesName);
                    string imagesName = imageAboutUs.FileName;
                    imageAboutUs.SaveAs(path + "\\" + imagesName);
                    //var fileName = Path.GetFileName(file.FileName);
                    about.Image = imagesName;

                    db.Abouts.Add(about);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: Admin/AboutManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            Session["imgPath"] = about.Image;
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Admin/AboutManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AboutID,Title,Content")] About about, HttpPostedFileBase imageAboutUs)
        {
            if (ModelState.IsValid)
            {
                if (imageAboutUs != null)
                {
                    string path = Server.MapPath("~/UploadImageAbout"); // Create a link Folder in your project  

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path); // if folder UploadFile is exist, this code will skip
                    string imageName = imageAboutUs.FileName;
                    imageAboutUs.SaveAs(path + "\\" + imageName);
                    about.Image = imageName;

                    db.Abouts.Add(about);
                    db.Entry(about).State = EntityState.Modified;

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
                    about.Image = Session["imgPath"].ToString();

                    db.Abouts.Add(about);
                    db.Entry(about).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(about);
        }

        // GET: Admin/AboutManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            Session["imgPath"] = about.Image;
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Admin/AboutManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = db.Abouts.Find(id);
            db.Abouts.Remove(about);
            
            DeleteBool(id);
            return RedirectToAction("Index");
        }

        public bool DeleteBool(int id)
        {
            try
            {
                About about = db.Abouts.Find(id);
                string path = Server.MapPath("~/UploadImageAbout");
                string currentImg = path + "\\" + about.Image;
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
                db.Abouts.Remove(about);
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
