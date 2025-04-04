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
    /// Interaction logic for PauseMenu.xaml
    /// </summary>


    /*
     * PAUSE MENU 
     * 
     * Gives the options to quit to the desktop or return to the main menu
     */
    public partial class PauseMenu : Window
    {
        public PauseMenu()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyPressed); //Event handler to recognise key presses
        }

        private void QuitToDesktop_Click(object sender, RoutedEventArgs e)
        {
            //Closes the application
            Application.Current.Shutdown();
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            Window w = new MainMenu();
            w.Show();
            Owner.Close();  
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
