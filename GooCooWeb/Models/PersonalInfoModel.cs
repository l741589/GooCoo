using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using GooCooServer.Entity;


namespace GooCooWeb.Models
{
    public class PersonalInfoModel
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
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "请输入正确的邮箱格式！")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "密码必须至少包含6个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public enum EAuthority 
        { 
            [Display(Name = "普通用户")]
            USER, 
            [Display(Name = "管理员")]
            ADMIN, 
            [Display(Name = "超级管理员")]
            SUPERADMIN
        }

        [Display(Name = "用户等级")]
        public EAuthority Authority { get; set; }

        [Display(Name = "违规天次")]
        public int Repvalue { get; set; }

        public int BorrowBookNumer { get; set; }
        public int DonateBookNumer { get; set; }

        public PersonalInfoModel()
        {
        }

        public PersonalInfoModel(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.PhoneNumber = user.Phonenumber;
            this.Email = user.Email;
            this.Authority = (EAuthority)user.Authority;
            this.Repvalue = user.Repvalue;
        }
    }
}