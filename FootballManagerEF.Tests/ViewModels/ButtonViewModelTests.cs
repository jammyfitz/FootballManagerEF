
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
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        MailerService mailerService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            matchValidatorService = new MatchValidatorService(playerMatchViewModel, new FakeDialogService());
            mailerService = new MailerService(playerMatchViewModel, fakeFootballRepo);
            buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mailerService);
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
            var mockButtonViewModel = new ButtonViewModel(mockFootballRepo, new PlayerMatchViewModel(mockFootballRepo), matchValidatorService, mailerService);
            mockButtonViewModel.PlayerMatches = fakePlayerMatchRepo.GetTenPlayerMatches(1);
            mockFootballRepo.Stub(x => x.Save());

            //Act
            mockButtonViewModel.UpdateCommand.Execute(null);

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonClickedAndDataGridIsInvalidSendErrorToUserIsCalled()
        {
            //Arrange 
            var mockMatchValidatorService = MockRepository.GenerateMock<IMatchValidatorService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, mockMatchValidatorService, mailerService);
            mockMatchValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockButtonViewModel.UpdateCommand.Execute(null);

            //Assert
            mockMatchValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }

        [Test]
        public void ButtonViewModel_WhenDataGridIsValidIsCalledAndGridRowHasPlayerAndNoTeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            buttonViewModel.UpdateCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndGridRowHasPlayerAndNoTeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();

            //Act
            buttonViewModel.UpdateCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndGridRowHasTeamAndNoPlayerReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTeamAndNoPlayer();

            //Act
            buttonViewModel.UpdateCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Either the team or the player is missing for one of the entries."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndPlayerAppearsMoreThanOnceReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithDuplicatePlayer();

            //Act
            buttonViewModel.UpdateCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("One of the selected players appears more than once for this match."));
        }

        [Test]
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndMoreThanMaxPlayersInATeamReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithTooManyPlayersInATeam();

            //Act
            buttonViewModel.UpdateCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("One of the teams has more than 5 players."));
        }

        [Test]
        public void ButtonViewModel_WhenEmailStatsIsClickedAndEmailHasBeenSentReturnMessageToUser()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendStats()).Return(true);

            //Act
            mockButtonViewModel.EmailStatsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasCalled(x => x.SendOKMessageToUser());
        }

        [Test]
        public void ButtonViewModel_WhenEmailStatsIsClickedAndEmailHasntBeenSentNoMessageToUser()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendStats()).Return(false);

            //Act
            mockButtonViewModel.EmailStatsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasNotCalled(x => x.SendOKMessageToUser());
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonClickedWithGiantKillerAlgorithmAndDataGridIsCompletePlayerMatchesIsUpdatedCorrectly()
        {
            //Arrange 
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            buttonViewModel.SelectedAlgorithm = new SelectionAlgorithm() { Class = new GiantKillerSelectorService(fakeFootballRepo) };
            ObservableCollection<PlayerMatch> expectedPlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesAfterGiantKillerAlgorithm();
            ObservableCollection<PlayerMatch> inputPlayerMatches = buttonViewModel.PlayerMatches;

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.IsTrue(buttonViewModel.PlayerMatches.SequenceEqual(expectedPlayerMatches, new PlayerMatchEqualityComparer()));
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonClickedWithTheProportionerAlgorithmAndDataGridIsCompletePlayerMatchesIsUpdatedCorrectly()
        {
            //Arrange 
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            buttonViewModel.SelectedAlgorithm = new SelectionAlgorithm() { Class = new TheProportionerSelectorService(fakeFootballRepo) };
            ObservableCollection<PlayerMatch> expectedPlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesAfterTheProportionerAlgorithm();
            ObservableCollection<PlayerMatch> inputPlayerMatches = buttonViewModel.PlayerMatches;

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.IsTrue(buttonViewModel.PlayerMatches.SequenceEqual(expectedPlayerMatches, new PlayerMatchEqualityComparer()));
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonIsClickedAndDataGridIsIncompleteReturnExpectedError()
        {
            //Arrange 
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetFiveFilledAndFiveEmptyPlayerMatches();

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.That(matchValidatorService.ErrorMessage, Is.EqualTo("Please ensure that the maximum number of players and teams are entered."));
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonIsClickedAndDataGridIsInvalidSendErrorToUserIsCalled()
        {
            //Arrange 
            var mockMatchValidatorService = MockRepository.GenerateMock<IMatchValidatorService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, mockMatchValidatorService, mailerService);
            mockMatchValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockButtonViewModel.AutoPickCommand.Execute(null);

            //Assert
            mockMatchValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }

        [Test]
        public void ButtonViewModel_WhenEmailTeamsIsClickedAndEmailHasBeenSentReturnMessageToUser()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendTeams()).Return(true);

            //Act
            mockButtonViewModel.EmailTeamsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasCalled(x => x.SendOKMessageToUser());
        }

        [Test]
        public void ButtonViewModel_WhenEmailTeamsIsClickedAndEmailHasntBeenSentNoMessageToUser()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendTeams()).Return(false);

            //Act
            mockButtonViewModel.EmailTeamsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasNotCalled(x => x.SendOKMessageToUser());
        }
    }
}
