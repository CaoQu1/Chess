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
    /// 群主注册模型
    /// </summary>
    public class RegisterParameter
    {
        /// <summary>
        /// 代理账号
        /// </summary>
        [DisplayName("代理账号")]
        [Required(ErrorMessage = "请填写:{0}")]
        public string AgentId { get; set; }            

        /// <summary>
        /// 代理密码
        /// </summary>
        [DisplayName("代理密码")]
        [Required(ErrorMessage = "请填写:{0}")]
        public string AgentPassWord { get; set; }

        ///// <summary>
        ///// 俱乐部编号
        ///// </summary>
        //[DisplayName("俱乐部编号")]
        //[Required(ErrorMessage = "请填写:{0}")]
        //public int ClubId { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        [DisplayName("平台编号")]
        [Required(ErrorMessage = "请选择:{0}")]
        public int PlatformId { get; set; }

        ///// <summary>
        ///// 检验字符串
        ///// </summary>
        //[DisplayName("检验字符串")]
        //[Required(ErrorMessage = "请填写:{0}")]
        //public string CheckKey { get; set; }
    }
}
