using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class MailerService : IMailerService
    {
        private List<PlayerStat> _playerStats;
        private Config _config;
        private IFootballRepository _footballRepository;

        public MailerService()
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            _config = _footballRepository.GetConfig();
        }

        public bool SendEmail()
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            GetPlayerStats();

            SmtpClient SmtpServer = new SmtpClient(_config.SmtpServer.ToString());
            var mail = new MailMessage();
            mail.From = new MailAddress(_config.SmtpAgentSine.ToString());
            mail.To.Add(_config.SmtpAgentSine.ToString());
            mail.Subject = DateTime.Now.ToString("yyyy-MM-dd") + " - Thursday Football Stats";
            mail.Body = GetEmailBody();
            SmtpServer.Port = int.Parse(_config.SmtpPort.ToString());
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_config.SmtpAgentSine.ToString(), GetDecryptedAgentDutyCode());
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return true;
        }

        public bool SendOKMessageToUser()
        {
            MessageBox.Show("E-mail sent OK!");
            return true;
        }

        private string GetEmailBody()
        {
            StringBuilder body = new StringBuilder("***MoleInTheBarn v1.2***\n");

            foreach(PlayerStat playerStat in _playerStats)
                body.Append(WritePlayerStatLine(playerStat.PlayerName, playerStat.MatchWins.ToString()));

            return body.ToString();
        }

        private static string WritePlayerStatLine(string playerName, string matchWins)
        {
            return string.Format("{0} : {1}\n", playerName, matchWins);
        }

        private string GetDecryptedAgentDutyCode()
        {
            return Decrypt(_config.SmtpAgentDutyCode.ToString());
        }

        private string Decrypt(string inputString)
        {
            return StringCipherService.Decrypt(inputString, "something");
        }

        private void GetPlayerStats()
        {
            _playerStats = _footballRepository.GetPlayerStats();
        }
    }
}
