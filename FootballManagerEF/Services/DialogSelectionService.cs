using FootballManagerEF.Interfaces;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class DialogSelectionService : IDialogSelectionService
    {
        public MessageBoxResult ShowDialog(string messageBoxText, string title)
        {
            return MessageBox.Show(messageBoxText, title, MessageBoxButton.YesNo);
        }
    }
}
