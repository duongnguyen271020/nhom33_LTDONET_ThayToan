using BookShopOnline_ProjectSem3.Common;
using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookShopOnline_ProjectSem3.Areas.Admin.Controllers
{

    public class LoginEmployeeController : Controller
    {
        public BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Admin/LoginEmployee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool LoginEmployee(string UserName, string Password)
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
        public Employee GetById(string userName)
        {
            return db.Employees.SingleOrDefault(x => x.Username == userName);
        }
        [HttpPost]
        public ActionResult Login(BookShopOnline_ProjectSem3.Models.Employee userModel, string Username, string Password)
        {
                if(Username == "" || Password == "")
                {
                    userModel.LoginErrorMessage = "Please enter your user name and password";
                    return View("Index", userModel);
                }
                //if(Username)
                Password = Encryptor.MD5Hash(Password);
                var obj = db.Employees.Where(x => x.Username == Username && x.Password == Password).SingleOrDefault();
                var userDetail = LoginEmployee(userModel.Username, Encryptor.MD5Hash(userModel.Password));
                if (userDetail == true || obj != null)
                {
                    var user = GetById(userModel.Username);
                    var userSession = new UserLogin();
                    //create session
                    Session["Employee"] = userModel;
                    FormsAuthentication.SetAuthCookie("user" + obj.EmployeeID, true);
                    

                    Session["EmployeeId"] = db.Employees.Single(x => x.Username == userModel.Username).EmployeeID;
                    Session["Image"] = db.Employees.Single(x => x.Username == userModel.Username).Image;
                    userSession.UserName = user.Username;
                    userSession.ID = user.EmployeeID;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    if (obj.Role.Equals("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (obj.Role.Equals("Manager"))
                    {
                        return RedirectToAction("Index", "BookMangement");
                    }
                    //return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    userModel.LoginErrorMessage = "Username or Password is wrong! Please type again";
                    return View("Index", userModel);
                }
            
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }

        //[HttpPost]
        //public ActionResult Login(string Username, string Password, Employee userModel, string Url)
        //{

        //    var obj = db.Employees.Where(x => x.Username == Username && x.Password == Password).SingleOrDefault();
        //    if (obj != null)
        //    {

        //        Session["Employee"] = userModel;
        //        FormsAuthentication.SetAuthCookie("user" + obj.EmployeeID, false);
        //        Session["EmployeeId"] = db.Employees.Single(x => x.Username == userModel.Username).EmployeeID;
        //        Session["Image"] = db.Employees.Single(x => x.Username == userModel.Username).Image;
        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Login fail . Please try again !";
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}