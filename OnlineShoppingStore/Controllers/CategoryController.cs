using OnlineShoppingStore.DBTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class CategoryController : Controller
    {
        private dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();
        // Show List
        public ActionResult Categories()
        {
            return View(db.Tbl_Category.ToList());
        }
    }
}