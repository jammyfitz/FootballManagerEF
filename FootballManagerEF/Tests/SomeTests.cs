using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FootballManagerEF.EFModel;
using FootballManagerEF.Controllers;
using FootballManagerEF.Repositories;

namespace FootballManagerEF.Tests
{
    [TestFixture]
    public class SomeTests
    {
        [Test]
        public void Matches_WhenGetMatchesIsCalledReturnsListOfMatches()
        {
            //Arrange 
            var fakeMatchRepo = new FakeMatchRepository();
            var matchController = new MatchController(fakeMatchRepo);

            //Act
            var result = matchController.GetMatches();

            //Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}
