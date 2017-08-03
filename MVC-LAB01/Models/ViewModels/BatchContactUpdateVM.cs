using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_LAB01.Models.ViewModels
{
    public class BatchContactUpdateVM
    {
       // [Required]
        public int Id { get; set; }
      //  [Required]
       // [MaxLength(50)]
        public string 職稱 { get; set; }
      //  [Required]
      //  [MaxLength(50)]
      //  [RegularExpression("\\d{4}-\\d{6}")]
        public string 手機 { get; set; }
      //  [Required]
       // [MaxLength(50)]

        public string 電話 { get; set; }
    }
}