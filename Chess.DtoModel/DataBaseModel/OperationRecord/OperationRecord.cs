using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 操作记录实体类
    /// </summary>
    public class OperationRecord : BaseEntity
    {
        /// <summary>
        ///第三方（战绩记录ID）
        /// </summary>
        public int RecId { get; set; }

        /// <summary>
        ///第三方（用户ID）
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string ActionUrl { get; set; }

        #region relationship
        /// <summary>
        /// 用户Id
        /// </summary>
        public int SystemUserId { get; set; }

        /// <summary>
        /// 俱乐部群主信息
        /// </summary>
        public virtual SystemUser SystemUser { get; set; }
        #endregion
    }
}
