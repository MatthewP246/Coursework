using System;
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
    /// Interaction logic for Grid.xaml
    /// </summary>
    public partial class Grid : Window
    {
        private Connect4 Game;
        private int PlayerCount = 2;
        private string Player;

        public Grid()
        {
            InitializeComponent();
            Game = new Connect4();
            DataContext = Game;

            if (PlayerCount % 2 == 0) ;
            else
            {
                Game.PlaceCounter(7);
            }

        }

        private void Column1_Click(object sender, RoutedEventArgs e)
        {

            Game.PlaceCounter(0);


        }

        private void Column2_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(1);
        }

        private void Column3_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(2);
        }

        private void Column4_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(3);
        }

        private void Column5_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(4);
        }

        private void Column6_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(5);
        }

        private void Column7_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(6);
        }
    }
}
