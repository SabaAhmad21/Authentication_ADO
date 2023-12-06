using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Authentication_ADO.Models;

namespace Authentication_ADO.Controllers
{
    public class AccountsController : Controller
    {
        Account_DBEntities entity = new Account_DBEntities();

        // GET: Accounts
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User UserInfo)
        {
            entity.Users.Add(UserInfo);
            entity.SaveChanges();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {

            bool UserExists = entity.Users.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            User u = entity.Users.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode ==credentials.Password);
            if(UserExists)
            {
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }
        [HttpPost]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}