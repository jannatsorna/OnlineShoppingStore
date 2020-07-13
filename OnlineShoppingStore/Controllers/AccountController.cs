using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //sealed class which is used for Formsauthentication
using System.Data.Entity.Infrastructure;
using OnlineShoppingStore.DBTables;

namespace OnlineShoppingStore.Models
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // Login
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            using (var context = new dbOnlineShoppingEntities())
            {
                bool isValid = context.Tbl_Members.Any(x => x.Email == model.Email && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.FirstName, false);//(name of the user,browser a save krbo na tai false diyechi)
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }

        }
        // Sign UP
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Tbl_Members model)
        {
            using (var context = new dbOnlineShoppingEntities())
            {
                context.Tbl_Members.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        // Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}