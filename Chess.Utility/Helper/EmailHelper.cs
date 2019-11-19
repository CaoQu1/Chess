using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<ExtResult> SendMailAsync(EmailConfig model)
        {
            ExtResult result = new ExtResult
            {
                IsSuccess = true,
                Message = "发送成功!"
            };
            try
            {
                MailMessage mail = new MailMessage(model.from, model.to);
                mail.Subject = model.subject;
                mail.Body = model.body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient
                {
                    Host = model.server,
                    Port = model.port,
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };
                if (model.ispwd)
                {
                    System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(model.username, model.password);
                    client.Credentials = networkCredential.GetCredential(client.Host, client.Port, "NTLM");
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
