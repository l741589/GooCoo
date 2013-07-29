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
    public class GetBookByBorrow : IHttpHandler
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
            IUser_BookDAO ub = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            IBook_BookInfoDAO bb = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
            HashSet<BookEx> books = new HashSet<BookEx>();
            String user_id=context.Request["user"];
            if (bb != null && ub != null)
            {
                List<Book> lbs = ub.GetBook(user_id);
                foreach (var e in lbs)
                {
                    BookEx bookex = Util.CloneEntity<BookEx>(bb.GetBookInfo(e.Id));
                    //bookex.Owner_id = user_id;
                    books.Add(bookex);
                }
            }
            else
            {
                BookEx book;
                book = new BookEx();
                book.Isbn = "wwwewew32ee2";
                book.Name = "sdfergw34sdsfdd";
                //book.Tags = new String[] { "4wwwwwe", "dffdfdf" };
                book.Timestamp = DateTime.UtcNow;
                //book.Owner_id = user_id;
                books.Add(book);

                book = new BookEx();
                book.Isbn = "sd34t344rt3e";
                book.Name = "供热为复位";
                //book.Tags = new String[] { "扔给我让我swe", "是否跟", "送给我" };
                book.Timestamp = DateTime.UtcNow;
                //book.Owner_id = user_id;
                books.Add(book);

                book = new BookEx();
                book.Isbn = "2323ewew3232";
                book.Name = "sdfergw34fdd";
                //book.Tags = new String[] { "432433232we", "dffdfdf" };
                book.Timestamp = DateTime.UtcNow;
                //book.Owner_id = user_id;
                books.Add(book);
            }
            context.Response.Output.Write(Util.EncodeJson(books));
        }

        #endregion
    }
}