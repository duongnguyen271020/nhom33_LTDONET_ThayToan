using BookShopOnline_ProjectSem3.Common;
using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class LoginController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        public bool LoginUser(string UserName, string Password)
        {
            var result = db.Users.Count(x => x.Username == UserName && x.Password == Password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult Login(BookShopOnline_ProjectSem3.Models.User user, string Username, string Password)
        {
            Password = Encryptor.MD5Hash(Password);
            var result = LoginUser(user.Username, Password);
            var obj = db.Users.Where(x => x.Username == Username && x.Password == Password).SingleOrDefault();
            if (result == true)
            {

                Session["username"] = Username; // use for comment
                Session["Client"] = new User();
                Session["Client"] = obj;
                Session["ClientId"] = db.Users.Single(x => x.Username == obj.Username).UserID; // use for profile
                return new RedirectResult(@Url.Action("Index", "Home"));
            }
            else
            {
                user.LoginErrorMessage = "Username or Password is wrong! Please type again";
                return View("Index", user);
            }
            //return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return new RedirectResult(@Url.Action("Index", "Home"));
        }
    }
}