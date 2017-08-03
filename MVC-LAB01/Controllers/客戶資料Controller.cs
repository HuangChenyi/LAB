using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_LAB01.Models;
using PagedList;
using MVC_LAB01.Models.ViewModels;
using System.Web.Security;

namespace MVC_LAB01.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        // private 客戶資料Entities db = new 客戶資料Entities();


        SelectList 客戶分類列表 = null;

        public 客戶資料Controller()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "VIP", Text = "VIP" });
            list.Add(new SelectListItem() { Value = "好客戶", Text = "好客戶" });
            list.Add(new SelectListItem() { Value = "壞客戶", Text = "壞客戶" });
            客戶分類列表 = new SelectList(list, "Value", "Text", "客戶分類");
        }

        [HttpPost]
        public ActionResult BatchUpdateContact(BatchContactUpdateVM[] data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var 客戶聯絡人 = 客戶聯絡人Repo.Find(item.Id);
                    客戶聯絡人.職稱 = item.職稱;
                    客戶聯絡人.手機 = item.手機;
                    客戶聯絡人.電話 = item.電話;
                }

                客戶聯絡人Repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            //    ViewData.Model = repo.GetTOP10ProductData();

            return View();
        }

        public ActionResult Download(string keyword = "", string 客戶分類 = "", string sortOrder = "", string sortColumn = "客戶名稱")
        {

            System.IO.MemoryStream stream = 客戶資料Repo.ExportExcelStreamFromDataTable(keyword, 客戶分類, sortOrder, sortColumn);
            FileContentResult fResult = new FileContentResult(stream.ToArray(), "application/x-xlsx");
            fResult.FileDownloadName = "export.xlsx";
            return fResult;
        }

        // GET: 客戶資料
        public ActionResult Index(string keyword = "", string 客戶分類 = "", string sortOrder = "", string sortColumn = "客戶名稱", int pageNo = 1)
        {

            ViewBag.客戶分類 = 客戶分類列表;

            var 客戶資料 = 客戶資料Repo.All(keyword, 客戶分類, sortOrder, sortColumn);

            if (sortOrder == "DESC")
            {
                sortOrder = "ASC";
            }
            else
            {
                sortOrder = "DESC";
            }


            ViewBag.sort = sortOrder;
            ViewBag.pageNo = pageNo;
            return View(客戶資料.ToList().ToPagedList(pageNo, 5));


        }

        public ActionResult ContactList(int Id)
        {
            var data = 客戶資料Repo.Find(Id).客戶聯絡人;
            return View(data.ToList());
        }



        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = 客戶分類列表;

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            ViewBag.客戶分類 = 客戶分類列表;
            if (ModelState.IsValid)
            {
                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號 + 客戶資料.密碼, "SHA1");
                客戶資料Repo.Add(客戶資料);
                客戶資料Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find(id.Value);
            ViewBag.客戶分類 = 客戶分類列表;
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            ViewBag.客戶分類 = 客戶分類列表;
            if (ModelState.IsValid)
            {
                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號 + 客戶資料.密碼, "SHA1");
                客戶資料Repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                客戶資料Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "Id,電話,傳真,地址,Email,密碼")] 客戶資料VM 客戶資料VM)
        {
            var 客戶資料 = 客戶資料Repo.Find(客戶資料VM.Id);
            if (ModelState.IsValid)
            {
                客戶資料.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(客戶資料.帳號 + 客戶資料VM.密碼, "SHA1");
                客戶資料.電話 = 客戶資料VM.電話;
                客戶資料.傳真 = 客戶資料VM.傳真;
                客戶資料.地址 = 客戶資料VM.地址;
                客戶資料.Email = 客戶資料VM.Email;
                客戶資料Repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                客戶資料Repo.UnitOfWork.Commit();
                return View(客戶資料);
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            客戶資料Repo.Delete(客戶資料);
            客戶資料Repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶資料Repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult EditData()
        {
            客戶資料 客戶資料 = 客戶資料Repo.FindBy帳號(User.Identity.Name);
            return View(客戶資料);
        }


    }
}
