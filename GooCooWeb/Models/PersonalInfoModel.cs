using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace GooCooWeb.Models
{
    public class PersonalInfoModel : RegisterModel
    {
        public enum EAuthority { USER, ADMIN, SUPERADMIN}

        [Display(Name = "用户等级")]
        public EAuthority authority;

        [Display(Name = "违规天次")]
        public int repvalue;
    }
}