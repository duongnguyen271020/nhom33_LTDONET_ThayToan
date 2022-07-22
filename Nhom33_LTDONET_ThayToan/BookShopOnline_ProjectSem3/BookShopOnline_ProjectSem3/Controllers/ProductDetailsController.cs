using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class ProductDetailsController : Controller
    {
        BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: ProductDetails
        public ActionResult Index(int id)
        {
            var product = db.Books.Find(id);
            Session["ProductDetailId"] = id;
            ViewBag.product = product;
            ViewBag.Image = "\\UploadFile" + "\\" + product.FILES.Where(x=>x.BookID == id).FirstOrDefault().FileName;
            var comment = new Comment()
            {
                BookID = product.BookID
            };
            return View("Index", comment);
        }

        [HttpPost]
        public ActionResult SendComment(Comment comment, double rating)
        {
            string Username = Session["username"].ToString();
            comment.UserID = db.Users.Single(a => a.Username.Equals(Username)).UserID;
            comment.Rating = rating;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "ProductDetails", new { id = comment.BookID });
        }
        public ActionResult AddCart(int quantity)
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<Cart>();
            }

            int Id = Int32.Parse(Session["ProductDetailId"].ToString());
            List<Cart> myCart = Session["Cart"] as List<Cart>;
            if (myCart.FirstOrDefault(x => x.BookId == Id) == null)
            {
                Book p = db.Books.Find(Id);
                string path = Server.MapPath("~/UploadFile");
                Cart newItem = new Cart()
                {
                    BookId = Id,
                    BookName = p.BookName,
                    Amount = quantity,
                    Price = Convert.ToDecimal(p.Price),

                    Image = "\\UploadFile" + "\\" + p.FILES.FirstOrDefault().FileName

                };
                myCart.Add(newItem);

                return RedirectToAction("Index", "Cart");
            }
            else
            {
                Cart cardItem = myCart.FirstOrDefault(m => m.BookId == Id);
                cardItem.Amount += quantity;

                return RedirectToAction("Index", "Cart");
            }

        }

    }
}