using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
using FootballManagerEF.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Tests.Services
{
    [TestFixture]
    public class MailerServiceTests
    {
        FootballRepository fakeFootballRepo;
        MailerService mailerService;
        IPlayerMatchViewModel playerMatchViewModel;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            mailerService = new MailerService(playerMatchViewModel, fakeFootballRepo);
        }

        [Test]
        public void MailerService_WhenSendStatsIsCalledItReturnsTrue()
        {
            //Arrange 
            bool expectedValue = true;

            //Act
            var result = mailerService.SendStats();

            //Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void MailerService_WhenSendTeamsIsCalledItReturnsTrue()
        {
            //Arrange 
            bool expectedValue = true;

            //Act
            var result = mailerService.SendTeams();

            //Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }
    }
}
