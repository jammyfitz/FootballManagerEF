
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

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerRepo = new FakePlayerRepository();
            playerValidatorService = new PlayerValidatorService(new FakeDialogService());
            playerViewModel = new PlayerViewModel(fakeFootballRepo, playerValidatorService);
        }

        [Test]
        [Ignore]
        public void PlayerViewModel_WhenUpdateButtonClickedAndDataGridIsValidSaveWasCalledOnFootballRepository()
        {
            //Arrange 
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var mockPlayerViewModel = new PlayerViewModel(mockFootballRepo, playerValidatorService);
            mockPlayerViewModel.Players = fakePlayerRepo.GetTenValidPlayers();
            mockFootballRepo.Stub(x => x.Save());

            //Act
            mockPlayerViewModel.UpdateButtonClicked();

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
        }

        [Test]
        public void PlayerViewModel_WhenUpdateButtonClickedAndDataGridIsInvalidSendErrorToUserIsCalled()
        {
            //Arrange
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var mockPlayerValidatorService = MockRepository.GenerateMock<IPlayerValidatorService>();
            var mockPlayerViewModel = new PlayerViewModel(mockFootballRepo, playerValidatorService);
            mockPlayerValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockPlayerViewModel.UpdateButtonClicked();

            //Assert
            mockPlayerValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }

    }
}
