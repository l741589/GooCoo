using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GooCooWeb.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "学号")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "手机号")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "请输入11位手机号码!")]
        public string PhoneNumer { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage="请输入正确的邮箱格式！")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "密码必须至少包含6个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}