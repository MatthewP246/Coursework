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

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for PlayUser.xaml
    /// </summary>
    public partial class PlayUser : Window
    {
        private Game Connect4;
        private string Player1Name;
        private string Player2Name;
        private string Player1Colour;
        private string Player2Colour;
        private DispatcherTimer GameTime;
        private int TimeLeft=600;

        public PlayUser(string colour, string P1Name, string P2Name)
        {
            InitializeComponent();
            this.Focus();
            Player1Name = P1Name;
            Player2Name = P2Name;
            Player1Colour = colour;
            if (Player1Colour == "R") Player2Colour = "Y";
            else Player2Colour = "R";

            Connect4 = new Game(Player1Colour, false, Player1Name, Player2Name, "");

            DataContext = Connect4;


            this.KeyDown += new KeyEventHandler(KeyPressed); //Event handler to recognise key presses


        }


        //Clicks to place counter in each Column
        private void Column1_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(0);
            Column1.Height = Column1.Height - 150;
        }

        private void Column2_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(1);
            Column2.Height = Column2.Height - 150;
        }

        private void Column3_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(2);
            Column3.Height = Column3.Height - 150;
        }

        private void Column4_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(3);
            Column4.Height = Column4.Height - 150;
        }

        private void Column5_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(4);
            Column5.Height = Column5.Height - 150;
        }

        private void Column6_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(5);
            Column6.Height = Column6.Height - 150;
        }

        private void Column7_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(6);
            Column7.Height = Column7.Height - 150;
        }


        private void KeyPressed(object sender, KeyEventArgs e) //Method for recognising keystrokes
        {
            if (e.Key == Key.Escape)
            {
                Cross_Click(sender, e);
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

        private void Cross_Click(object sender, RoutedEventArgs e) //Closes the game
        {
            GameTime.Stop();
            Window w = new PauseMenu();
            w.Owner = this;
            w.ShowDialog();
            //if thePause menu is closed, restart the timer
            GameTime.Start();
        }

        private void Restart_Click(object sender, RoutedEventArgs e) //Restarts the game with the original settings
        {
            Window w = new PlayUser(Player1Colour, Player1Name, Player2Name);
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
                if (Connect4.board.player.Colour == Player1Colour) GameWin(Player1Name);
                else GameWin(Player2Name);
            }
            //if a draw, also end the game where no one wins
            else if (Status == "Draw") GameWin("No one");
            
        }

        private async void GameWin(string Winner)
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
            Restart.Visibility = Visibility.Hidden;
            Close.Visibility = Visibility.Hidden;
            CurrentPlayer.Visibility = Visibility.Hidden;

            //Stops the game time
            GameTime.Stop();
            //Displays a message to show the winner and make it visible
            GameWinner.Text = $"{Winner} Wins";
            GameWinner.Visibility = Visibility.Visible;
            //wait 5s before closing the window and returning to the main menu
            await Task.Delay(5000);
            Application.Current.MainWindow.Show();
            this.Close();
        }




    }
}
