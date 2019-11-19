using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.Model
{

    /// <summary>
    /// 验证结果枚举
    /// </summary>
    public enum EnumSuccess
    {
        Success = 0,
        Fail = 1,
        VeFail = 2
    }

    /// <summary>
    /// 接口返回公用结果
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 验证是否成功
        /// </summary>
        public bool valid { get; set; }

        /// <summary>
        /// 0代表成功，1代表失败
        /// </summary>
        public int result { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 茶馆名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 接口返回公共参数
    /// </summary>
    public class BaseFaceResult : BaseResult
    {

        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 授权接口
    /// </summary>
    public class Auth2Result
    {
        /// <summary>
        /// 验证是否成功
        /// </summary>
        public bool valid { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string code { get; set; }
    }

    /// <summary>
    /// 战绩查询接口
    /// </summary>
    public class PayScoreResult : BaseFaceResult
    {
        /// <summary>
        /// 分数
        /// </summary>
        public string scores { get; set; }
    }

    /// <summary>
    /// 俱乐部成员接口
    /// </summary>
    public class ClubMemberResult : BaseFaceResult
    {
        /// <summary>
        /// 成员
        /// </summary>
        public string members { get; set; }
    }
}