using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class SystemUser : BaseEntity
    {
        public SystemUser()
        {
            this.OperationRecords = new List<OperationRecord>();
            this.RecordLogs = new List<RecordLog>();
        }

        /// <summary>
        /// 第三方（用户编号）
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 第三方（用户名称）
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 第三方（对外用户编号）
        /// </summary>
        public int? GameId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public EnumSex Sex { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 头像二进制数据
        /// </summary>
        public byte[] HeadImg { get; set; }

        /// <summary>
        /// 付款二维码二进制数据
        /// </summary>
        public byte[] PayCodeImg { get; set; }

        /// <summary>
        /// 付款码图片
        /// </summary>
        public string PayCodeUrl { get; set; }

        /// <summary>
        /// 用户身份（0、群主，1、成员）
        /// </summary>
        public EnumRole UserIdentity { get; set; }

        /// <summary>
        ///（微信） openid
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// （微信）unionid
        /// </summary>
        public string Unionid { get; set; }

        /// <summary>
        /// 代理账号
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 代理密码
        /// </summary>
        public string AgentPassWord { get; set; }

        /// <summary>
        /// 验证字符串
        /// </summary>
        public string CheckKey { get; set; }

        #region relationship

        /// <summary>
        /// 平台编号
        /// </summary>
        public int? PlatformId { get; set; }

        /// <summary>
        /// 群主平台信息
        /// </summary>
        public virtual Platform Platform { get; set; }

        /// <summary>
        /// 用户操作记录信息
        /// </summary>
        public virtual ICollection<OperationRecord> OperationRecords { get; set; }

        /// <summary>
        /// 用户战绩记录信息
        /// </summary>
        public virtual ICollection<RecordLog> RecordLogs { get; set; }
        #endregion
    }

    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum EnumSex
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKown = 0,

        /// <summary>
        /// 男人
        /// </summary>
        Man = 1,

        /// <summary>
        /// 女人
        /// </summary>
        WoMan = 2
    }

    /// <summary>
    /// 角色枚举
    /// </summary>
    [Flags]
    public enum EnumRole
    {
        /// <summary>
        /// 群主
        /// </summary>
        GroupOwner = 1,

        /// <summary>
        /// 成员
        /// </summary>
        Member = 2
    }
}
