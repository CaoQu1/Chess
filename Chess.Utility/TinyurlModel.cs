using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{

    /// <summary>
    /// 短网址
    /// </summary>
    [Serializable, DataContract]
    public class TinyurlModel
    {
        /// <summary>
        /// 短网址
        /// </summary>
        [DataMember]
        public string url_short { get; set; }

        /// <summary>
        /// 源网址
        /// </summary>
        [DataMember]
        public string url_long { get; set; }

        /// <summary>
        /// 短网址类型
        /// </summary>
        [DataMember]
        public EnumTinyurlType link_type { get; set; }

    }

    /// <summary>
    /// 枚举：链接类型（0：推广，1：邀请）
    /// </summary>
    [Serializable]
    public enum EnumTinyurlType
    {
        /// <summary>
        /// 推广
        /// </summary>
        [Description("推广")]
        Referrer = 0,
        /// <summary>
        /// 邀请
        /// </summary>
        [Description("邀请")]
        Invite = 1
    }


    /// <summary>
    /// 短网址实体类
    /// </summary>
    [Serializable, DataContract]
    public class SinaTinyurlModel : TinyurlModel
    {
        ///<summary>
        ///类型
        ///</summary>
        [DataMember]
        public virtual int type { get; set; }
    }
}


/// <summary>
/// 百度短网址服务
/// </summary>
[Serializable]
public class BaiduTinyurlModel
{
    /// <summary>
    /// 短网址
    /// </summary>
    public string ShortUrl { get; set; }
    /// <summary>
    /// 源网址
    /// </summary>
    public string LongUrl { get; set; }

    /// <summary>
    /// 生成状态
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 错误提示
    /// </summary>
    public string ErrMsg { get; set; }
}

/// <summary>
/// 51短网址服务
/// </summary>
public class H51TinyurlModel
{
    /// <summary>
    /// 编码
    /// </summary>
    public int code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string msg { get; set; }

    /// <summary>
    /// 短网址
    /// </summary>
    public string short_url { get; set; }
}

/// <summary>
/// suo.im短网址服务
/// </summary>
public class SuoTinyurlModel
{
    /// <summary>
    /// 错误消息
    /// </summary>
    public string err { get; set; }

    /// <summary>
    /// 短网址
    /// </summary>
    public string url { get; set; }
}
