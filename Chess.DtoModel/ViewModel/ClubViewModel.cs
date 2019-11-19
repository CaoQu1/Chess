using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 俱乐部视图模型
    /// </summary>
    public class ClubViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 房费
        /// </summary>
        public string RoomMoney { get; set; }

        /// <summary>
        /// 倍率
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// 收款码
        /// </summary>
        public string PayImage { get; set; }

        /// <summary>
        /// 短连接
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// 俱乐部成员
        /// </summary>
        public List<ClubMemberViewModel> ClubMemberViews { get; set; }
    }

    /// <summary>
    /// 俱乐部成员视图
    /// </summary>
    public class ClubMemberViewModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 付款码
        /// </summary>
        public string PayImage { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 游戏ID
        /// </summary>
        public int GameId { get; set; }
    }
}
