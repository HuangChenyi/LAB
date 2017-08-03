using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using MVC_LAB01.Models.Utility;
using System.Data;

namespace MVC_LAB01.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public  IQueryable<客戶資料> All(string keyWord, string 客戶分類 , string sortOrder , string sortColumn)
        {
            var data = All().Where(p => p.客戶名稱.Contains(keyWord) && (客戶分類 == "" || p.客戶分類== 客戶分類) );

            switch (sortColumn)
            {
                case "客戶名稱":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.客戶名稱);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.客戶名稱);
                    }
                    break;
                case "客戶分類":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.客戶分類);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.客戶分類);
                    }
                    break;
                case "統一編號":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.統一編號);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.統一編號);
                    }
                    break;
                case "電話":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.電話);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.電話);
                    }
                    break;
                case "傳真":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.傳真);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.傳真);
                    }
                    break;
                case "地址":
                    if (sortOrder == "DESC")
                    {
                        data = data.OrderByDescending(s => s.地址);
                    }
                    else
                    {
                        data = data.OrderBy(s => s.地址);
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
            }

                    return data;
        }

        internal 客戶資料 FindBy帳號(string 帳號)
        {
            var data = this.All().Where(p=>p.帳號 == 帳號).FirstOrDefault();

            return data;
        }

        internal MemoryStream ExportExcelStreamFromDataTable(string keyword, string 客戶分類, string sortOrder, string sortColumn)
        {
            var data = this.All(keyword, 客戶分類, sortOrder, sortColumn);
            MemoryStream ms = new MemoryStream();

            DataTable dt= ExcelUtil.ConvertObjectsToDataTable(data);
            ExcelUtil.ExportExcelFromDataTable(dt, ms);

            return ms;



        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }


        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;

            var 客戶聯絡人 = entity.客戶聯絡人;

            foreach (客戶聯絡人 item in 客戶聯絡人)
            {
                item.是否已刪除 = true;
            }


        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}