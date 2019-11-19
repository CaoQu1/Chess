using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain
{
    /// <summary>
    /// web上下文
    /// </summary>
    public interface IWebContext
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        SystemUser CurrentUser { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        Guid InstanceId { get; }
    }
}
