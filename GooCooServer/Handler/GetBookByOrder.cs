﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;

namespace GooCooServer.Handler
{
    public class GetBookByOrder : IHttpHandler
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            IUser_BookInfoDAO ub = DAOFactory.createDAO("User_BookInfoDAO") as IUser_BookInfoDAO;
            List<BookInfo> books = new List<BookInfo>();
            if (ub != null)
            {
                List<BookInfo> lbs = ub.GetBookInfo(context.Request["user"]);
            }
            else
            {
                if (context.Request["user"] == null) throw new BMException("参数错误");
                BookInfo book;
                book = new BookInfo();
                book.Isbn = "wwweweeww32ee2";
                book.Name = "sdfergw34sdsfdd";
                book.Tags = new String[] { "4wwwwwe", "dffdfdf" };
                book.Timestamp = 322343423243;
                books.Add(book);

                book = new BookInfo();
                book.Isbn = "sd34t344rt3e";
                book.Name = "供sa热为复se位";
                book.Tags = new String[] { "扔给我让我swe", "是否跟", "送给我" };
                book.Timestamp = DateTime.UtcNow.Ticks;
                books.Add(book);

                book = new BookInfo();
                book.Isbn = "额";
                book.Name = "而谷歌";
                book.Tags = new String[] { "432433232we", "dffdfdf" };
                book.Timestamp = 232546788755455657L;
                books.Add(book);
            }
            StringBuilder ret = new StringBuilder();
            new JavaScriptSerializer().Serialize(books, ret);
            context.Response.Output.Write(ret.ToString());
        }

        #endregion
    }
}
