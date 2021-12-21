using FootballManagerEF.Helpers;
using FootballManagerEF.Models;
using FootballManagerEF.Services;
using NUnit.Framework;

namespace FootballManagerEF.Tests.Helpers
{
    [TestFixture]
    public class SelectorServiceHelperTests
    {
        [Test]
        public void SelectorServiceHelper_ShouldDefaultScoreWhenNotPlayedBefore()
        {
            //Arrange 
            var playerCalculation = new PlayerCalculation
            {
                MatchesPlayed = 0,
                WinRatio = 0,
                RecentMatchWins = 0
            };
            var expectedResult = 1m;

            //Act
            var result = SelectorServiceHelper.GetPlayerScore(playerCalculation);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SelectorServiceHelper_ShouldCalculateScore()
        {
            //Arrange 
            var playerCalculation = new PlayerCalculation
            {
                MatchesPlayed = 5,
                WinRatio = 100,
                RecentMatchWins = 5
            };
            var expectedResult = 2m;

            //Act
            var result = SelectorServiceHelper.GetPlayerScore(playerCalculation);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
