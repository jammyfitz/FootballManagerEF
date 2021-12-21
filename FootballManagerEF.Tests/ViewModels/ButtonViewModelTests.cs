
using FootballManagerEF.Repositories;
using FootballManagerEF.ViewModels;
using NUnit.Framework;
using System.Linq;
using Rhino.Mocks;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Services;
using System.Collections.ObjectModel;

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
        public void Setup()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
        }

        [SetUp]
        public void Init()
        {
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
        public void ButtonViewModel_WhenUpdateButtonIsClickedAndGridRowHasPlayerAndNoTeamSaveWasCalledOnFootballRepository()
        {
            //Arrange
            var mockFootballRepo = MockRepository.GenerateMock<IFootballRepository>();
            var mockButtonViewModel = new ButtonViewModel(mockFootballRepo, new PlayerMatchViewModel(mockFootballRepo), matchValidatorService, mailerService);
            matchValidatorService.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesWithPlayerAndNoTeam();
            mockFootballRepo.Stub(x => x.Save());

            //Act
            mockButtonViewModel.UpdateCommand.Execute(null);

            //Assert
            mockFootballRepo.AssertWasCalled(x => x.Save());
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
        public void ButtonViewModel_WhenEmailStatsIsClickedAndEmailHasBeenSentReturnOKToUser()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendStats()).Return(true);

            //Act
            mockButtonViewModel.EmailStatsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasCalled(x => x.SendOKToUser());
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
            mockMailerService.AssertWasNotCalled(x => x.SendOKToUser());
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonIsClickedWithGiantKillerAlgorithmAndDataGridIsCompletePlayerMatchesIsUpdatedCorrectly()
        {
            //Arrange 
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            buttonViewModel.SelectedAlgorithm = new SelectionAlgorithm() { Class = new GiantKillerSelectorService(fakeFootballRepo) };
            ObservableCollection<PlayerMatch> expectedPlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesAfterGiantKillerAlgorithm();

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.IsTrue(buttonViewModel.PlayerMatches.SequenceEqual(expectedPlayerMatches, new PlayerMatchEqualityComparer()));
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonIsClickedWithTheProportionerAlgorithmAndDataGridIsCompletePlayerMatchesIsUpdatedCorrectly()
        {
            //Arrange 
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            buttonViewModel.SelectedAlgorithm = new SelectionAlgorithm() { Class = new TheProportionerSelectorService(fakeFootballRepo) };
            ObservableCollection<PlayerMatch> expectedPlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesAfterTheProportionerAlgorithm();

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.IsTrue(buttonViewModel.PlayerMatches.SequenceEqual(expectedPlayerMatches, new PlayerMatchEqualityComparer()));
        }

        [Test]
        public void ButtonViewModel_WhenAutoPickButtonIsClickedWithThePorterAlgorithmAndDataGridIsCompletePlayerMatchesIsUpdatedCorrectly()
        {
            //Arrange 
            buttonViewModel.PlayerMatches = fakePlayerMatchRepo.GetPlayerMatchesBeforeAlgorithm();
            buttonViewModel.SelectedAlgorithm = new SelectionAlgorithm() { Class = new ThePorterSelectorService(fakeFootballRepo) };
            ObservableCollection<PlayerMatch> inputPlayerMatches = new ObservableCollection<PlayerMatch>(buttonViewModel.PlayerMatches);

            //Act
            buttonViewModel.AutoPickCommand.Execute(null);

            //Assert
            Assert.IsFalse(buttonViewModel.PlayerMatches.SequenceEqual(inputPlayerMatches, new PlayerMatchEqualityComparer()));
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
        public void ButtonViewModel_WhenEmailTeamsIsClickedAndDataGridIsCompleteSendTeamsIsCalled()
        {
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendTeams()).Return(true);

            //Act
            mockButtonViewModel.EmailTeamsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasCalled(x => x.SendTeams());
        }

        [Test]
        public void ButtonViewModel_WhenEmailTeamsIsClickedAndDataGridIsCompleteReturnOKToUser()
        {   
            //Arrange 
            var mockMailerService = MockRepository.GenerateMock<IMailerService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mockMailerService);
            mockMailerService.Stub(x => x.SendTeams()).Return(true);

            //Act
            mockButtonViewModel.EmailTeamsCommand.Execute(null);

            //Assert
            mockMailerService.AssertWasCalled(x => x.SendOKToUser());
        }

        [Test]
        public void ButtonViewModel_WhenEmailTeamsIsClickedAndDataGridIsIncompleteSendErrorToUser()
        {
            //Arrange 
            var mockMatchValidatorService = MockRepository.GenerateMock<IMatchValidatorService>();
            var mockButtonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, mockMatchValidatorService, mailerService);
            mockMatchValidatorService.Stub(x => x.DataGridIsValid()).Return(false);

            //Act
            mockButtonViewModel.EmailTeamsCommand.Execute(null);

            //Assert
            mockMatchValidatorService.AssertWasCalled(x => x.SendErrorToUser());
        }
    }
}
