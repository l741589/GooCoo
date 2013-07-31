using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using GooCooServer.DAO;
using GooCooServer.IDAO;

namespace GooCooWeb.Models
{
    public class RegisterModel : IValidatableObject
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

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            if (userDAO.isExist(Id))
            {
                yield return new ValidationResult("该用户已存在！", new string[] { "Id" });
            }
        }

        #endregion
    }
}