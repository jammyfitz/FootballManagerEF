using FootballManagerEF.Interfaces;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowMessageBox(string message)
        {
            MessageBox.Show(message);
            return true;
        }
    }
}
