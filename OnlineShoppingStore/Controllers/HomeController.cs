using OnlineShoppingStore.DBTables;
using OnlineShoppingStore.Models.Home;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        // This portion for pagination
        [HttpGet]
        public ActionResult Index(int? page)
        {
            List<Tbl_Product> _product = new List<Tbl_Product>();
            using (dbOnlineShoppingEntities db = new dbOnlineShoppingEntities())
            {
                int pagesize = 4, pageindex = 1;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
                //var list = db.Tbl_Product.OrderByDescending(x => x.ProductId).ToList();
                _product = db.Tbl_Product.OrderBy(m=>m.ProductId).ToList();
                IPagedList<Tbl_Product> _obj = _product.ToPagedList(pageindex, pagesize);
                return View(_obj);
            }

           // return View(_product);
        }

        // Add item into shoppingcart
        public ActionResult AddToCart(int productId)
        {
            dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();
            if(Session["cart"]==null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
        /*    else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;

            }
            return Redirect("Index"); */

            
             else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.Tbl_Product.Find(productId);
                //removing duplicate item from cart 
                foreach(var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int preQuantity = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = preQuantity + 1
                        });
                        break;                   
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        break;
                    }
                }//removing duplicate item from cart end....

                //cart.Add(new Item()
                //{
                //    Product = product,s
                //    Quantity = 1
                //});
                Session["cart"] = cart;

            }
            return Redirect("Index");

            

        }

        public ActionResult RemoveFromCart(int productId)
        {
          //  dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();
            List<Item> cart = (List<Item>)Session["cart"];
          //  var product = db.Tbl_Product.Find(productId);
            foreach(var item in cart)
            {
                if(item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;

            return Redirect("Index");
        }
    }
}