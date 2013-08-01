using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.IDAO;
using GooCooServer.DAO;
using GooCooServer.Entity;

namespace GooCooWeb.Controllers.ajax
{
    //地址AjaxBookInfoUser/AddFavor?isbn=aaa
    public class AjaxBookInfoUserController : Controller
    {
        //
        // GET: /AjaxBookInfoUser/   
        [HttpPost]     
        public ActionResult AddFavor(string isbn)
        {
            return this.AddRelation(isbn, User_BookInfo.ERelation.FAVOR, Operation.ADD);
        }
        [HttpPost]
        public ActionResult AddOrder(string isbn)
        {
            return this.AddRelation(isbn, User_BookInfo.ERelation.ORDER, Operation.ADD);
        }

        [HttpPost]
        public ActionResult RemoveOrder(string isbn)
        {
            return this.AddRelation(isbn, User_BookInfo.ERelation.ORDER, Operation.REMOVE);
        }
        [HttpPost]
        public ActionResult RemoveFavor(string isbn)
        {
            return this.AddRelation(isbn, User_BookInfo.ERelation.FAVOR, Operation.REMOVE);
        }



        private enum Operation { ADD, REMOVE }
        private ActionResult AddRelation(string isbn, User_BookInfo.ERelation relation, Operation operation)
        {
            try
            {
                IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
                IUser_BookInfoDAO userBookInfoDAO = DAOFactory.createDAO("User_BookInfoDAO") as IUser_BookInfoDAO;

                string userSessionID = (string)Session["UserSessionID"];
                User user = userDAO.Get(userSessionID);
                if (operation == Operation.ADD)
                {
                    userBookInfoDAO.Add(isbn, user.Id, relation);
                }
                else
                {
                    userBookInfoDAO.Del(isbn, user.Id, relation);
                }
                return Json(new { result = true });
            }
            catch (Exception)
            {
                return Json(new { result = false });
            }
        }
    }
}
