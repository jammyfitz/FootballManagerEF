using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
using FootballManagerEF.ViewModels;
using NUnit.Framework;
using System.Linq;

namespace FootballManagerEF.Tests.ViewModels
{
    [TestFixture]
    public class MatchButtonViewModelTests
    {
        FootballRepository fakeFootballRepo;
        MatchViewModel matchViewModel;
        MatchButtonViewModel matchButtonViewModel;
        FakeMatchRepository fakeMatchRepo;
        ButtonViewModel buttonViewModel;
        PlayerMatchViewModel playerMatchViewModel;
        MailerService fakeMailerService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakeMatchRepo = new FakeMatchRepository();
            matchViewModel = new MatchViewModel(fakeFootballRepo);
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            matchButtonViewModel = new MatchButtonViewModel(fakeFootballRepo, matchViewModel, new FakeDialogSelectorService());
            fakeMailerService = new MailerService(playerMatchViewModel, fakeFootballRepo);
            buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, new FakeMatchValidatorService(true), fakeMailerService);
            matchViewModel.PlayerMatchViewModel = playerMatchViewModel;
            matchViewModel.ButtonViewModel = buttonViewModel;
        }

        [Test]
        public void MatchButtonViewModel_WhenCreateMatchButtonIsClickedAMatchIsAddedToMatches()
        {
            //Arrange
            matchViewModel.Matches = fakeMatchRepo.GetTwoMatches();

            //Act
            matchButtonViewModel.CreateMatchCommand.Execute(null);

            //Assert
            Assert.That(matchViewModel.Matches.Count(), Is.EqualTo(3));
        }

        [Test]
        public void MatchButtonViewModel_WhenDeleteMatchButtonIsClickedAMatchIsDeletedFromMatches()
        {
            //Arrange
            matchViewModel.Matches = fakeMatchRepo.GetTwoMatches();
            matchViewModel.SelectedMatch = matchViewModel.Matches.First();

            //Act
            matchButtonViewModel.DeleteMatchCommand.Execute(null);

            //Assert
            Assert.That(matchViewModel.Matches.Count(), Is.EqualTo(1));
        }
    }
}
