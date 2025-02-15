﻿using System;
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


namespace Coursework_UI
{

    public partial class GameSettings : Window 
    {
        private string Mode;
        private string FirstPlayer;
        private string P1Name;
        private string P2Name;

        private PlayerViewer viewer;


        public GameSettings(string Gamemode)
        {
            InitializeComponent();
            Mode = Gamemode;
            viewer = new PlayerViewer();
            DataContext = viewer;
            if (Mode == "Computer")
            {
                Column2.Visibility = Visibility.Collapsed;
                Colour.Text = "Select Colour";
            }
            else
            {
                StackDifficulty.Visibility = Visibility.Collapsed;
            }
                this.KeyDown += new KeyEventHandler(KeyPressed);

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            P1Name = Player1Name.Text;
            P2Name = Player2Name.Text;
            if (Colour.Text == "Red") FirstPlayer = "R";
            else FirstPlayer = "Y";
            if (Mode == "User")
            {
                
                Window w = new PlayUser(FirstPlayer, P1Name, P2Name);
                w.Show();
                this.Close();
            }
            else
            {
                Window w = new PlayComputer(FirstPlayer, P1Name, P2Name);
                w.Show();
                this.Close();
            }
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            string name = NewUser.Text.Trim();
            if(string.IsNullOrWhiteSpace(name) || name=="Enter Username")
            {
                MessageBox.Show("Please enter a valid name!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            viewer.AddPlayer(name);
            NewUser.Clear();
            NewUser.Text = "Enter Username";
            
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }

        protected void OnExit(ExitEventArgs e)
        {
            this.OnExit(e);
            Application.Current.Shutdown();
        }
    }
}
