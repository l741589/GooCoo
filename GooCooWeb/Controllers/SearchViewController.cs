using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.IDAO;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooWeb.Models;
using System.Collections;

namespace GooCooWeb.Controllers
{
    public class SearchViewController : Controller
    {
        //
        // GET: /SearchView/      
        public ActionResult Index(string keyword, string type = "标题", int page = 0)
        {
            SearchResultModel resultModel = new SearchResultModel();
            //防止恶意请求
            if (!type.Equals("标题") && !type.Equals("ISBN") && !type.Equals("模糊"))
            {
                type = "标题";
            }

            resultModel.SearchType = type;
            if (keyword == null || keyword.Length == 0)
            {
                resultModel.Keyword = "";
                resultModel.HasSearch = false;                                
                ViewBag.SearchResult = resultModel;
                return View();
            }

            resultModel.Keyword = keyword;


            /*
             * 暂时注释
             * 
            IBookInfoDAO bookInfoDao = DAOFactory.createDAO("IBookInfoDao") as IBookInfoDAO;
            try
            {                
                //计算结果总数
                if (type.Equals("标题"))
                {
                    resultModel.ResultCount = bookInfoDao.GetCountByName(keyword);
                }
                else if (type.Equals("ISBN"))
                {
                    resultModel.ResultCount = bookInfoDao.GetCountByIsbn(keyword);
                }
                else if (type.Equals("模糊"))
                {
                    resultModel.ResultCount = bookInfoDao.GetCountByKeyWord(keyword);
                }
                resultModel.TotalPage = resultModel.ResultCount / SearchResultModel.recordPerPage + 1;
                resultModel.HasSearch = true;
            }
            catch (Exception e) {                
            }
           

            //获取显示结果
            if (resultModel.ResultCount != 0)
            {
                if (type.Equals("标题"))
                {
                    resultModel.Results = bookInfoDao.GetByName(keyword, 1 + (page - 1) * SearchResultModel.recordPerPage, SearchResultModel.recordPerPage);
                }
                else if (type.Equals("ISBN"))
                {
                    resultModel.Results = bookInfoDao.GetByIsbn(keyword, 1 + (page - 1) * SearchResultModel.recordPerPage, SearchResultModel.recordPerPage);
                }
                else if (type.Equals("模糊"))
                {
                    resultModel.Results = bookInfoDao.GetByKeyWord(keyword, 1 + (page - 1) * SearchResultModel.recordPerPage, SearchResultModel.recordPerPage);
                }                
            }
            */

            ///////////////测试用结果
            resultModel.ResultCount = 190;
            resultModel.TotalPage = (resultModel.ResultCount + (SearchResultModel.recordPerPage - 1)) / SearchResultModel.recordPerPage;
            resultModel.HasSearch = true;

            List<BookInfo> tempResultArray = new List<BookInfo>();
            for (int i = 0; i < SearchResultModel.recordPerPage; i++)
            {
                BookInfo bookInfo = new BookInfo();
                bookInfo.Isbn = Convert.ToString(i);
                bookInfo.Summary = "《武林外史》作于1965年，原名《风雪会中州》，是古龙中期转型作品，从中可看到古龙对武侠不断求新探索，其中不少人物是古龙后来作品的原型。 全书围绕一段武林恩怨展开，主角是四个性格各异的少年——沈浪、朱七七、王怜花、熊猫儿，更有许多江湖奇人异士，纠缠其中，场景跨越中原、太行、大漠、楼兰、可称宏图巨著，情节跌宕。";
                bookInfo.Name = "武林外史";
                //bookInfo.Author = "古龙"
                bookInfo.Photourl = "http://img3.douban.com/mpic/s2157315.jpg";
                tempResultArray.Add(bookInfo);
            }
            resultModel.Results = tempResultArray;



            if (resultModel.ResultCount == 0)
            {
                ViewBag.SearchResult = resultModel;
                return View("NoResult");
            }
            else
            {
                //设置page
                resultModel.CurrentPage = page;
                resultModel.PageFrom = resultModel.CurrentPage - 2 > 0 ? resultModel.CurrentPage - 2 : 1;
                resultModel.PageTo = resultModel.CurrentPage + 4 <= resultModel.TotalPage ? resultModel.CurrentPage + 4 : resultModel.TotalPage;
                resultModel.PreviewPage = resultModel.CurrentPage - 1 > 0 ? resultModel.CurrentPage - 1 : 0;
                resultModel.NextPage = resultModel.CurrentPage + 1 <= resultModel.TotalPage ? resultModel.CurrentPage + 1 : 0;

                ViewBag.SearchResult = resultModel;
                return View();
            }
        }

    }
}
