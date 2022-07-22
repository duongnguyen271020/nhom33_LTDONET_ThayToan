using BookShopOnline_ProjectSem3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;

namespace BookShopOnline_ProjectSem3.Controllers
{
    public class CheckOutController : Controller
    {
        private BookDB_DemoEntities db = new BookDB_DemoEntities();
        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProceedToOrder(decimal totalPrice )
        {

            User user = new User();
            user = Session["Client"] as User;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session["Orders"] = new Order();
            Order orders = Session["Orders"] as Order;
            orders.TotalPrice = totalPrice;
            orders.Address = user.Address;
            orders.Email = user.Email;
            orders.Phone = user.Phone;
            
            //orders.CreatedDate = DateTime.Now;

            Session["OrderDetails"] = new List<OrderDetail>();
            List<OrderDetail> orderDetails = Session["OrderDetails"] as List<OrderDetail>;

            List<Cart> cartCheckOut = new List<Cart>();
            cartCheckOut = Session["Cart"] as List<Cart>;

            foreach (var item in cartCheckOut)
            {
                OrderDetail itemOrderDetail = new OrderDetail()
                {
                    BookID = item.BookId,
                    ProductName = item.BookName,
                    Quantity = item.Amount,
                    Price = item.Price,
                    OrderID = orders.OrderID,
                    UserID = user.UserID
                    
                };
                orderDetails.Add(itemOrderDetail);
            }



            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CheckoutOrders(string otherAddress, string otherEmail, string otherPhone)
        {
            if(otherAddress != "" && otherEmail != "" && otherPhone != "")
            {
                Order orders = Session["Orders"] as Order;
                Order otherOrders = new Order();
                otherOrders.Address = otherAddress;
                otherOrders.Email = otherEmail;
                otherOrders.Phone = otherPhone;
                otherOrders.TotalPrice = orders.TotalPrice;
                otherOrders.CreatedDate = DateTime.Now;
                otherOrders.Status = false;
                db.Orders.Add(otherOrders);
            }
            else
            {
                Order orders = Session["Orders"] as Order;
                orders.CreatedDate = DateTime.Now;
                orders.Status = false;
                db.Orders.Add(orders);
                
            }
            
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            orderDetails = Session["OrderDetails"] as List<OrderDetail>;
            foreach (var item in orderDetails)
            {
                db.OrderDetails.Add(item);
            }

            db.SaveChanges();
            
            //Session["Orders"].Clear();
            Session.Remove("Orders");
            Session.Remove("OrderDetails");
            Session.Remove("Cart");

            

            return RedirectToAction("Index", "Home");
        }
    }
}