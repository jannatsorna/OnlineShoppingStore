using OnlineShoppingStore.DBTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class SlideImageController : Controller
    {
        // GET: SlideImage
        public ActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddImage(Tbl_SlideImage img)
        {
            string filename = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string extension = Path.GetExtension(img.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            img.SlideImage = "~/IMG/" + filename;
            filename = Path.Combine(Server.MapPath("~/IMG/"), filename);
            img.ImageFile.SaveAs(filename);
            using (dbOnlineShoppingEntities db = new dbOnlineShoppingEntities())
            {
                db.Tbl_SlideImage.Add(img);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult ImageView(int id)
        {
            Tbl_SlideImage img = new Tbl_SlideImage();
            using (dbOnlineShoppingEntities db = new dbOnlineShoppingEntities())
            {
                img = db.Tbl_SlideImage.Where(x => x.SlideId == id).FirstOrDefault();
            }
            return View(img);
        }
    }
}