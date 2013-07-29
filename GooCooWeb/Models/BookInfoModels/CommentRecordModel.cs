using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;
using GooCooServer.IDAO;
using GooCooServer.DAO;

namespace GooCooWeb.Models.BookInfoModels
{
    public class CommentRecordModel
    {
        public Comment Content { get; set; }        //评论内容
        public User CommentMaker { get; set; }      //评论者


        //转换函数
        public static List<CommentRecordModel> toRecord(List<Comment> commentList)
        { 
            List<CommentRecordModel> resultList = new List<CommentRecordModel>();
            IUser_CommentDAO dao = DAOFactory.createDAO("User_CommentDAO") as IUser_CommentDAO;
            //ICommentDAO commentDAO = DAOFactory.createDAO("CommentDAO") as ICommentDAO;

            foreach (Comment comment in commentList)
            {
                CommentRecordModel record = new CommentRecordModel();
                record.Content = comment;
                User user = null;
                try
                {
                    user = dao.GetUser(comment.Id);
                }
                catch (Exception) { }
                record.CommentMaker = user;

                resultList.Add(record);
            }
            return resultList;
        }
    }
}