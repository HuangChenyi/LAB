using MVC_LAB01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_LAB01.Controllers
{
    [Authorize(Roles = "Cust,admin" )]
    public class BaseController : Controller
    {
        // GET: Base
        protected 客戶資料Repository 客戶資料Repo
                      = RepositoryHelper.Get客戶資料Repository();

        //protected 客戶清單資訊Repository repo客戶清單資訊
        //   = RepositoryHelper.Get客戶清單資訊Repository();

        protected 客戶銀行資訊Repository 客戶銀行資訊Repo
           = RepositoryHelper.Get客戶銀行資訊Repository();

        protected 客戶聯絡人Repository 客戶聯絡人Repo
           = RepositoryHelper.Get客戶聯絡人Repository();


        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index", "Home")
                .ExecuteResult(this.ControllerContext);
        }
    }
}