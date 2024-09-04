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
        private Counter[,] Grid2D;
        private Counter CurrentPlayer;


        public Board()
        {
            Grid = new Counter[42];
            Grid2D = new Counter[7, 6];
            for (int x = 0; x < Grid.Length; x++)
            {
                Grid[x] = new Counter("0");
            }
            CurrentPlayer = new Counter("1");



        }
        public Counter[] l
        {
            get { return Grid; }
        }



        public void PlaceCounter(int C, string v)
        {
            int pos = C * 6;
            int y = 0;
            bool win = false;
            while (pos < C*6 + 6)
                if (Grid[pos].Colour == "0")
                {
                    Grid[pos].Colour = v;
                    Grid2D[C, y] = new Counter(v);
                    win = checkWin(C, y);
                    break;
                }
                else
                {
                    pos++;
                    y++;
                }
            
            UpdatePlayer();

        }
        public Counter u
        {
            get { return CurrentPlayer; }
        }

        private void UpdatePlayer()
        {
            if (CurrentPlayer.Colour == "1") CurrentPlayer.Colour = "2";
            else CurrentPlayer.Colour = "1";
        }



        private bool checkWin(int x, int y, string Player)
        {
            bool win = false;
            int count = 0;

            //Vertical Check

            for(int j = 0; j < 7; j++)
            {
                if (Grid2D[x, j] == player)
            }

            return win;
        }


    }
}
