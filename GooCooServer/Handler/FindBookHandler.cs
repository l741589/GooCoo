using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Entity.Ex;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class FindBookHandler : IHttpHandler
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
            IBookInfoDAO bookInfoDAO = (IBookInfoDAO)DAOFactory.createDAO("BookInfoDAO");
            IBook_BookInfoDAO bb = (IBook_BookInfoDAO)DAOFactory.createDAO("Book_BookInfoDAO");

            List<BookEx> books = new List<BookEx>();
            if (bookInfoDAO != null)
            {
                try
                {

                    String keyword = context.Request["keyword"];
                    bool flag = context.Request["flag"] == "1";
                    if (keyword == null) keyword = "";
                    List<BookInfo> infos = bookInfoDAO.GetByKeyWord(keyword);
                    foreach (BookInfo info in infos)
                    {
                        BookEx bex = Util.CloneEntity<BookEx>(info);
                        if (flag)
                        {
                            bex.Books = new List<BookEx.Book>();
                            var bs = bb.GetBook(info.Isbn);
                            foreach (var b in bs)
                            {
                                BookEx.Book bk = new BookEx.Book();
                                bk.Id = b.Id;
                                bk.Owner = null;
                                bex.Books.Add(bk);
                            }
                        }
                        books.Add(bex);
                    }
                }
                catch (BMException)
                {
                    books = new List<BookEx>();
                }
            }
            context.Response.Output.Write(Util.EncodeJson(books));
        }

        #endregion
    }
}
