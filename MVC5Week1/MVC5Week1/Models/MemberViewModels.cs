using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Week1.Models
{
    public class MemberViewModels
    {
        [Required]
        public string 帳號 { get; set; }


        [Required]
        public string 密碼 { get; set; }
    }
}