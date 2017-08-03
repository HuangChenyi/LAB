using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_LAB01.Models.ViewModels
{
    public class 客戶資料VM
    {
        public int Id { get; set; }
     
        public string 電話 { get; set; }
        public string 傳真 { get; set; }
        public string 地址 { get; set; }
        public string Email { get; set; }
    
        public string 密碼 { get; set; }


    }
}