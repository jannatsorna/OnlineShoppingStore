using OnlineShoppingStore.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomePgProductDetailsController : Controller
    {
        private dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();  // Connection String
        // GET: HomePgProductDetails

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Product _product = db.Tbl_Product.Find(id);
            if (_product == null)
            {
                return HttpNotFound();
            }
            return View(_product);      
        }
    }
}

