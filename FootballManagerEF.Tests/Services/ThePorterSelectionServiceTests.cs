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
        public void ThePorterSelectionService_AppliesAlgorithmAndReordersTeams()
        {
            //Arrange 
            var playerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            const int bibsTeamId = 1;

            //Act
            var result = porterSelectorService.ApplyAlgorithm(playerMatches);

            //Assert
            Assert.That(result.Count, Is.EqualTo(playerMatches.Count()));
            Assert.That(result.Count(x => x.TeamID == bibsTeamId), Is.EqualTo(5));
            Assert.That(result.Count(x => x.TeamID != bibsTeamId), Is.EqualTo(5));
        }
    }
}
