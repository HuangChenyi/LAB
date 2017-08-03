namespace MVC_LAB01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶資料Repository 客戶資料Repo = RepositoryHelper.Get客戶資料Repository();
            客戶資料 data = 客戶資料Repo.All().Where(p => p.帳號 == this.帳號 && p.Id != this.Id).FirstOrDefault();


            if (data != null || this.帳號 == "admin")
            {
                yield return new ValidationResult("帳號已有人使用", new string[] { "帳號" });
            }

            yield return ValidationResult.Success;
        }
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
        public string 客戶分類 { get; set; }
        [MaxLength(50)]
        public string 帳號 { get; set; }
        [MaxLength(255)]
        public string 密碼 { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
