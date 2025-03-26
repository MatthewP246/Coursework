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

namespace Coursework_UI
{

    public partial class MainMenu : Window
    {
        private PlayerViewer Viewer;
        private bool SavedGame;
        
        public MainMenu()
        {
            Viewer = new PlayerViewer();
            DataContext = Viewer;

            SavedGame = false;

            this.KeyDown += new KeyEventHandler(KeyPressed);
            
        }

        private void PlayUser(object sender, RoutedEventArgs e)
        {
            if (SavedGame)
            {
                var Result = MessageBox.Show("Would you like to continue the previous game?", "Load Game", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {

                }
                else if (Result == MessageBoxResult.No)
                {
                    Window w = new GameSettings("User");
                    this.Hide();
                    w.ShowDialog();
                }

            }
            else
            {
                Window w = new GameSettings("User");
                this.Hide();
                w.ShowDialog();
            }




        }

        private void PlayComputer(object sender, RoutedEventArgs e)
        {

            if (SavedGame)
            {
                var Result = MessageBox.Show("Would you like to continue the previous game?", "Load game", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {

                }
                else if (Result == MessageBoxResult.No)
                {
                    Window w = new GameSettings("Computer");
                    this.Hide();
                    w.ShowDialog();
                }
                else ;

            }
            else
            {
                Window w = new GameSettings("Computer");
                this.Hide();
                w.ShowDialog();
            }


        }

        private void Rules(object sender, RoutedEventArgs e)
        {

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

