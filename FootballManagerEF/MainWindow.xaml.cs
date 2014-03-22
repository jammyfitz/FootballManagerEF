using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballManagerEF
{
    /// *** Initial Thoughts to Class Hierarchy ***
    /// 
    /// A Team has many Players
    /// A Match has many Players and a Player plays many Matches
    /// A Game is a single Match
    /// A Game has many Players
    /// A Game has many Teams
    /// 
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
