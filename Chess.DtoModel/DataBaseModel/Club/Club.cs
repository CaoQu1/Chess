using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 俱乐部实体类
    /// </summary>
    public class Club : BaseEntity
    {
        public Club()
        {
            this.ClubMembers = new List<ClubMember>();
        }

        /// <summary>
        /// 第三方（用户Id)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///（第三方） 俱乐部编号
        /// </summary>
        public int ClubId { get; set; }

        /// <summary>
        /// （第三方）俱乐部名称
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// 短链接
        /// </summary>
        public string ShortLink { get; set; }

        /// <summary>
        /// 管理员短链接
        /// </summary>
        public string MangerShortLink { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 倍率
        /// </summary>
        public double Rate { get; set; } = 1;
        /// <summary>
        /// 收款金额说明
        /// </summary>
        public string PayRemark { get; set; }

        /// <summary>
        /// 俱乐部群主Id
        /// </summary>
        public int? SystemUserId { get; set; }

        /// <summary>
        /// 收款二维码
        /// </summary>
        public string PayImage { get; set; }

        #region relationship

        /// <summary>
        /// 平台标识
        /// </summary>
        public int? PlatformId { get; set; }

        /// <summary>
        /// 俱乐部平台信息
        /// </summary>
        public virtual Platform Platform { get; set; }

        /// <summary>
        /// 俱乐部成员信息
        /// </summary>
        public virtual ICollection<ClubMember> ClubMembers { get; set; }

        #endregion
    }
}
