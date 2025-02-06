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


namespace Coursework_UI
{

    public partial class GameSettings : Window 
    {
        private string Mode;
        private string FirstPlayer;
        private string P1Name;
        private string P2Name;

        public IObservable<string> ComputerGame;


        public GameSettings(string Gamemode)
        {
            InitializeComponent();
            Mode = Gamemode;
            if (Mode == "Conmputer")
            {
                Player2Name.Visibility = Visibility.Hidden;
            }
                this.KeyDown += new KeyEventHandler(KeyPressed);

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == "User")
            {
                Window w = new PlayUser(FirstPlayer);
            }
            else
            {
                Window w = new PlayComputer(FirstPlayer);
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }
    }
}
