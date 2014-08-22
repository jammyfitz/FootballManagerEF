﻿
using FootballManagerEF.Repositories;
using FootballManagerEF.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Services;

namespace FootballManagerEF.Tests.ViewModels
{
    [TestFixture]
    public class ButtonViewModelTests
    {
        [Test]
        public void ButtonViewModel_WhenInsertPlayerMatchesIsCalledOnlyFilledRecordsAreInserted()
        {
            //Arrange 
            var fakeFootballRepo = new FootballRepository();
            var playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            playerMatchViewModel.PlayerMatches = fakeFootballRepo.GetFiveFilledAndFiveEmptyPlayerMatches();
            var buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, null);

            //Act
            var result = buttonViewModel.GetPlayerMatchesToInsert();

            //Assert
            Assert.That((int)result.Select(x => (x.MatchID & x.TeamID)!= null).Count(), Is.EqualTo(5));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonClickedAndDataGridIsValidSaveWasCalledOnFootballRepository()
        {
            //Arrange 
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var fakeFootballRepo = new FootballRepository();
            var buttonViewModel = new ButtonViewModel(mockFootballRepo, new PlayerMatchViewModel(mockFootballRepo), null);
            buttonViewModel.PlayerMatches = fakeFootballRepo.GetTenPlayerMatches(1);
            mockFootballRepo.Stub(x => x.GetTenPlayerMatches(Arg<int>.Is.Anything)).Return(fakeFootballRepo.GetTenPlayerMatches(1));
            mockFootballRepo.Stub(x => x.Save());

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonClickedAndDataGridIsInvalidAnErrorMessageIsReturned()
        {
            //Arrange 
            var fakePlayerMatchRepo = new FakePlayerMatchRepository();
            var fakeFootballRepository = new FootballRepository();
            var buttonViewModel = new ButtonViewModel(fakeFootballRepository, new PlayerMatchViewModel(fakeFootballRepository), new FakeDialogService());
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(buttonViewModel.ErrorMessage, Is.Not.Null.Or.Empty);
        }
    }
}
