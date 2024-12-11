using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            this.Focus();
            this.KeyDown += new KeyEventHandler(KeyPressed);
        }

         

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {

            Window w = new PlayUser("1");
            this.Hide();
            w.ShowDialog();
            

        }

        private void Rules_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
