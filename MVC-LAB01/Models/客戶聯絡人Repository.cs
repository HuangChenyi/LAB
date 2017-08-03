using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Data;
using MVC_LAB01.Models.Utility;

namespace MVC_LAB01.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include("客戶資料").Where(p => p.是否已刪除 == false );
        }

        public IEnumerable<客戶聯絡人> All(string keyworkd, string 職稱  , string sortColumn , string sortOrder)
        {
            var data  = this.All().Where(p => p.姓名.Contains(keyworkd) && (職稱 =="" || p.職稱 == 職稱)).OrderBy(p=>p.客戶Id);

            switch (sortColumn)
            {
                case "職稱":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.職稱);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.職稱);
                    }
                    break;
                case "Email":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.Email);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.Email);
                    }
                    break;
                case "姓名":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.姓名);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.姓名);
                    }
                    break;
                case "手機":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.手機);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.手機);
                    }
                    break;
                case "客戶名稱":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.客戶資料.客戶名稱);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.客戶資料.客戶名稱);
                    }
                    break;
                case "電話":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.電話);
                        sortOrder = "ASC";
                    }
                    else
                    {
                        data = data.OrderBy(s => s.電話);                    }
                    break;

            }
            return data;
        }



        internal MemoryStream ExportExcelStreamFromDataTable(string keyword, string 職稱條件, string sortColumn, string sortOrder)
        {
            var data = this.All(keyword, 職稱條件, sortColumn, sortOrder);
            MemoryStream ms = new MemoryStream();

            DataTable dt = ExcelUtil.ConvertObjectsToDataTable(data);
            ExcelUtil.ExportExcelFromDataTable(dt, ms);

            return ms;

        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().Include("客戶資料").FirstOrDefault(p => p.Id == id);

        }

     
        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }

        internal dynamic 取得職稱列表()
        {
            return this.All()
           .AsQueryable()
           .Select(s => new SelectListItem()
           {
               Value = s.職稱,
               Text = s.職稱
           }).Distinct()
                ;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}