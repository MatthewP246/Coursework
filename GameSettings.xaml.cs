using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;


namespace Connect4
{
    /*
     * GAME SETTINGS
     * Allows the user to select:
     * Usernames, Colour, Difficulty
     * Allows the user to create a new player
     */
    public partial class GameSettings : Window 
    {
        private string Mode;
        private string FirstPlayer;
        private string P1Name;
        private string P2Name;
        private Random Rgen = new Random();
        private DatabaseAccess Database;

        private PlayerViewer Viewer;


        public GameSettings(string Gamemode)
        {
            InitializeComponent();
            Mode = Gamemode;
            Viewer = new PlayerViewer();
            Database = new DatabaseAccess();
            DataContext = Viewer;

            if (Mode == "Computer")
            {
                Column2.Visibility = Visibility.Collapsed;
                ColourText.Text = "Select Colour";
            }
            else
            {
                Difficulty.Visibility = Visibility.Collapsed;
                DifficultyText.Visibility = Visibility.Collapsed;
            }
                this.KeyDown += new KeyEventHandler(KeyPressed);

        }

        private void Start(object sender, RoutedEventArgs e)
        {
            
            P1Name = Player1Name.Text;
            P2Name = Player2Name.Text;
            if(string.IsNullOrWhiteSpace(P1Name) || (Mode!="Computer" && string.IsNullOrWhiteSpace(P2Name)) ||(Mode=="Computer" && string.IsNullOrWhiteSpace(Difficulty.Text)) || string.IsNullOrWhiteSpace(Colour.Text))
            {
                
                MessageBox.Show("Please enter all required data!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (P1Name == P2Name)
            {
                MessageBox.Show("Please select 2 usernames","Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Colour.Text == "Yellow") FirstPlayer = "Y";
            else if (Colour.Text == "Red") FirstPlayer = "R";
            //if randomly generated number is even, first Player is red
            else if (Rgen.Next(101) % 2 == 0) FirstPlayer = "R";
            else FirstPlayer = "Y";
            if (Mode == "User")
            {
                
                Window w = new PlayUser(FirstPlayer, P1Name, P2Name, null, 1);
                w.Show();
                this.Close();
            }
            else
            {
                Window w = new PlayComputer(FirstPlayer, P1Name, Difficulty.Text,null);
                w.Show();
                this.Close();
            }
        }

        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            string name = NewUser.Text.Trim();
            if(string.IsNullOrWhiteSpace(name) || name=="Enter Username")
            {
                MessageBox.Show("Please enter a valid name!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Viewer.AddPlayer(name);
            NewUser.Clear();
            NewUser.Text = "Enter Username";
            
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Window w = new MainMenu();
            w.Show();
            this.Close();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close(sender, e);
            }
        }

        private void NewUser_GotFocus(object sender, RoutedEventArgs e)
        {
            NewUser.Clear();
        }

    }
}
