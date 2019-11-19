using Chess.Utility;
using Chess.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Chess
{
    /// <summary>
    /// api接口
    /// </summary>
    public class ShortController : ApiController
    {

        /// <summary>
        ///获取短连接
        /// </summary>
        /// <param name="recordLogStr"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetShortUrl(string clubId, string roomId)
        {
            //string encode = System.Web.HttpUtility.UrlEncode(recordLogStr, System.Text.Encoding.UTF8);
            //string url = "http://score.mxiong.com.cn/RecordLog/GetRecordLog?" + encode;
            //string url2 = "http://score.mxiong.com.cn/RecordLog/GetRecordLog?" + recordLogStr;
            //TinyurlModel Tm = new TinyurlModel();
            //BaiduProvider bp = new BaiduProvider();
            //Tm = bp.GetTinyurl(url2, EnumTinyurlType.Referrer);
            //return Ok(Tm.url_short);
            string url = ConfigHelper.WebServerDamain + "/RecordLog/GetRecordLog?clubId=" + clubId + "&roomId=" + roomId + "";
            TinyurlModel Tm = new TinyurlModel();
            BaiduProvider bp = new BaiduProvider();
            Tm = bp.GetTinyurl(HttpUtility.UrlEncode(url), EnumTinyurlType.Referrer);
            return Json(new { url = Tm.url_short });
        }
    }
}
