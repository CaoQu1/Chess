using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 平台实体类
    /// </summary>
    public class Platform : BaseEntity
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public string PlatformId { get; set; }

        /// <summary>
        /// 平台名字
        /// </summary>
        public string PlatformName { get; set; }

        #region relationship

        /// <summary>
        /// 平台用户信息
        /// </summary>
        public virtual ICollection<SystemUser> SystemUsers { get; set; }

        /// <summary>
        /// 平台战绩记录
        /// </summary>
        public virtual ICollection<RecordLog> RecordLogs { get; set; }

        /// <summary>
        /// 平台俱乐部信息
        /// </summary>
        public virtual ICollection<Club> Clubs { get; set; }

        #endregion
    }
}
