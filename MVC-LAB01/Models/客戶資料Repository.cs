using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_LAB01.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public  IQueryable<客戶資料> All(string keyWord)
        {
            return All().Where(p => p.客戶名稱.Contains(keyWord));
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