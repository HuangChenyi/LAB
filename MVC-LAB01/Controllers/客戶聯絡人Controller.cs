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

namespace MVC_LAB01.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        // private 客戶資料Entities db = new 客戶資料Entities();

     //   客戶聯絡人Repository 客戶聯絡人Repo = new 客戶聯絡人Repository();


        // GET: 客戶聯絡人
        public ActionResult Index(string keyword ="", string 職稱條件="" , string sortOrder="ASC", string sortColumn="職稱" ,int pageNo = 1 )
        {


            ViewBag.職稱列表 = 客戶聯絡人Repo.取得職稱列表();

            var 客戶聯絡人 = 客戶聯絡人Repo.All(keyword, 職稱條件  , sortColumn, sortOrder) ;

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
            return View(客戶聯絡人.ToList().ToPagedList(pageNo,5));
        }

        public ActionResult Download(string keyword = "", string 職稱條件 = "", string sortOrder = "ASC", string sortColumn = "職稱")
        {

            System.IO.MemoryStream stream = 客戶聯絡人Repo.ExportExcelStreamFromDataTable(keyword, 職稱條件, sortColumn, sortOrder);
            FileContentResult fResult = new FileContentResult(stream.ToArray(), "application/x-xlsx");
            fResult.FileDownloadName = "export.xlsx";
            return fResult;
        }

        //[HttpPost]
        // public ActionResult Index(string keyword)
        // {

        //     return View(客戶聯絡人Repo.All(keyword));
        // }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
              
                客戶聯絡人Repo.Add(客戶聯絡人);
                客戶聯絡人Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人Repo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                客戶聯絡人Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id.Value);
           
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }


            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id);
            客戶聯絡人Repo.Delete(客戶聯絡人);
            客戶聯絡人Repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶聯絡人Repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
