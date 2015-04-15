using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Classes
{
    public class Mailer : IMailer
    {
        SmtpData _smtpData;
        IMailHelper _mailHelper;

        public Mailer(SmtpData smtpData, IMailHelper mailHelper)
        {
            _smtpData = smtpData;
            _mailHelper = mailHelper;
        }

        public IMailer CreateInstance(SmtpData smtpData, IMailHelper mailHelper)
        {
            return new Mailer(smtpData, mailHelper);
        }

        public bool SendMail()
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

        public MailMessage CreateMail()
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_mailHelper.GetFromAddress()),
                Subject = _mailHelper.GetSubject(),
                Body = _mailHelper.GetBody(),
            };

            foreach (var address in _mailHelper.GetToAddresses())
                mail.To.Add(address);

            return mail;
        }
    }
}
