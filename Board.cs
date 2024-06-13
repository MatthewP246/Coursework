using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Coursework_UI
{
    internal class Board 
    {
        private int[,] Locations;
        public event PropertyChangedEventHandler PropertyChanged;

        public Board()
        {

        }

        public void CreateBoard(int Width, int Height)
        {
            Locations = new int[7,6];
        }


        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }

        public int[,] location
        {
            get { return Locations; }
            set { Locations = value; OnPropertyChanged("b"); }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }


        }

        public void PlaceCounter(int Column)
        {
            location[0, 0] = 1;
        }


    }
}
