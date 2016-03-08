using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Tests.Services
{
    [TestFixture]
    public class ThePorterSelectionServiceTests
    {
        FootballRepository fakeFootballRepo;
        FakePlayerMatchRepository fakePlayerMatchRepo;
        ThePorterSelectorService porterSelectorService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
            porterSelectorService = new ThePorterSelectorService(fakeFootballRepo);
        }

        [Test]
        public void ThePorterSelectionService_ReordersTeamsWithShortestPlayerOnBibs()
        {
            //Arrange 
            var playerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            const int bibsTeamId = 1;

            //Act
            var result = porterSelectorService.ApplyAlgorithm(playerMatches);

            //Assert
            Assert.That(result.First(x => x.PlayerID == 10).TeamID == bibsTeamId);
        }
    }
}
