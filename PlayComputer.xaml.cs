﻿using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for PlayComputer.xaml
    /// </summary>
    public partial class PlayComputer : Window
    {

        private Game Connect4;
        private string Colour;
        private string PlayerName;
        private string Difficulty;
        public PlayComputer(string colour, string P1, string Diff, Game Game)
        {
            InitializeComponent();
            Colour = colour;
            PlayerName = P1;
            Difficulty = Diff;
            DifficultyText.Text = $"Difficulty: {Difficulty}";

            if (Game != null) Connect4 = Game;
            else Connect4 = new Game(Colour, PlayerName, "Computer", Difficulty);


            DataContext = Connect4;
            this.KeyDown += new KeyEventHandler(KeyPressed);

        }

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

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close(sender, e);
            }
            //Allows the use of the numpad or the top Row of numbers
            if (e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                Column1_Click(sender, e);
            }

            if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                Column2_Click(sender, e);
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

        private void Close(object sender, RoutedEventArgs e)
        {
            Window w = new PauseMenu();
            w.Owner = this;
            w.ShowDialog();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            
            Window w = new PlayComputer(Colour, PlayerName, Difficulty, null);
            w.Show();
            this.Close();
        }

        private async void PlaceCounter(int C)
        {
            string Status;
            Status = Connect4.PlaceCounter(C);
            if (Status == "Draw") GameEnd();
            else if (Status == "Win") GameEnd(true);
            else if(Status != "No Move")
            {
                await Task.Delay(500);
                Status = Connect4.PlaceCounter(-1);
                if (Status == "Draw") GameEnd();
                if (Status == "Win") GameEnd(false);
            }
            
            
        }

        private async void GameEnd()
        {
            //Prevents the user placing any more counters
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

            GameWinner.Text = "No one Wins";
            GameWinner.Visibility = Visibility.Visible;

            await Task.Delay(5000);
            Application.Current.MainWindow.Show();
            this.Close();
        }

        //Overload for GameEnd method
        private async void GameEnd(bool Win)
        {
            //Prevents the user placing any more counters
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

            GameWinner.Text = $"You {(Win?"Win":"Lose")}";
            GameWinner.Visibility = Visibility.Visible;
            
            await Task.Delay(5000);
            Application.Current.MainWindow.Show();
            this.Close();
        }
        

    }
}
