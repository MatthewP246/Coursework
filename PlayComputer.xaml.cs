using System;
using System.Collections.Generic;
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

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for PlayComputer.xaml
    /// </summary>
    public partial class PlayComputer : Window
    {

        private Game Connect4;
        private string Colour;
        public PlayComputer(string colour, bool AIGame)
        {
            InitializeComponent();
            Colour = colour;
            Connect4 = new Game(Colour, AIGame);
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
                Close.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D1)
            {
                Column1.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D2)
            {
                Column2.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D3)
            {
                Column3.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D4)
            {
                Column4.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D5)
            {
                Column5.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D6)
            {
                Column6.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            if (e.Key == Key.D7)
            {
                Column7.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
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
            Window w = new PlayComputer(Colour, true);
            w.Show();
            this.Close();



        }

        private void PlaceCounter(int C)
        {
            Connect4.PlaceCounter(C);
        }
    }
}
