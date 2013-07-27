using System;
using System.Collections.Generic;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Entity.Ex;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class GetBookByFavor : IHttpHandler
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
            List<BookEx> books = new List<BookEx>();
            String user_id = context.Request["user"];
            if (ub != null)
            {
                List<BookInfo> lbs = ub.GetBookInfo(user_id,User_BookInfo.ERelation.FAVOR);
                foreach (var e in lbs)
                {
                    BookEx b = Util.CloneEntity<BookEx>(e);
                    books.Add(b);
                }
            }
            else
            {
                if (user_id == null||user_id=="") throw new BMException("参数错误");
                BookEx book;
                book = new BookEx();
                book.Isbn = "12133232432";
                book.Name = "sdfergw34sdsfddew";
              //  book.Tags = new String[] { "4wwwwwe", "dffdfdf" };
            //    book.Timestamp = 322343423243;
                books.Add(book);

                book = new BookEx();
                book.Isbn = "ilukiukjh";
                book.Name = "供sa热为复se位we";
            //    book.Tags = new String[] { "扔给我让我swe", "是否跟", "送给我" };
            //    book.Timestamp = DateTime.UtcNow.Ticks;
                books.Add(book);

                book = new BookEx();
                book.Isbn = "额bbn,.,/,";
                book.Name = "而谷歌sdsd";
           //     book.Tags = new String[] { "432433232we", "dffdfdf" };
           //     book.Timestamp = 232546788755455657L;
                books.Add(book);
            }
            context.Response.Output.Write(Util.EncodeJson(books));
        }

        #endregion
    }
}
