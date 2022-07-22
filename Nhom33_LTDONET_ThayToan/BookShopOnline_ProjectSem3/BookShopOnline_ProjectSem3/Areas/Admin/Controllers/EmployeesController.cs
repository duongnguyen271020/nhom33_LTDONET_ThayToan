using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopOnline_ProjectSem3.Common;
using BookShopOnline_ProjectSem3.Models;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : BaseController
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();

        // GET: Admin/Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Username,Password,Name,Email,Phone,Address,Birthday,Gender,Role,Status")] Employee employee, HttpPostedFileBase imageProduct)
        {
            if (ModelState.IsValid)
            {
                var result = db.Employees.Count(x => x.Username == employee.Username);
                if (result > 0)
                {
                    employee.LoginErrorMessage = "tên tài khoản của bạn đã được sử dụng";
                }
                else
                {
                    var encryptedMd5pas = Encryptor.MD5Hash(employee.Password);
                    employee.Password = encryptedMd5pas;
                    db.Employees.Add(employee);
                    if (imageProduct != null)
                    {
                        //products.CreatedDate = DateTime.Now;
                        //db.Products.Add(products);
                        string path = Server.MapPath("~/UploadImageEmployees");
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                        //string imagesName = imagesProduct.FileName;
                        //imagesProduct.SaveAs(path + "\\" + imagesName);
                        string imagesName = imageProduct.FileName;
                        imageProduct.SaveAs(path + "\\" + imagesName);
                        //var fileName = Path.GetFileName(file.FileName);
                        employee.Image = imagesName;

                        db.Employees.Add(employee);
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Index");
                }
                
                
            }

            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            //Session["PasswordEp"] = employee.Password;
            Session["imgPath"] = employee.Image;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public bool KTPassword(string UserName, string Password)
        {
            var result = db.Employees.Count(x => x.Username == UserName && x.Password == Password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Username,Password,Name,Email,Phone,Address,Birthday,Gender,Role,Status")] Employee employee, HttpPostedFileBase imageProduct)
        {
            if (ModelState.IsValid)
            {
                var result = db.Employees.Count(x => x.Username == employee.Username);
                var kt = KTPassword(employee.Username, employee.Password);
                if (kt == true)
                {
                    db.Employees.Add(employee);
                }
                else
                {
                    var encryptedMd5pas = Encryptor.MD5Hash(employee.Password);
                    employee.Password = encryptedMd5pas;
                    db.Employees.Add(employee);
                }
                
                //db.Entry(employee).State = EntityState.Modified;
                if (imageProduct != null)
                {
                    string path = Server.MapPath("~/UploadImageEmployees"); // Create a link Folder in your project  

                    if (!Directory.Exists(path)) Directory.CreateDirectory(path); // if folder UploadFile is exist, this code will skip
                    string imageName = imageProduct.FileName;
                    imageProduct.SaveAs(path + "\\" + imageName);
                    employee.Image = imageName;

                    db.Employees.Add(employee);
                    db.Entry(employee).State = EntityState.Modified;

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
                    employee.Image = Session["imgPath"].ToString();

                    db.Employees.Add(employee);
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            Session["imgPath"] = employee.Image;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //// POST: Admin/Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
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
                Employee employee = db.Employees.Find(id);
                string path = Server.MapPath("~/UploadImageEmployees");
                //string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                string currentImg = path + "\\" + employee.Image;
                if (System.IO.File.Exists(currentImg))
                {
                    System.IO.File.Delete(currentImg);
                }
                db.Employees.Remove(employee);
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
