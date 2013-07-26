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
    public class GetUserByBorrow : IHttpHandler
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
            List<UserEx> users = new List<UserEx>();
            BookEx book = new BookEx();
            book.Books = new List<BookEx.Book>();
            String book_isbn = context.Request["isbn"];
            if (bb != null && ub != null)
            {
                List<Book> lbs = bb.GetBook(book_isbn);
                book.Count = lbs.Count;
                foreach (var e in lbs)
                {
                    User owner = ub.GetUser(e.Id);
                    if (owner == null)
                    {
                        BookEx.Book b = new BookEx.Book();
                        b.Id = e.Id;
                        b.Owner = null;
                        book.Books.Add(b);
                    }
                    else
                    {
                        UserEx u = Util.CloneEntity<UserEx>(owner);
                        u.Holds.Add(book_isbn);
                        users.Add(u);
                        BookEx.Book b = new BookEx.Book();
                        b.Id = e.Id;
                        b.Owner = u.Id;
                        book.Books.Add(b);
                    }
                }
            }
            else
            {
                UserEx user = new UserEx();
                user.Id = "1123435";
                user.Name = "ewrwrew";
                user.Authority = UserEx.EAuthority.USER;
                users.Add(user);
                BookEx.Book b = new BookEx.Book();
                b.Id = 23;
                b.Owner = user.Id;
                book.Books.Add(b);

                user = new UserEx();
                user.Id = "2342132";
                user.Name = "书而已";
                user.Authority = UserEx.EAuthority.USER;
                users.Add(user);
                b = new BookEx.Book();
                b.Id = 24;
                b.Owner = user.Id;
                book.Books.Add(b);

                user = new UserEx();
                user.Id = "1123345";
                user.Name = "孙建华";
                user.Authority = UserEx.EAuthority.ADMIN;
                users.Add(user);
                b = new BookEx.Book();
                b.Id = 25;
                b.Owner = null;
                book.Books.Add(b);

                book.Count = 3;
            }
            context.Response.Output.Write(Util.EncodeJson(users,book));
        }
        #endregion
    }
}
