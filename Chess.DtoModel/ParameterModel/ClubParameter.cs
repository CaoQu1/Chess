using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 俱乐部编辑参数
    /// </summary>
    public class ClubParameter
    {
        /// <summary>
        /// 俱乐部编号
        /// </summary>
        [DisplayName("俱乐部编号")]
        [Required(ErrorMessage = "请输入{0}")]
        public int clubId { get; set; }

        /// <summary>
        /// 房费
        /// </summary>
        [DisplayName("房费")]
        [Required(ErrorMessage = "请输入{0}")]
        public decimal paymoney { get; set; }

        /// <summary>
        /// 倍率
        /// </summary>
        [DisplayName("倍率")]
        [Required(ErrorMessage = "请输入{0}")]
        public double rate { get; set; }
    }
}
