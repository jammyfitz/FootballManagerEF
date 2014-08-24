
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
        FootballRepository fakeFootballRepo;
        PlayerMatchViewModel playerMatchViewModel;
        FakePlayerMatchRepository fakePlayerMatchRepo;
        MatchValidatorService matchValidatorService;
        ButtonViewModel buttonViewModel;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            matchValidatorService = new MatchValidatorService(new PlayerMatchViewModel(fakeFootballRepo), new FakeDialogService());
            buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService);
        }

        [Test]
        public void ButtonViewModel_WhenInsertPlayerMatchesIsCalledOnlyFilledRecordsAreInserted()
        {
            //Arrange 
            playerMatchViewModel.PlayerMatches = fakeFootballRepo.GetFiveFilledAndFiveEmptyPlayerMatches();

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
            var mockButtonViewModel = new ButtonViewModel(mockFootballRepo, new PlayerMatchViewModel(mockFootballRepo), matchValidatorService);
            mockButtonViewModel.PlayerMatches = fakePlayerMatchRepo.GetTenPlayerMatches(1);
            mockFootballRepo.Stub(x => x.Save());

            //Act
            mockButtonViewModel.UpdateButtonClicked();

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonClickedAndDataGridIsInvalidSendErrorToUserIsCalled()
        {
            //Arrange 

            var mockMatchValidatorService = MockRepository.GenerateMock<IMatchValidatorService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, mockMatchValidatorService);
            mockMatchValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockButtonViewModel.UpdateButtonClicked();

            //Assert
            mockMatchValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }

        [Test]
        public void ButtonViewModel_WhenDataGridIsValidIsCalledAndGridRowHasPlayerAndNoTeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndGridRowHasPlayerAndNoTeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndGridRowHasTeamAndNoPlayerReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTeamAndNoPlayer();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndPlayerAppearsMoreThanOnceReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithDuplicatePlayer();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("One of the selected players appears more than once for this match."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndMoreThanMaxPlayersInATeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTooManyPlayersInATeam();

            //Act
            buttonViewModel.UpdateButtonClicked();

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("One of the teams has more than 5 players."));
        }
    }
}
