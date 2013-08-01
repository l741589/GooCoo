using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GooCooWeb.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "学号")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(60, ErrorMessage = "密码必须至少包含6个字符。", MinimumLength = 6)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "下次自动登录")]
        public bool RememberMe { get; set; }
    }
}