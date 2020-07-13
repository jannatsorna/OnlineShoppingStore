using OnlineShoppingStore.DBTables;
using OnlineShoppingStore.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{

        //    return View();
        //}

        [HttpGet]
        public ActionResult Index()
        {
            List<Tbl_Product> _product = new List<Tbl_Product>();
           
            using (dbOnlineShoppingEntities db = new dbOnlineShoppingEntities())
            {
                _product = db.Tbl_Product.OrderBy(m=>m.ProductId).ToList();
            }

            return View(_product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}