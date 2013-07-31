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
    public class AjaxCommentController : Controller
    {
        //
        // POST: /AjaxComment/

        [HttpPost]
        public ActionResult AddComment(string content, string isbn)
        {
            try
            {
                IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;                
                string userSessionID = (string)Session["UserSessionID"];
                User user = userDAO.Get(userSessionID);

                ICommentDAO commentDAO = DAOFactory.createDAO("CommentDAO") as ICommentDAO;
                IUser_CommentDAO user_commentDAO = DAOFactory.createDAO("User_CommentDAO") as IUser_CommentDAO;                
                Comment comment = new Comment();
                comment.Content = content;                
                commentDAO.Add(comment, isbn, user.Id);
                //commentDAO.Add(comment, isbn, userSessionID);


                return Json(new { result = true, content = comment.Content, userName = user.Name, time = Convert.ToString(comment.Timestamp) });
            }
            catch (Exception)
            {
                return Json(new { result = false });
            }
        }
    }
}
