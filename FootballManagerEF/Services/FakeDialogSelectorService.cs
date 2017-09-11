using FootballManagerEF.Interfaces;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class FakeDialogSelectorService : IDialogSelectorService
    {
        public MessageBoxResult ShowDialog(string messageBoxText, string title)
        {
            return MessageBoxResult.Yes;
        }
    }
}
