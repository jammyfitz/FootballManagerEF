using FootballManagerEF.Classes;
using FootballManagerEF.Helpers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class MailerService : IMailerService
    {
        private List<PlayerStat> _playerStats;
        private Config _config;
        private IFootballRepository _footballRepository;
        private IMailer _mailer;
        private IMailHelper _mailHelper;
        private SmtpData _smtpData;
        private IPlayerMatchViewModel _playerMatchViewModel;
        private ObservableCollection<Team> _teams;

        public MailerService(IPlayerMatchViewModel playerMatchViewModel, ObservableCollection<PlayerMatch> playerMatches, ObservableCollection<Team> teams)
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            _mailer = new Mailer(_smtpData, _mailHelper);
            InitialiseData(playerMatchViewModel);
        }

        public MailerService(IPlayerMatchViewModel playerMatchViewModel, IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _mailer = new FakeMailer(true);
            InitialiseData(playerMatchViewModel);
        }

        public bool SendStats()
        {
            _footballRepository.Refresh();
            var playerStatisticsData = _footballRepository.GetPlayerStatisticsData();

            _mailHelper = new PlayerStatsMailHelper(playerStatisticsData, _config.SmtpAgentSine);
            SetupMail(_mailHelper);

            ////_mailer.SendMail();
            Task.Run(() => _mailer.SendEmailViaGraphApi());

            return true;
        }

        public bool SendTeams()
        {
            _mailHelper = new TeamsMailHelper(_playerMatchViewModel.PlayerMatches, _footballRepository, _config.SmtpAgentSine);
            SetupMail(_mailHelper);
            ////_mailer.SendMail();
            Task.Run(() => _mailer.SendEmailViaGraphApi());

            return true;
        }

        public bool SendCancellation()
        {
            _mailHelper = new CancellationMailHelper(_playerMatchViewModel.PlayerMatches, _footballRepository, _config.SmtpAgentSine);
            SetupMail(_mailHelper);
            ////_mailer.SendMail();
            Task.Run(() => _mailer.SendEmailViaGraphApi());

            return true;
        }

        private void SetupMail(IMailHelper mailHelper)
        {
            _mailer = _mailer.CreateInstance(_smtpData, mailHelper);
        }

        public bool SendOKToUser()
        {
            MessageBox.Show("E-mail sent OK!");
            return true;
        }

        #region Private
        private void InitialiseData(IPlayerMatchViewModel playerMatchViewModel)
        {
            _playerMatchViewModel = playerMatchViewModel;
            _teams = _footballRepository.GetTeams();
            _config = _footballRepository.GetConfig();
            _smtpData = InitialiseSmtpData();
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
            RegisterWinRatios(_playerStats);
        }

        private void RegisterWinRatios(List<PlayerStat> _playerStats)
        {
            foreach (PlayerStat playerStat in _playerStats)
            {
                playerStat.WinRatio = ((decimal)playerStat.MatchWins / (decimal)playerStat.MatchesPlayed) * 100;
            }
        }

        private SmtpData InitialiseSmtpData()
        {
            return new SmtpData
            {
                AgentSine = _config.SmtpAgentSine,
                AgentDutyCode = GetDecryptedAgentDutyCode(),
                SmtpAgentDutyParam1 = _config.SmtpAgentDutyParam1,
                SmtpAgentDutyParam2 = _config.SmtpAgentDutyParam2,
                SmtpAgentDutyParam3 = _config.SmtpAgentDutyParam3,
                Host = _config.SmtpServer,
                Port = _config.SmtpPort
            };
        }
        #endregion
    }
}
