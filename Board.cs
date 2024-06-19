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
        private Counter[] Locations;

        public Board()
        {
            Locations = new Counter[42];
        }
        public Counter[] l
        {
            get { return Locations; }
        }

        //public void CreateBoard(int Width, int Height)



        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }



        public void PlaceCounter(int C, string v)
        {
            int pos = C * 7;
            while (pos < C * 7 + 7)
                if (Locations[pos] == null)
                {
                    Locations[pos] = new Counter(v);
                    break;
                }
                else
                {
                    pos++;
                }

        }




    }
}
