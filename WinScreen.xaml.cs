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

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for WinScreen.xaml
    /// </summary>
    public partial class WinScreen : Window
    {
        public WinScreen()
        {
            InitializeComponent();

        }

        private async void CloseWindow(object sender, EventArgs e)
        {
            await Task.Delay(5000);
            Window w = new MainMenu();
            w.Show();
            this.Close();
        }
    }
}
