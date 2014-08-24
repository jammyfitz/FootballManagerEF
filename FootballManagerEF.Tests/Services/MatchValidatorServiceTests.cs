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
        FakePlayerMatchRepository fakePlayerMatchRepo;
        MatchValidatorService matchValidatorService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
            matchValidatorService = new MatchValidatorService(new PlayerMatchViewModel(fakeFootballRepo), new FakeDialogService());
        }

        [Test]
        public void MatchValidatorService_WhenDataGridIsValidIsCalledAndGridRowHasPlayerAndNoTeamReturnFalse()
        {
            //Arrange 
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
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTeamAndNoPlayer();

            //Act
            var result = matchValidatorService.DataGridIsValid();

            //Assert
            Assert.That(result, Is.EqualTo(false)); 
        }
    }
}
