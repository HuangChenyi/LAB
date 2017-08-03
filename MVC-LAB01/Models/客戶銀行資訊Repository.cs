using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Data;
using MVC_LAB01.Models.Utility;

namespace MVC_LAB01.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Include("客戶資料").Where(p => p.是否已刪除 == false);
        }

        public IQueryable<客戶銀行資訊> All(string keyword)
        {
            return All().Where(p => p.銀行名稱.Contains(keyword));
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Include("客戶資料").FirstOrDefault(p => p.Id == id);
        }

        internal MemoryStream ExportExcelStreamFromDataTable(string keyword)
        {
            var data = this.All(keyword);
            MemoryStream ms = new MemoryStream();

            DataTable dt = ExcelUtil.ConvertObjectsToDataTable(data);
            ExcelUtil.ExportExcelFromDataTable(dt, ms);

            return ms;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}