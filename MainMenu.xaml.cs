using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect4
{

    public partial class MainMenu : Window
    {
        private PlayerViewer Viewer;
        
        public MainMenu()
        {
            Viewer = new PlayerViewer();
            DataContext = Viewer;


            this.KeyDown += new KeyEventHandler(KeyPressed);
            
        }

        private void PlayUser(object sender, RoutedEventArgs e)
        {
			Window w = new GameSettings("User");
			this.Hide();
			w.ShowDialog();




		}

        private void PlayComputer(object sender, RoutedEventArgs e)
        {

			Window w = new GameSettings("Computer");
			this.Hide();
			w.ShowDialog();


		}

        private void Rules(object sender, RoutedEventArgs e)
        {
            Window w = new Rules();
            this.Hide();
			w.ShowDialog();
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

