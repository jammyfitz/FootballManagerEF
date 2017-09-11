
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
    public class PlayerViewModelTests
    {
        FootballRepository fakeFootballRepo;
        PlayerViewModel playerViewModel;
        FakePlayerRepository fakePlayerRepo;
        PlayerValidatorService playerValidatorService;
        PlayerMatchViewModel playerMatchViewModel;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerRepo = new FakePlayerRepository();
            playerValidatorService = new PlayerValidatorService(new FakeDialogService());
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            playerViewModel = new PlayerViewModel(fakeFootballRepo, playerMatchViewModel, playerValidatorService);
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonClickedAndDataGridIsValidSaveWasCalledOnFootballRepository()
        {
            //Arrange 
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var mockPlayerValidatorService = MockRepository.GenerateMock<IPlayerValidatorService>();
            var mockPlayerMatchViewModel = MockRepository.GenerateMock<IPlayerMatchViewModel>();
            var mockPlayerViewModel = new PlayerViewModel(mockFootballRepo, mockPlayerMatchViewModel, mockPlayerValidatorService);
            mockPlayerViewModel.Players = fakePlayerRepo.GetTenValidPlayers();
            mockPlayerValidatorService.Stub(x => x.DataGridIsValid()).Return(true);
            mockFootballRepo.Stub(x => x.Save());

            //Act
            mockPlayerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonClickedAndDataGridIsInvalidSendErrorToUserIsCalled()
        {
            //Arrange
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var mockPlayerValidatorService = MockRepository.GenerateMock<IPlayerValidatorService>();
            var mockPlayerViewModel = new PlayerViewModel(mockFootballRepo, playerMatchViewModel, mockPlayerValidatorService);
            mockPlayerValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockPlayerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            mockPlayerValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }

        [Test]
        public void PlayerViewModel_WhenDataGridIsValidIsCalledAndGridRowHasPlayerNameAndNoActiveFlagReturnExpectedError()
        {
            //Arrange 
            playerValidatorService.Players = fakePlayerRepo.GetPlayersWithPlayerNameAndNoActiveFlag();

            //Act
            playerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            Assert.That(playerValidatorService.ErrorMessage, Is.EqualTo("Either the player or the active field is missing for one of the entries."));
        }

        [Test]
        public void PlayerViewModel_WhenDataGridIsValidIsCalledAndGridRowHasActiveFlagAndNoPlayerNameReturnExpectedError()
        {
            //Arrange 
            playerValidatorService.Players = fakePlayerRepo.GetPlayersWithActiveFlagAndNoPlayerName();

            //Act
            playerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            Assert.That(playerValidatorService.ErrorMessage, Is.EqualTo("Either the player or the active field is missing for one of the entries."));
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonIsClickedAndPlayerAppearsMoreThanOnceReturnExpectedError()
        {
            //Arrange 
            playerValidatorService.Players = fakePlayerRepo.GetPlayersWithDuplicatePlayerName();

            //Act
            playerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            Assert.That(playerValidatorService.ErrorMessage, Is.EqualTo("One of the players with that name already exists."));
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonIsClickedAndPlayerHasNonAlphaCharactersReturnExpectedError()
        {
            //Arrange 
            playerValidatorService.Players = fakePlayerRepo.GetPlayersWithNonAlphaCharacters();

            //Act
            playerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            Assert.That(playerValidatorService.ErrorMessage, Is.EqualTo("One of the players has Non-Alphabetic characters."));
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonIsClickedAndPlayerHasInvalidEmailAddressReturnExpectedError()
        {
            //Arrange 
            playerValidatorService.Players = fakePlayerRepo.GetPlayersWithInvalidEmailAddress();

            //Act
            playerViewModel.UpdatePlayersCommand.Execute(null);

            //Assert
            Assert.That(playerValidatorService.ErrorMessage, Is.EqualTo("One of the players has an invalid e-mail address."));
        }
    }
}
