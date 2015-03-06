using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Classes
{
    public class Mailer
    {
        SmtpData _smtpData;
        IMailHelper _mailHelper;

        public Mailer(SmtpData smtpData, IMailHelper mailHelper)
        {
            _smtpData = smtpData;
            _mailHelper = mailHelper;
        }

        public bool SendEmail()
        {
            var mail = CreateMail();

            SmtpClient SmtpServer = new SmtpClient(_smtpData.Host);
            SmtpServer.Port = int.Parse(_smtpData.Port);
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpData.AgentSine, _smtpData.AgentDutyCode);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return true;
        }

        private MailMessage CreateMail()
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_mailHelper.GetFromAddress()),
                Subject = _mailHelper.GetSubject(),
                Body = _mailHelper.GetBody(),
            };

            mail.To.Add(string.Join(",", _mailHelper.GetToAddresses()));
            return mail;
        }
    }
}
