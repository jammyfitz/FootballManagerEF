using FootballManagerEF.Interfaces;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class DialogSelectorService : IDialogSelectorService
    {
        public MessageBoxResult ShowDialog(string messageBoxText, string title)
        {
            return MessageBox.Show(messageBoxText, title, MessageBoxButton.YesNo);
        }
    }
}
