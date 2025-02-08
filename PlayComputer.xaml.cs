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
        public PlayComputer(string colour, string P1, string P2)
        {
            InitializeComponent();
            Colour = colour;
            P1Name = P1;
            P2Name = P2;

            Connect4 = new Game(Colour, true, P1Name, P2Name);
            DataContext = Connect4;
            this.KeyDown += new KeyEventHandler(KeyPressed);

        }

        private async void Column1_Click(object sender, RoutedEventArgs e)
        {

            PlaceCounter(0);
            await Task.Delay(500);
            //-1 indicates computer makes move
            PlaceCounter(-1);


        }

        private async void Column2_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(1);
            await Task.Delay(500);
            PlaceCounter(-1);
        }

        private async void Column3_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(2);
            await Task.Delay(500);
            PlaceCounter(-1);
        }

        private async void Column4_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(3);
            await Task.Delay(500);
            PlaceCounter(-1);
        }

        private async void Column5_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(4);
            await Task.Delay(500);
            PlaceCounter(-1);
        }

        private async void Column6_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(5);
            await Task.Delay(500);
            PlaceCounter(-1);
        }

        private async void Column7_Click(object sender, RoutedEventArgs e)
        {
            PlaceCounter(6);
            await Task.Delay(500);
            PlaceCounter(-1);
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
            Window w = new PlayComputer(Colour, P1Name, P2Name);
            w.Show();
            this.Close();



        }

        private void PlaceCounter(int C)
        {
            Connect4.PlaceCounter(C);
        }

        protected void OnExit(ExitEventArgs e)
        {
            this.OnExit(e);
            Application.Current.Shutdown();
        }
    }
}
