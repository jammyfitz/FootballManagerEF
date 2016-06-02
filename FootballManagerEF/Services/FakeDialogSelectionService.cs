using FootballManagerEF.Interfaces;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class FakeDialogSelectionService : IDialogSelectionService
    {
        public MessageBoxResult ShowDialog(string messageBoxText, string title)
        {
            return MessageBoxResult.Yes;
        }
    }
}
