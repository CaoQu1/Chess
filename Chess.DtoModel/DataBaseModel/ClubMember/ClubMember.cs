using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 俱乐部成员实体类
    /// </summary>
    public class ClubMember : BaseEntity
    {
        /// <summary>
        ///第三方（俱乐部编号）
        /// </summary>
        public int ClubId { get; set; }

        /// <summary>
        ///第三方（用户Id）
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 平台标识
        /// </summary>
        public string PlatformId { get; set; }

        /// <summary>
        /// 成员状态 0:历史群员 1：当前正常成员
        /// </summary>
        public int ClubMembersState { get; set; }

        /// <summary>
        /// 是否为俱乐部管理员 0：群员 1：管理员
        /// </summary>
        public bool IsClubManger { get; set; }

        /// <summary>
        /// 是否是拥有者
        /// </summary>
        public bool IsOwner { get; set; }

        #region relationship

        /// <summary>
        /// 俱乐部编号
        /// </summary>
        public int? SystemClubId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int? SystemUserId { get; set; }

        /// <summary>
        /// 成员用户信息
        /// </summary>
        public virtual SystemUser SystemUser { get; set; }

        /// <summary>
        /// 成员俱乐部信息
        /// </summary>
        public virtual Club Club { get; set; }
        #endregion
    }
}
