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
     * 
     * Allows the user to select:
     * Usernames, Colour, Difficulty
     * Allows the user to create a new player or load a saved game
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

            //Displays/Hides certain boxes depending on the gamemode
            if (Mode == "Computer")
            {
                Column2.Visibility = Visibility.Collapsed;
                ColourText.Text = "Select Colour";
                Player2Name.Text = "Computer";
                //Only shows saved games against the computer
                Viewer.ChooseSavedGames();
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
            //Gets the data from the text boxes
            P1Name = Player1Name.Text;
            P2Name = Player2Name.Text;
            Game game;
            int GameSaveID = 0;
            //Checks if the user has entered all data or loaded a game
            if (SaveGame.Text == null)
            {
                if (string.IsNullOrWhiteSpace(P1Name) || (Mode != "Computer" && string.IsNullOrWhiteSpace(P2Name)) || (Mode == "Computer" && string.IsNullOrWhiteSpace(Difficulty.Text)) || string.IsNullOrWhiteSpace(Colour.Text))
                {

                    MessageBox.Show("Please enter all required data!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (P1Name == P2Name)
                {
                    MessageBox.Show("Please select 2 usernames", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            //if the user has selected a saved game, load it
            if (!string.IsNullOrWhiteSpace(SaveGame.Text))
            {
                GameSaveID = int.Parse(SaveGame.Text);
                game = Database.LoadGame(GameSaveID);
            }
            else
            {
                //If a saved game is not selected, create a new game
                game = null;
                if (Colour.Text == "Yellow") FirstPlayer = "Y";
                else if (Colour.Text == "Red") FirstPlayer = "R";
                //if randomly generated number is even, first Player is red
                else if (Rgen.Next(101) % 2 == 0) FirstPlayer = "R";
                else FirstPlayer = "Y";
            }


            //Creates a new game with the selected data and depending on the gamemode
            if (Mode == "User")
            {
                
                Window w = new PlayUser(FirstPlayer, P1Name, P2Name, game, GameSaveID);
                w.Show();
                this.Close();
            }
            else
            {
                Window w = new PlayComputer(FirstPlayer, P1Name, Difficulty.Text,game, GameSaveID);
                w.Show();
                this.Close();
            }
        }

        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            //Adds a new player to the database
            string name = NewUser.Text.Trim();
            //Checks if the name is valid
            if (string.IsNullOrWhiteSpace(name) || name=="Enter Username")
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
            //Closes the window
            Window w = new MainMenu();
            w.Show();
            this.Close();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            //Method for recognising keystrokes
            if (e.Key == Key.Escape)
            {
                Close(sender, e);
            }
        }

        private void NewUser_GotFocus(object sender, RoutedEventArgs e)
        {
            //Clears the text box when it is clicked
            NewUser.Clear();
        }

    }
}
