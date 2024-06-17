using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xaml.Schema;

namespace Coursework_UI
{
    internal class Board 
    {
        private Counter[,] Locations = new Counter[7, 6];

        public Board()
        {
            Locations[0, 0] = new Counter("0");
        }
        public Counter[,] locations
        {
            get { return Locations; }
        }

        public void CreateBoard(int Width, int Height)
        {

        }


        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }



        public void PlaceCounter(int C, int R, string v)
        {
            if (Locations[C, R].Number == null)
            {
                Locations[C, R] = new Counter(v);
            }
            else Locations[C, R].Number = v;
        }


    }
}
