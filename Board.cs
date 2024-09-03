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
        private Counter Player;


        public Board()
        {
            Locations = new Counter[42];
            for (int x = 0; x < Locations.Length; x++)
            {
                Locations[x] = new Counter("0");
            }
            Player = new Counter("1");



        }
        public Counter[] l
        {
            get { return Locations; }
        }




        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }



        public void PlaceCounter(int C, string v)
        {
            int pos = C * 6;
            while (pos < C*6 + 6)
                if (Locations[pos].Number == "0")
                {
                    Locations[pos].Number = v;
                    break;
                }
                else
                {
                    pos++;
                }

        }
        public Counter u
        {
            get { return Player; }
        }

        public void UpdatePlayer()
        {
            if (Player.Number == "1") Player.Number = "2";
            else Player.Number = "1";
        }



        public bool CheckWin(int l)
        {
            bool win = false;





           return win;

        }


    }
}
