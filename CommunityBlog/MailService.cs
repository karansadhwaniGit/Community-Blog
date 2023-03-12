using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using CommunityBlog.Models;

namespace CommunityBlog
{
    public class MailService
    {
        SmtpClient smtpClient;
        public MailService()
        {
            smtpClient = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("30a9f45cb9f1c8", "0ab110c85a2004"),
                EnableSsl = true,
            
            };
        }
        public void sendMail(MailMessage mailModel)
        {
            smtpClient.Send(mailModel);
            
        }
    }
}
