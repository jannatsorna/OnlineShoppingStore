using OnlineShoppingStore.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        private dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();  // Connection String
        
        public IEnumerable<Tbl_Product> ListofProducts { get; set; }
        public HomeIndexViewModel CreateModel()
        {
            return new HomeIndexViewModel
            {
                ListofProducts = db.Tbl_Product.ToList()
            };
        }
    }
}