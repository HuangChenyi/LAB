using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC_LAB01.Models;
using System.Web.Security;

namespace MVC_LAB01.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel data)
        {
            // 登入時清空所有 Session 資料

            string controlName = "Home";
            string actionName = "Index";
            object routeValue = null;

            if (CheckLogin(data))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Account, false);

                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = null;

                if (data.Account == "admin")
                {
                    ticket = new FormsAuthenticationTicket(1,
                      data.Account,
                      DateTime.Now,
                      DateTime.Now.AddMinutes(30),
                      isPersistent,
                      "admin",
                      FormsAuthentication.FormsCookiePath);


                }
                else
                {
                    ticket = new FormsAuthenticationTicket(1,
                         data.Account,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(30),
                         isPersistent,
                         "Cust",
                         FormsAuthentication.FormsCookiePath);


                    controlName = "EditProfile";
                    actionName = "EditProfile";

                    routeValue = new { Account = data.Account };
                }

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));




                return RedirectToAction(actionName, controlName, routeValue);
            }

            return View();
        }

        private bool CheckLogin(LoginViewModel data)
        {
  


            if (data.Account == "admin" && data.Password == "123456")
            {

                return true;
            }

     

            return false;

        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel data)
        {
            if (ModelState.IsValid)
            {
                // TODO

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }


    }
}