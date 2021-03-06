﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooWeb.Models;
using System.Web.Routing;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.DAO;
using GooCooServer.Entity;

namespace GooCooWeb.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [LoggedOnFilter]
        public ActionResult LogOn()
        {
            return View();
        }

        [LoggedOnFilter]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sessionID = ValidateUser(model.Id, model.Password);
                    Session["UserSessionID"] = sessionID;
                    if (model.RememberMe)
                    {
                        var cookie = new HttpCookie("UserSessionID");
                        cookie.Value = sessionID;
                        Response.Cookies.Add(cookie);
                    }
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (BMException ex)
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        private String ValidateUser(String id, String password)
        {
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            return userDAO.Login(id, password);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                // 尝试注册用户
                User newUser = new User();
                newUser.Id = model.Id;
                newUser.Name = model.Name;
                newUser.Password = model.Password;
                newUser.Phonenumber = model.PhoneNumber;
                newUser.Email = model.Email;
                newUser.Repvalue = 0;
                newUser.Authority = GooCooServer.Entity.User.EAuthority.USER;

                IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
                try
                {
                    userDAO.Add(newUser);
                    LogOnModel logOnModel = new LogOnModel();
                    logOnModel.Id = newUser.Id;
                    logOnModel.Password = newUser.Password;
                    logOnModel.RememberMe = false;
                    return LogOn(logOnModel, returnUrl);
                }
                catch (BMException)
                {
                    ModelState.AddModelError("", "该学号已被注册");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View();
        }

        public class LoggedOnFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
                if (filterContext.HttpContext.Session["UserSessionID"] != null)
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
            }
        }

        public ActionResult LogOut(String returnUrl)
        {
            Session["UserSessionID"] = null;
            if (Request.Cookies["UserSessionID"] != null)
            {
                HttpCookie cookie = new HttpCookie("UserSessionID");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            return Redirect(returnUrl);
        }
    }
}