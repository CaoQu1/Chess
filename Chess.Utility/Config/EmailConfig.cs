using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chess.Utility
{
    /// <summary>
    /// 邮件配置
    /// </summary>
    [XmlRoot("email")]
    public class EmailConfig : BaseConfig<EmailConfig>
    {
        /// <summary>
        /// 发件人
        /// </summary>
        [XmlElement("from")]
        public string from { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [XmlElement("to")]
        public string to { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [XmlElement("subject")]
        public string subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [XmlElement("body")]
        public string body { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [XmlElement("username")]
        public string username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [XmlElement("password")]
        public string password { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        [XmlElement("port")]
        public int port { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        [XmlElement("server")]
        public string server { get; set; }

        /// <summary>
        /// 是否启用身份验证
        /// </summary>
        [XmlElement("ispwd")]
        public bool ispwd { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public EmailConfig() : base(ConfigConst.EMAIL)
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public override EmailConfig initConfig()
        {
            var emailModel = new EmailConfig
            {
                from = "1165627191@qq.com",
                to = "1076966403@qq.com",
                subject = "测试邮件标题",
                body = @"<html>
                        <head></head>
                        <body>
                        <p>{0}</p>
                        </body>
                        </html>",
                username = "1165627191@qq.com",
                password = "CaoQu557282828",
                port = 587,
                server = "smtp.qq.com",
                ispwd = true
            };
            base.saveConfig(emailModel);
            return emailModel;
        }
    }
}
