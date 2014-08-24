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
    public class MatchValidatorServiceTests
    {
        FootballRepository fakeFootballRepo;
        MatchValidatorService matchValidatorService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            matchValidatorService = new MatchValidatorService(new PlayerMatchViewModel(fakeFootballRepo), new FakeDialogService());
        }

        [Test]
        public void MatchValidatorService_WhenDataGridIsValidIsCalledAndGridRowHasPlayerAndNoTeamReturnFalse()
        {
            //Arrange 
            var fakePlayerMatchRepo = new FakePlayerMatchRepository();
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            var result = matchValidatorService.DataGridIsValid();

            //Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void MatchValidatorService_WhenDataGridIsValidIsCalledAndGridRowHasTeamAndNoPlayerReturnFalse()
        {
            //Arrange 
            var fakePlayerMatchRepo = new FakePlayerMatchRepository();
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTeamAndNoPlayer();

            //Act
            var result = matchValidatorService.DataGridIsValid();

            //Assert
            Assert.That(result, Is.EqualTo(false)); 
        }
    }
}
