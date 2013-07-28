using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Entity.Ex;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class UpdateBook : IHttpHandler
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

        private void HandleBook(BookEx book , bool test = false)
        {
            IBookDAO bdao = null;
            if (!test) bdao = DAOFactory.createDAO("BookDAO") as IBookDAO;
            if (book.Count < book.RealCount)
            {
                if (book.Count < book.BorrowedBook)
                {
                    throw new BMException("部分书被用户持有，无法删除。");
                }
                else
                {
                    Debug.Assert(book.Books != null, "书数量为空");
                    BookEx.Book[] books = book.Books.ToArray();
                    foreach (var e in books)
                    {
                        if (e.Owner == null)
                        {
                            book.Books.Remove(e);
                            if (!test) bdao.Del(e.Id);
                        }
                        if (book.Count == book.RealCount) break;
                    }
                    Debug.Assert(book.Count == book.RealCount, "没有删掉足够的书");
                }
            }
            else if (book.Count > book.RealCount)
            {
                if (book.Books == null) book.Books = new List<BookEx.Book>();
                while (book.Count > book.RealCount)
                {
                    BookEx.Book b = new BookEx.Book();
                    if (!test)
                    {
                        Book t = bdao.Add(book);
                        b.Id = t.Id;
                    }
                    b.Owner = null;
                    book.Books.Add(b);
                }
            }
        }

        void RefillBook(BookEx book)
        {
            if (book.Filled) return;
            if (book.Books == null)
            {
                book.Books = new List<BookEx.Book>();
                IBook_BookInfoDAO bb = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                IUser_BookDAO ub = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
                if (bb != null && ub!=null)
                {
                    List<Book> books=bb.GetBook(book.Isbn);
                    foreach (var e in books)
                    {
                        BookEx.Book b = new BookEx.Book();
                        b.Id = e.Id;
                        User u = ub.GetUser(b.Id);
                        if (u == null) b.Owner = null; else b.Owner = u.Id;
                        book.Books.Add(b);
                    }
                }
            }

        }

        public void ProcessRequest(HttpContext context)
        {
            BookEx book = null;
            try
            {
                IBookInfoDAO dao = DAOFactory.createDAO("BookInfoDAO") as IBookInfoDAO;
                String sbook = context.Request["book"];
                if (sbook == null) throw new NullReferenceException("user域为空");
                book = Util.DecodeJson<BookEx>(sbook);
                RefillBook(book);
                if (book == null) throw new NullReferenceException("user解析失败");
                dao.Set(book);
                HandleBook(book);
                context.Response.Write(Util.EncodeJson(book, "成功"));
            }
            catch (NullReferenceException e)
            {
                //context.Response.Write(e.Message);
                HandleBook(book,true);
                context.Response.Write(Util.EncodeJson(book, "成功"));
            }
            catch (BMException e)
            {
                context.Response.Write(e.Message);
            }
        }

        #endregion
    }
}
