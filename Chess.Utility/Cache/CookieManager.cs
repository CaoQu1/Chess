using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chess.Utility
{
    /// <summary>
    /// cookie管理类
    /// </summary>
    public class CookieManager : ICookieCacheManager
    {
        /// <summary>
        /// 请求上下文
        /// </summary>
        private HttpContext _httpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            Add(key, value, 0);
        }

        /// <summary>
        /// 添加cooike
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dateTimeOffset"></param>
        public void Add(string key, string value, int dateTimeOffset)
        {
            HttpCookie cookie = _httpContext.Request.Cookies[key];
            if (cookie == null)
            {
                cookie = new HttpCookie(key, value);
            }
            else
            {
                cookie.Value = value;
            }
            if (dateTimeOffset != 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(dateTimeOffset);
            }
            _httpContext.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return IsExists(key) ? _httpContext.Request.Cookies[key].Value : null;
        }

        /// <summary>
        /// 是否存在cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExists(string key)
        {
            return _httpContext.Request.Cookies.AllKeys.Contains(key);
        }

        /// <summary>
        /// 移除cookie
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            HttpCookie httpCookie = new HttpCookie(key);
            if (IsExists(key))
            {
                httpCookie = _httpContext.Request.Cookies[key];
                if (httpCookie != null)
                {
                    httpCookie.Expires = DateTime.Now.AddYears(-1);
                }
            }
            else
            {
                httpCookie.Expires = DateTime.Now.AddYears(-1);
                httpCookie.HttpOnly = false;
            }
            _httpContext.Response.AppendCookie(httpCookie);
            _httpContext.Response.Cookies.Remove(key);
        }
    }
}
