using Chess.Utility;
using Chess.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chess.Api.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return Content("Api服务已启动");
        }


        public string GetShortUrl(string recordLogStr)
        {

            string encode= System.Web.HttpUtility.UrlEncode(recordLogStr, System.Text.Encoding.UTF8);
            string url = "http://score.mxiong.com.cn/RecordLog/GetRecordLog?"+ encode;
            string url2= "http://score.mxiong.com.cn/RecordLog/GetRecordLog?" + recordLogStr;
            TinyurlModel Tm = new TinyurlModel();
            BaiduProvider bp = new BaiduProvider();
            Tm = bp.GetTinyurl(url2, EnumTinyurlType.Referrer);
            return Tm.url_short;
        }
    }
}
