using System;
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

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for PlayComputer.xaml
    /// </summary>
    public partial class PlayComputer : Window
    {

        private Game Connect4;
        private string Colour;
        private string P1Name;
        private string P2Name;
        private string Difficulty;
        public PlayComputer(string colour, string P1, string P2, string Diff)
        {
            InitializeComponent();
            Colour = colour;
            P1Name = P1;
            P2Name = P2;
            Difficulty = Diff;

            Connect4 = new Game(Colour, true, P1Name, P2Name, Difficulty);
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
                this.Cross_Click(sender, e);
            }

            if (e.Key == Key.D1)
            {
                this.Column1_Click(sender, e);
            }

            if (e.Key == Key.D2)
            {
                this.Column2_Click(sender, e);
            }

            if (e.Key == Key.D3)
            {
                this.Column3_Click(sender, e);
            }

            if (e.Key == Key.D4)
            {
                this.Column4_Click(sender, e);
            }

            if (e.Key == Key.D5)
            {
                this.Column5_Click(sender, e);
            }

            if (e.Key == Key.D6)
            {
                this.Column6_Click(sender, e);
            }

            if (e.Key == Key.D7)
            {
                this.Column7_Click(sender, e);
            }
        }

        private void Cross_Click(object sender, RoutedEventArgs e)
        {
            Window w = new PauseMenu();
            w.Owner = this;
            w.ShowDialog();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Window w = new PlayComputer(Colour, P1Name, P2Name, Difficulty);
            w.Show();
            this.Close();



        }

        private async void PlaceCounter(int C)
        {
            Connect4.PlaceCounter(C);
            if (Connect4.CheckWin() == true)
            {
                Window w = new WinScreen(P1Name);
                w.ShowDialog();
                this.Close();
            }
            else
            {
                await Task.Delay(500);
                PlaceCounter(-1);
                if (Connect4.CheckWin() == true)
                {
                    Window w = new WinScreen("Computer");
                    w.ShowDialog();
                    this.Close();
                }
            }
            
        }

    }
}
