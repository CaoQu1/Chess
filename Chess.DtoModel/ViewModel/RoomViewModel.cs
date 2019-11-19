using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel.ViewModel
{
     public class RoomViewModel
    {
        /// <summary>
        /// 房间id
        /// </summary>
       public int RoomId { get; set; }

        /// <summary>
        /// 倍率
        /// </summary>
       public double Rate { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 群收款码
        /// </summary>
        public byte PayCode { get; set; }

        /// <summary>
        /// 付款码图片
        /// </summary>
        public string PayCodeUrl { get; set; }
        /// <summary>
        /// 平台标识
        /// </summary>
        public int PlatformId { get; set; }
    }
}
