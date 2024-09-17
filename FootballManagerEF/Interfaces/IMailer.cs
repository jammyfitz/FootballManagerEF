using System.Net.Mail;

namespace FootballManagerEF.Interfaces
{
    public interface IMailer
    {
        IMailer CreateInstance(SmtpData smtpData, IMailHelper mailHelper);
        MailMessage CreateMail();
        bool SendMail();
        void SendEmailViaGraphApi();
    }
}
