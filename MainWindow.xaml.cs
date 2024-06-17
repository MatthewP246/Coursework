using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Connect4 Game;

        public MainWindow()
        {
            InitializeComponent(); 
            Game = new Connect4();
            DataContext = Game;
        }

        private void Column1_Click(object sender, RoutedEventArgs e)
        {
            Game.PlaceCounter(1,1,"0");
        }
    }
}
