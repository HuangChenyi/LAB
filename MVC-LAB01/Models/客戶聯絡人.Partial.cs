namespace MVC_LAB01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶聯絡人Repository 客戶聯絡人Repo = RepositoryHelper.Get客戶聯絡人Repository();
            客戶聯絡人 data = 客戶聯絡人Repo.Where(p => p.Email == this.Email && 
            p.客戶Id == this.客戶Id && (p.是否已刪除 == false )).Where(p => p.Id != this.Id).
            FirstOrDefault();


            if (data != null)
            {
                yield return new ValidationResult("Email已有人使用", new string[] { "Email" });
            }

            yield return ValidationResult.Success;
        }
    }

        public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string 職稱 { get; set; }
        [Required]
        [MaxLength(50)]
        public string 姓名 { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression("\\d{4}-\\d{6}")]
        public string 手機 { get; set; }
        [Required]
        [MaxLength(50)]
        public string 電話 { get; set; }
        public bool 是否已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
