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
        private Counter[] Grid;
        private Counter Player;


        public Board()
        {
            Grid = new Counter[42];
            for (int x = 0; x < Grid.Length; x++)
            {
                Grid[x] = new Counter("0");
            }
            Player = new Counter("1");



        }
        public Counter[] l
        {
            get { return Grid; }
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
                if (Grid[pos].Colour == "0")
                {
                    Grid[pos].Colour = v;
                    break;
                }
                else
                {
                    pos++;
                }
            UpdatePlayer();

        }
        public Counter u
        {
            get { return Player; }
        }

        private void UpdatePlayer()
        {
            if (Player.Colour == "1") Player.Colour = "2";
            else Player.Colour = "1";
        }



        public bool CheckWin(int l)
        {
            bool win = false;





           return win;

        }


    }
}
