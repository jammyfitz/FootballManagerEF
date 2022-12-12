using FootballManagerEF.Interfaces;
using System.Net;
using System.Net.Mail;

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

            var SmtpServer = new SmtpClient(_smtpData.Host)
            {
                Port = int.Parse(_smtpData.Port),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpData.AgentSine, _smtpData.AgentDutyCode),
                EnableSsl = true
            };

            SmtpServer.Send(mail);

            return true;
        }

        public MailMessage CreateMail()
        {
            var mail = new MailMessage()
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
