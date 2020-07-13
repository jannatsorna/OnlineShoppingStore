using OnlineShoppingStore.DBTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        private dbOnlineShoppingEntities db = new dbOnlineShoppingEntities();  // Connection String


         // For Category
        // Show All Category List

        public ActionResult Categories_List()
        {
            return View(db.Tbl_Category.ToList());
        }
        
        // Show Details 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Category _cat = db.Tbl_Category.Find(id);
            if (_cat == null)
            {
                return HttpNotFound();
            }
            return View(_cat);
        }

        // Add or Create Category

        public ActionResult AddCategory()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AddCategory([Bind(Include = "CategoryId,CategoryName,IsActive,IsDelete")]Tbl_Category _cat)
        {
            if(ModelState.IsValid)
            {
                db.Tbl_Category.Add(_cat);
                db.SaveChanges();
                return RedirectToAction("Categories_List");
            }
            return View(_cat);
        }
        
        // Update Category
        public ActionResult UpdateCategory(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tbl_Category _cat = db.Tbl_Category.Find(id);
            if(_cat == null)
            {
                return HttpNotFound();
            }

            return View(_cat);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory([Bind(Include = "CategoryId,CategoryName,IsActive,IsDelete")]Tbl_Category _cat)
        { 
            if (ModelState.IsValid)
            {
                db.Entry(_cat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categories_List");
            }
            return View(_cat);
        }

        // Delete Category

        public ActionResult DeleteCategory(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tbl_Category _cat = db.Tbl_Category.Find(id);
                if (_cat == null)
                {
                    return HttpNotFound();
                }
                return View(_cat);
            }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Category employee = db.Tbl_Category.Find(id);
            db.Tbl_Category.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Categories_List");
        }
        /*
                [HttpGet]
                //   [Authorize(Roles = "Admin, Customer")]
                public ActionResult UpdateCategory(int id)
                {
                    using (dbOnlineShoppingEntities dbmodel = new dbOnlineShoppingEntities())
                    {
                        var update = dbmodel.Tbl_Category.Where(u => u.CategoryId == id).FirstOrDefault();
                        return View();
                    }
                }
                //after change or update data it should be saved in DB so it comes in this portion with [HttpPost]

                [HttpPost]
                //   [Authorize(Roles = "Admin, Customer")]

                public ActionResult Update(int id, Tbl_Category usermodel)
                {
                    try
                    {
                        using (dbOnlineShoppingEntities dbmodel = new dbOnlineShoppingEntities())
                        {
                            dbmodel.Entry(usermodel).State = EntityState.Modified;
                            dbmodel.SaveChanges();
                        }
                        return RedirectToAction("Caterogies_List");//after update,the updated value shown in List,so back to /User_Info/List

                    }
                    catch
                    {
                        return View(); //Update.cshtml pg e rtn krbe
                    }

                }
        */

        // For Product
        // Show All Product List

        public ActionResult Product_List()

        {
            try
            {
                return View(db.Tbl_Product.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // Show Details 
        public ActionResult Product_Details(int? id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = db.Tbl_Category.OrderBy(m => m.CategoryId).ToList();//Find(id);
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        */

        // Add or Create Products

        public ActionResult AddProducts()   
        {
            //   ViewBag.CategoeyList = GetCategory();  
            //   ViewBag.CategoryList=db.Tbl_Category.OrderBy(m=>m.CategoryId).ToList();

            var CategoryList = db.Tbl_Category.ToList();
            ViewBag.CategoryList = new SelectList(CategoryList, "CategoryId", "CategoryName"); // show categoryname in dropdown list
            return View();
        }

        [HttpPost]
        public ActionResult AddProducts(Tbl_Product _product)
        {
            string filename = Path.GetFileNameWithoutExtension(_product.ImageFile.FileName);
            string extension = Path.GetExtension(_product.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            _product.ProductImage = "~/IMG/" + filename;
            filename = Path.Combine(Server.MapPath("~/IMG/"), filename);
            _product.ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
               
                _product.CreatedDate = DateTime.Now; // to show current time
                db.Tbl_Product.Add(_product);
                db.SaveChanges();
                return RedirectToAction("Product_List");
            }
            ModelState.Clear();
            return View(_product);
        }
        [HttpGet]
        public ActionResult ViewImageofProducts(Tbl_Product _product,int id)
        {
           // Tbl_Product _product = new Tbl_Product();

            using (dbOnlineShoppingEntities db = new dbOnlineShoppingEntities())
            {
                _product = db.Tbl_Product.Where(x => x.ProductId == id).FirstOrDefault();
            }

            return View(_product);
        }


        // Update Product
        public ActionResult UpdateProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var CategoryList = db.Tbl_Category.ToList();
            ViewBag.CategoryList = new SelectList(CategoryList, "CategoryId", "CategoryName");

            Tbl_Product _product = db.Tbl_Product.Find(id);
            if (_product == null)
            {
                return HttpNotFound();
            }

            return View(_product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(Tbl_Product _product)
        {
            string filename = Path.GetFileNameWithoutExtension(_product.ImageFile.FileName);
            string extension = Path.GetExtension(_product.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            _product.ProductImage = "~/IMG/" + filename;
            filename = Path.Combine(Server.MapPath("~/IMG/"), filename);
            _product.ImageFile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                _product.Modified = DateTime.Now;
                db.Entry(_product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product_List");
            }
            return View(_product);
        }

        // Delete Category

        public ActionResult DeleteProduct(int? id)
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

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin")]
        public ActionResult _DeleteConfirmed(int id)
        {
            Tbl_Product _product = db.Tbl_Product.Find(id);
            db.Tbl_Product.Remove(_product);
            db.SaveChanges();
            return RedirectToAction("Product_List");
        }

       

    }

}



