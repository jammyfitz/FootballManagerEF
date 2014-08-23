using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
