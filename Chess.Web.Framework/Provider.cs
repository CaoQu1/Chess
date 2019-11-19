using Chess.Service;
using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chess.Web.Framework
{
    /// <summary>
    /// 网络服务
    /// </summary>
    public static class Provider
    {
        #region Field

        /// <summary>
        /// 新浪服务
        /// <remarks></remarks>
        /// </summary>
        private static SinaProvider _sinaProvider;

        /// <summary>
        /// 百度服务
        /// </summary>
        private static BaiduProvider _baiduProvider;

        /// <summary>
        /// 加密类
        /// </summary>
        private static IEncryptionService _encryptionServ;

        #endregion

        #region Ctor

        static Provider()
        {
            _sinaProvider = new SinaProvider();
            _baiduProvider = new BaiduProvider();
            _encryptionServ = new EncryptionService();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 短网址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static TinyurlModel Tinyurl(int uid, int? cid = null)
        {
            return _baiduProvider.GetTinyurl(uid, cid);
        }

        /// <summary>
        /// 验证推广链接参数是否正确
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool ValidateReferrerUrl(string hex, out int uid)
        {
            uid = -1;
            if (String.IsNullOrEmpty(hex)) return false;

            var dstUid = _encryptionServ.DecryptText(hex.HexToString(), ConfigHelper.GetAppSetting("EncryptedStr"));
            return (!String.IsNullOrEmpty(dstUid) && int.TryParse(dstUid, out uid));
        }

        /// <summary>
        /// 验证分享编号
        /// </summary>
        /// <param name="hex">加密后的值</param>
        /// <param name="uid">用户编号</param>
        /// <param name="cid">圈子编号</param>
        /// <returns></returns>
        public static bool ValidateInviteUrl(string hex, out int uid, out int cid)
        {
            uid = -1;
            cid = -1;
            if (String.IsNullOrEmpty(hex)) return false;

            var dst = (_encryptionServ.DecryptText(hex.HexToString()) ?? "").Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            return (dst.Length == 2 && int.TryParse(dst[0], out uid) && int.TryParse(dst[1], out cid));
        }

        #endregion
    }

    /// <summary>
    /// 新浪服务
    /// </summary>
    public class SinaProvider
    {
        /// <summary>
        /// 短网址缓存
        /// </summary>
        private Dictionary<string, List<TinyurlModel>> _cache_short_urls;

        /// <summary>
        /// 加密类
        /// </summary>
        private IEncryptionService _encryptionServ;

        public SinaProvider()
        {
            _cache_short_urls = new Dictionary<string, List<TinyurlModel>>();
            _encryptionServ = new EncryptionService();
        }

        /// <summary>
        /// 获取短网址
        /// </summary>
        /// <param name="source_url">目标网址(完整网址路径)</param>
        /// <returns></returns>
        public List<TinyurlModel> GetTinyurl(string source_url, EnumTinyurlType type)
        {
            string _lower_source_url = source_url.ToLower().Trim();
            if (_cache_short_urls.ContainsKey(_lower_source_url) && _cache_short_urls[_lower_source_url] != null)
            {
                return _cache_short_urls[_lower_source_url];
            }

            #region 短网址获取

            var model = new TinyurlModel()
            {
                url_short = source_url,
                url_long = source_url,
                link_type = type
            };

            try
            {
                string api_url = "http://api.t.sina.com.cn/short_url/shorten.json";
                string api_parms = string.Format("source={0}&url_long={1}", 1681459862, HttpUtility.UrlEncode(_lower_source_url));

                var sina_tinyurls = NetWorkHelper.HttpGetDown<List<SinaTinyurlModel>>(string.Join("?", api_url, api_parms));
                if (sina_tinyurls != null)
                {
                    _cache_short_urls.Add(_lower_source_url, sina_tinyurls.Select(q => new TinyurlModel()
                    {
                        url_short = q.url_short,
                        url_long = q.url_long,
                        link_type = type
                    }).ToList());
                    return _cache_short_urls[_lower_source_url];
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }

            return new List<TinyurlModel>() { model };

            #endregion
        }

        /// <summary>
        /// 获取返回数据中第一条短网址
        /// </summary>
        /// <param name="source_url">目标网址(完整网址路径)</param>
        /// <returns></returns>
        public TinyurlModel GetSingleTinyurl(string source_url, EnumTinyurlType type)
        {
            List<TinyurlModel> lst = GetTinyurl(source_url, type);
            if (lst != null && lst.Count > 0) return lst[0];
            return new TinyurlModel()
            {
                url_short = source_url,
                url_long = source_url,
                link_type = type
            };
        }

        /// <summary>
        /// 获取推荐或邀请链接
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetTinyurl(int uid, int? cid = null)
        {
            if (cid.HasValue) return GetInviteTinyurl(uid, cid.Value);
            else return GetReferrerTinyurl(uid);
        }

        /// <summary>
        /// 获取推荐短网址
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetReferrerTinyurl(int uid)
        {
            var baseUri = string.Concat(ConfigHelper.WebServerDamain, "/User/Register");
            //des加密后处理成hex字符串
            var hash = _encryptionServ.EncryptText(uid.ToString(), "Chess").ToHex();
            return GetSingleTinyurl(string.Concat(baseUri, "?u=", hash), EnumTinyurlType.Referrer);
        }

        /// <summary>
        /// 获取邀请短网址
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetInviteTinyurl(int uid, int cid)
        {
            var baseUri = string.Concat(ConfigHelper.WebServerDamain, "/Join");
            var hash = _encryptionServ.EncryptText(String.Format("{0}+{1}", uid, cid)).ToHex();
            return GetSingleTinyurl(string.Concat(baseUri, "?u=", hash), EnumTinyurlType.Invite);
        }
    }

    /// <summary>
    /// 百度服务
    /// </summary>
    public class BaiduProvider
    {
        /// <summary>
        /// 短网址缓存
        /// </summary>
        private Dictionary<string, TinyurlModel> _cache_short_urls;

        /// <summary>
        /// 加密类
        /// </summary>
        private IEncryptionService _encryptionServ;

        public BaiduProvider()
        {
            _cache_short_urls = new Dictionary<string, TinyurlModel>();
            _encryptionServ = new EncryptionService();
        }

        /// <summary>
        /// 获取短网址信息
        /// </summary>
        /// <param name="source_url"></param>
        /// <returns></returns>
        public TinyurlModel GetTinyurl(string source_url, EnumTinyurlType type)
        {
            string _lower_source_url = source_url.ToLower().Trim();
            if (_cache_short_urls.ContainsKey(_lower_source_url) && _cache_short_urls[_lower_source_url] != null)
            {
                return _cache_short_urls[_lower_source_url];
            }

            #region 短网址获取

            try
            {
                var pairs = new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("url", source_url),
                    new KeyValuePair<string, string>("format", "json")
                    //new KeyValuePair<string, string>("access_type", "web")
                };

                //var baidu_tinyurl = NetWorkHelper.HttpGetDown<H51TinyurlModel>("http://h5ip.cn/index/api", pairs);
                var baidu_tinyurl = NetWorkHelper.HttpGetDown<SuoTinyurlModel>("http://suo.im/api.php", pairs);

                //if (baidu_tinyurl != null && baidu_tinyurl.code == 0)
                if (baidu_tinyurl != null && baidu_tinyurl.err == "")
                {
                    _cache_short_urls.Add(_lower_source_url, new TinyurlModel()
                    {
                        url_long = source_url,
                        //url_short = baidu_tinyurl.short_url,
                        url_short = baidu_tinyurl.url,
                        link_type = type
                    });
                    return _cache_short_urls[_lower_source_url];
                }
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }

            return new TinyurlModel()
            {
                url_short = source_url,
                url_long = source_url,
                link_type = type
            };

            #endregion
        }

        /// <summary>
        /// 获取推荐或邀请链接
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetTinyurl(int uid, int? cid = null)
        {
            if (cid.HasValue) return GetInviteTinyurl(uid, cid.Value);
            else return GetReferrerTinyurl(uid);
        }

        /// <summary>
        /// 获取推荐短网址
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetReferrerTinyurl(int uid)
        {
            var baseUri = string.Concat(ConfigHelper.WebServerDamain, ConfigHelper.ShortUrl);

            //des加密后处理成hex字符串
            var hash = _encryptionServ.EncryptText(uid.ToString(), ConfigHelper.GetAppSetting("EncryptedStr")).ToHex();
            return GetTinyurl(string.Concat(baseUri, "?u=", hash), EnumTinyurlType.Referrer);
        }

        /// <summary>
        /// 获取邀请短网址
        /// </summary>
        /// <returns></returns>
        public TinyurlModel GetInviteTinyurl(int uid, int cid)
        {
            var baseUri = string.Concat(ConfigHelper.WebServerDamain, ConfigHelper.ShortUrl);
            var hash = _encryptionServ.EncryptText(String.Format("{0}+{1}", uid, cid)).ToHex();
            return GetTinyurl(string.Concat(baseUri, "?u=", hash), EnumTinyurlType.Invite);
        }
    }
}
