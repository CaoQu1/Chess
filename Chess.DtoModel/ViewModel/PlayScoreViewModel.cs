using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel.ViewModel
{
    public class PlayScoreViewModel
    {

        /// <summary>
        ///第三方（战绩记录ID）
        /// </summary>
        public int RecId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 游戏id
        /// </summary>
        public int GameId { get; set; }
        /// <summary>
        /// 房间id
        /// </summary>
        public int RoomID { get; set; }
        /// <summary>
        /// 俱乐部id
        /// </summary>
        public int ClubID { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>
        public string WriteTime { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 是否大赢家
        /// </summary>
        public bool Winner { get; set; }


        /// <summary>
        /// 付款码
        /// </summary>
        public  byte[] PayCodeImg { get; set; }

        /// <summary>
        /// 付款码图片
        /// </summary>
        public string PayCodeUrl { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public EnumPayStatus PayState { get; set; }

        /// <summary>
        /// 平台标识
        /// </summary>
        public int? PlatformId { get; set; }


        /// <summary>
        /// 用户Id
        /// </summary>
        public int? SystemUserId { get; set; }
    }
}
