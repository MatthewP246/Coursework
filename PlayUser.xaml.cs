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
    /// Interaction logic for PlayUser.xaml
    /// </summary>
    public partial class PlayUser : Window
    {
        private Game Connect4 = new Game();
        private string FirstPlayer;
        public PlayUser()
        {
            InitializeComponent();
            FirstPlayer = "1";
            Connect4.NewGame(FirstPlayer);
            DataContext = Connect4;


        }

        private void Column1_Click(object sender, RoutedEventArgs e)
        {

            Connect4.PlaceCounter(0);


        }

        private void Column2_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(1);
        }

        private void Column3_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(2);
        }

        private void Column4_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(3);
        }

        private void Column5_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(4);
        }

        private void Column6_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(5);
        }

        private void Column7_Click(object sender, RoutedEventArgs e)
        {
            Connect4.PlaceCounter(6);
        }
    }
}
