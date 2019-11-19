using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chess.Utility
{
    /// <summary>
    /// 请求帮助类
    /// </summary>
    public class NetWorkHelper
    {
        #region HttpWebRequest

        /// <summary>
        /// get同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpGet(string url, object data)
        {
            return RequestMessage(new HttpSetting
            {
                Url = url,
                Data = data,
                Type = HttpType.GET
            });
        }

        /// <summary>
        /// post同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpPost(string url, object data)
        {
            return RequestMessage(new HttpSetting
            {
                Url = url,
                Data = data,
                Type = HttpType.POST
            });
        }

        /// <summary>
        /// get同步请求（返回对象）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="fromData"></param>
        /// <returns></returns>
        public static T HttpGet<T>(string url, object data)
        {
            var result = HttpGet(url, data);
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        /// <summary>
        /// Post异步请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T HttpPost<T>(string url, object data)
        {
            var result = HttpPost(url, data);
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        /// <summary>
        /// 请求响应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RequestMessage(HttpSetting model)
        {
            if (model.Type == HttpType.GET)
            {
                var props = model.Data.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(model.Data)));
                if (props != null && props.Count() > 0)
                {
                    var param = string.Join("&", props);
                    if (param.Length > 0)
                    {
                        param = param.Substring(0, param.Length - 1);
                        model.Url = string.Format("{0}?{1}", model.Url, param);
                    }
                }
            }
            var request = HttpWebRequest.Create(model.Url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.CookieContainer = model.Cookies;
            request.Method = model.Type.ToString();
            if (model.Type == HttpType.POST)
            {
                var data = JsonConvert.SerializeObject(model.Data);
                byte[] bytes = Encoding.Default.GetBytes(data);
                using (var writerStream = request.GetRequestStream())
                {
                    writerStream.Write(bytes, 0, bytes.Length);
                }
            }
            using (var readStream = request.GetResponse().GetResponseStream())
            {
                using (var read = new StreamReader(readStream))
                {
                    return read.ReadToEnd();
                }
            }
        }

        #endregion

        #region  WebClient
        /// <summary>
        /// get异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetDownAsync(string url, params KeyValuePair<string, string>[] pairs)
        {
            using (var req = new WebClient())
            {
                var fromBodyPairs = pairs.Select(q => string.Format("{0}={1}", q.Key, q.Value));
                return await req.DownloadStringTaskAsync(string.Concat(url, "?", string.Join("&", fromBodyPairs)));
            }
        }

        /// <summary>
        /// get异步请求（返回对象）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<T> HttpGetDownAsync<T>(string url, params KeyValuePair<string, string>[] pairs)
        {
            var result = await HttpGetDownAsync(url, pairs);
            return result != null ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        /// <summary>
        /// get同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string HttpGetDown(string url, params KeyValuePair<string, string>[] pairs)
        {
            using (var req = new WebClient())
            {
                req.Encoding = Encoding.UTF8;
                var fromBodyPairs = pairs.Select(q => string.Format("{0}={1}", q.Key, q.Value));
                url = string.Concat(url, "?", string.Join("&", fromBodyPairs));
                LogHelper.Info($"NetWorkHelper>>>>>>>,url=>{url}");
                var result = req.DownloadString(url);
                LogHelper.Info($"NetWorkHelper>>>>>>>,result=>{result}");
                return result;
            }
        }

        /// <summary>
        /// get同步请求(返回对象)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static T HttpGetDown<T>(string url, params KeyValuePair<string, string>[] pairs)
        {
            var result = HttpGetDown(url, pairs);
            return result != null ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        #endregion

        #region HttpClient

        /// <summary>
        /// get异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, params KeyValuePair<string, string>[] pairs)
        {
            using (var httpClient = new HttpClient())
            {
                var formBodyPairs = pairs.Select(x => string.Format("{0}={1}", x.Key, x.Value));
                url = string.Concat(url, "?", string.Join("&", formBodyPairs));
                return await httpClient.GetStringAsync(url);
            }
        }

        /// <summary>
        /// get异步请求（返回对象）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<T> HttpGetAsync<T>(string url, params KeyValuePair<string, string>[] pairs)
        {
            var result = await HttpGetAsync(url, pairs);
            return result != null ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        #endregion

        /// <summary>
        /// 请求对象
        /// </summary>
        public class HttpSetting
        {
            public HttpType Type { get; set; }

            public string Url { get; set; }

            public object Data { get; set; }

            public CookieContainer Cookies { get; set; }
        }

        /// <summary>
        /// 请求类型
        /// </summary>
        public enum HttpType
        {
            POST,
            GET
        }
    }
}
