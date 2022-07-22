using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class CartController : Controller
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCart(int Id)
        {
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<Cart>();
            }
            List<Cart> myCart = Session["Cart"] as List<Cart>;
            if (myCart.FirstOrDefault(x => x.BookId == Id) == null)
            {
                Book p = db.Books.Find(Id);
                string path = Server.MapPath("~/UploadFile");
                Cart newItem = new Cart()
                {
                    BookId = Id,
                    BookName = p.BookName,
                    Amount = 1,
                    Price = Convert.ToDecimal(p.Price),

                    Image = "\\UploadFile" + "\\" + p.FILES.FirstOrDefault().FileName

                };
                myCart.Add(newItem);
                
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                Cart cardItem = myCart.FirstOrDefault(m => m.BookId == Id);
                int NewAmount = cardItem.Amount++;
                
                return RedirectToAction("Index", "Cart");
            }

        }
        public ActionResult UpdateCart(int BookId, int NewAmount, string btnSubmit)
        {
            
            List<Cart> myCard = Session["Cart"] as List<Cart>;
            Cart item = myCard.FirstOrDefault(m => m.BookId == BookId);
            if (item != null)
            {
                if (btnSubmit == "Update")
                {
                    item.Amount = NewAmount;
                }
                else if (btnSubmit == "Delete")
                {
                    myCard.Remove(item);
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteItem(int BookId)
        {
            List<Cart> myCart = Session["Cart"] as List<Cart>;
            Cart itemDelete = myCart.FirstOrDefault(m => m.BookId == BookId);
            if (itemDelete != null)
            {
                myCart.Remove(itemDelete);
            }
            return RedirectToAction("Index");
        }
    }
}