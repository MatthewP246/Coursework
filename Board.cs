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
            Locations[0] = new Counter("0");
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
            for (int pos = C * 7; pos < C *7+7; pos++)
            {
                if (Locations[pos] == null)
                {
                    Locations[pos] = new Counter(v);
                }
                else Locations[pos].Number = v;
            }
        }


    }
}
