using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for WinScreen.xaml
    /// </summary>
    public partial class WinScreen : Window
    {
        private DatabaseAccess Database;
        public WinScreen(string Winner)
        {
            InitializeComponent();
            Playerwin.Text = Winner + " Wins";
            CloseWindow();

        }

        private async void CloseWindow()
        {
            await Task.Delay(5000);
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}
