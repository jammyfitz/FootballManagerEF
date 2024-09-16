using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMailer
    {
        IMailer CreateInstance(SmtpData smtpData, IMailHelper mailHelper);
        MailMessage CreateMail();
        bool SendMail();
        Task<bool> SendMailUsingOAuthAsync();
        void SendEmailViaGraphApi();
    }
}
