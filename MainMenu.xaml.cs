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

namespace Connect4
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        private PlayerViewer Viewer;
        public MainMenu()
        {
            InitializeComponent();
            Viewer = new PlayerViewer();
            DataContext = Viewer;


            this.KeyDown += new KeyEventHandler(KeyPressed);
        }


        private void PlayUser(object sender, RoutedEventArgs e)
        {


            Window w = new GameSettings("User");
            this.Close();
            w.Show();




        }

        private void PlayComputer(object sender, RoutedEventArgs e)
        {

            Window w = new GameSettings("Computer");
            this.Close();
            w.Show();


        }

        private void Rules(object sender, RoutedEventArgs e)
        {
            Window w = new Rules();
            this.Close();
            w.Show();
        }


        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
