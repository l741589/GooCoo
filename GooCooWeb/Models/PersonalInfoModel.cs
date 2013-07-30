using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using GooCooServer.Entity;


namespace GooCooWeb.Models
{
    public class PersonalInfoModel : RegisterModel
    {
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
            //this.PhoneNumer = user.PhoneNumber;
            //this.Email = user.Email;
            this.Authority = (EAuthority)user.Authority;
            this.Repvalue = user.Repvalue;
        }
    }
}