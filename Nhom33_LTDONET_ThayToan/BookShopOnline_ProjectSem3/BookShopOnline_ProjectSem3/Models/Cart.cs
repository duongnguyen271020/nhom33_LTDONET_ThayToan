using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopOnline_ProjectSem3.Models
{
    public class Cart
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public decimal Total
        {
            get
            {
                return Price * Amount;
            }
        }
    }
}