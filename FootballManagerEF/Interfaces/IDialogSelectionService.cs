
using System.Windows;

namespace FootballManagerEF.Interfaces
{
    public interface IDialogSelectorService
    {
        MessageBoxResult ShowDialog(string messageBoxText, string title);
    }
}
