using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 战绩记录实体类
    /// </summary>
    public class RecordLog : BaseEntity
    {
        /// <summary>
        ///第三方（战绩记录ID）
        /// </summary>
        public int RecId { get; set; }
        /// <summary>
        ///第三方（俱乐部编号）
        /// </summary>
        public int ClubId { get; set; }
        /// <summary>
        /// 第三方（用户Id）
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        ///第三方（房间号）
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        ///第三方（回放码）
        /// </summary>
        public string ReplayId { get; set; }
        /// <summary>
        ///第三方（分数）
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 成员openId
        /// </summary>
        public string MemberOpenid { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public EnumPayStatus PayState { get; set; }

        /// <summary>
        /// 平台标识
        /// </summary>
        public int? PlatformId { get; set; }

        #region relationship

        /// <summary>
        /// 用户Id
        /// </summary>
        public int? SystemUserId { get; set; }

        /// <summary>
        /// 战绩用户信息
        /// </summary>
        public virtual SystemUser SystemUser { get; set; }
        #endregion
    }

    /// <summary>
    /// 付款状态枚举
    /// </summary>
    public enum EnumPayStatus
    {
        /// <summary>
        /// 已付
        /// </summary>
        Paid = 1,

        /// <summary>
        /// 未付
        /// </summary>
        UnPaid = 2
    }
}
