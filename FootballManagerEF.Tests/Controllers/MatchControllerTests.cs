using FootballManagerEF.Controllers;
using FootballManagerEF.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Tests.Controllers
{
    [TestFixture]
    public class MatchControllerTests
    {
        [Test]
        public void MatchController_WhenGetMatchesIsCalledReturnsListOfMatches()
        {
            //Arrange 
            var fakeMatchRepo = new FakeMatchRepository();
            var matchController = new MatchController(fakeMatchRepo);

            //Act
            var result = matchController.GetMatches();

            //Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        // MatchController_WhenGetMatchesIsCalledReturnsListOfMatchesFromTwoWeeksAgo()

        // MatchController_WhenGetMatchesIsCalledDoesntReturnListOfMatchesLongerThanTwoWeeksAgo()

        // MatchController_WhenGetMatchesIsCalledReturnsListOfMatchesInChronologicalOrder()
    }
}
