using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooWeb.Models;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.DAO;

namespace GooCooWeb.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sessionID = ValidateUser(model.Id, model.Password);
                    Session["UserSessionID"] = sessionID;
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "SearchView");
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

    }
}
