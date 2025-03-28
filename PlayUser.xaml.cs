using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for PlayUser.xaml
    /// </summary>


    /*
     * PLAY USER
     * Allows 2 players to play against each other
     * Can load a saved game if selected
     */
    public partial class PlayUser : Window
    {
        private Game Connect4;
        private string Player1Name;
        private string Player2Name;
        private string Player1Colour;
        private DatabaseAccess Database;
        private int GameSaveID;


        public PlayUser(string colour, string P1Name, string P2Name, Game Game)
        {
            InitializeComponent();
            this.Focus();
            Player1Name = P1Name;
            Player2Name = P2Name;
            Player1Colour = colour;
            Database = new DatabaseAccess();

            //Loads a saved game if selected
            if (Game!= null) Connect4 = Game;
			else Connect4 = new Game(Player1Colour, Player1Name, Player2Name, "None");


            DataContext = Connect4;


            this.KeyDown += new KeyEventHandler(KeyPressed); //Event handler to recognise key presses


        }


        //Clicks to place counter in each Column
        private void Column1_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(0);
        }

        private void Column2_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(1);
        }

        private void Column3_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(2);
        }

        private void Column4_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(3);
        }

        private void Column5_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(4);
        }

        private void Column6_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(5);
        }

        private void Column7_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(6);
        }


        private void KeyPressed(object sender, KeyEventArgs e) //Method for recognising keystrokes
        {
            if (e.Key == Key.Escape)
            {
                Close(sender, e);
            }
            //Allows the use of the numpad or the top Row of numbers
            if (e.Key == Key.D1 || e.Key==Key.NumPad1)
            {
                Column1_Click(sender, e);
            }
            
            if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                Column2_Click(sender,e);
            }
            
            if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                Column3_Click(sender, e);
            }

            if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                Column4_Click(sender, e);
            }
            
            if (e.Key == Key.D5 || e.Key == Key.NumPad5)
            {
                Column5_Click(sender, e);
            }
            
            if (e.Key == Key.D6 || e.Key == Key.NumPad6)
            {
                Column6_Click(sender, e);
            }
            
            if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                Column7_Click(sender, e);
            }
        }

        private void Close(object sender, RoutedEventArgs e) //Closes the game
        {
            
            Window w = new PauseMenu();
            w.Owner = this;
            w.ShowDialog();
        }

        private void Restart(object sender, RoutedEventArgs e) //Restarts the game with the original settings
        {
            Window w = new PlayUser(Player1Colour, Player1Name, Player2Name, Connect4);
            w.Show();
            this.Close();

            

        }

        private void PlaceCounter(int C)
        {
            //Places counter and assings the status of the game after placing a counter
            string Status;
            Status=Connect4.PlaceCounter(C);
            if (Status == "Win")
            {
                //if someone wins end the game and indicate who
                if (Connect4.board.Player.Colour == Player1Colour) GameEnd(Player1Name);
                else GameEnd(Player2Name);
            }
            //if a draw, also end the game where no one wins
            else if (Status == "Draw") GameEnd("No one");
            
        }

        private async void GameEnd(string Winner)
        {
            //Prevents the user placing any more Counters
            Column1.Visibility = Visibility.Collapsed;
            Column2.Visibility = Visibility.Collapsed;
            Column3.Visibility = Visibility.Collapsed;
            Column4.Visibility = Visibility.Collapsed;
            Column5.Visibility = Visibility.Collapsed;
            Column6.Visibility = Visibility.Collapsed;
            Column7.Visibility = Visibility.Collapsed;
            //Prevents the user starting a new game
            RestartButton.Visibility = Visibility.Hidden;
            CloseButton.Visibility = Visibility.Hidden;
            CurrentPlayer.Visibility = Visibility.Hidden;
            SaveGameButton.Visibility = Visibility.Hidden;

            //Displays a message to show the winner and make it visible
            GameWinner.Text = $"{Winner} Wins";
            GameWinner.Visibility = Visibility.Visible;
            //wait 5s before closing the window and returning to the main menu
            await Task.Delay(5000);
            Window w = new MainMenu();
            w.Show();
            this.Close();
        }

        private void SaveGame(object sender, RoutedEventArgs e)
        {
            //Saves the game
            GameSaveID = Database.SaveGame(Connect4, GameSaveID);
		}



    }
}
