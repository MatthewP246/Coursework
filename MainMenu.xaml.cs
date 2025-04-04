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
using System.Windows.Shapes;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary
    
    /*
     * MAIN MENU
     * 
     * allows the user to select which mode they play, look at the rules or see the leaderboard
     */
    public partial class MainMenu : Window
    {

        private PlayerViewer Viewer;
        public MainMenu()
        {
            InitializeComponent();
            Viewer = new PlayerViewer();
            DataContext = Viewer;


            this.KeyDown += new KeyEventHandler(KeyPressed);
        }

        //Opens the game settings window with the selected mode
        private void PlayUser(object sender, RoutedEventArgs e)
        {
            Window w = new GameSettings("User");
            w.Show();
            this.Close();
        }

        private void PlayComputer(object sender, RoutedEventArgs e)
        {
            Window w = new GameSettings("Computer");
            w.Show();
            this.Close();
        }

        private void Rules(object sender, RoutedEventArgs e)
        {
            //Opens the rules window
            Window w = new Rules();
            w.Show();
            this.Close();
        }



        //Used to recognise keystrokes
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
