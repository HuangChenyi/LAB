namespace MVC_LAB01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料
    {
    }
    
    public partial class 客戶資料MetaData
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string 客戶名稱 { get; set; }
        [Required]
        [MaxLength(8)]
        public string 統一編號 { get; set; }
        [Required]
        [MaxLength(50)]
        public string 電話 { get; set; }
        [MaxLength(50)]
        public string 傳真 { get; set; }
        [MaxLength(100)]
        public string 地址 { get; set; }
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        public bool 是否已刪除 { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
