﻿using System;
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
    /// Interaction logic for Rules.xaml
    /// </summary>
    /// 
    /*
     * RULES
     * Allows new players to learn the rules of the game
     */
    public partial class Rules : Window
    {
        public Rules()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyPressed); //Event handler to recognise key presses
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }    }
}
