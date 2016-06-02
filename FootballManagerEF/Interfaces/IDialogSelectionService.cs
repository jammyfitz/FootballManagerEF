
using System.Windows;

namespace FootballManagerEF.Interfaces
{
    public interface IDialogSelectionService
    {
        MessageBoxResult ShowDialog(string messageBoxText, string title);
    }
}
