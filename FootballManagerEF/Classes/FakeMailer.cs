using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Services
{
    public class FakeMailer : IMailer
    {
        private bool _result;

        public IMailer CreateInstance(SmtpData smtpData, IMailHelper mailHelper)
        {
            return new FakeMailer(true);
        }

        public FakeMailer(bool result)
        {
            _result = result;
        }

        public MailMessage CreateMail()
        {
            return new MailMessage();
        }

        public bool SendMail()
        {
            return _result;
        }
    }
}
