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

            for(int a = 0; a < 7; a++)
            {
                for (int b = 0; b < 6; b++)
                {
                    Grid2D[a, b] = new Counter("0");
                }
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
            bool win = false;
            while (pos < C*6 + 6)
                if (Grid[pos].Colour == "0")
                {
                    Grid[pos].Colour = v;
                    Grid2D[C, pos % 6].Colour = v;
                    win = checkWin(C, v);
                    break;
                }
                else
                {
                    pos++;
                }
            
            UpdatePlayer();
            if(win == true)
            {
                CurrentPlayer.Colour = "0";
            }

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



        private bool checkWin(int x, string Player)
        {
            bool win = false;
            int count = 0;

            //Vertical Check
            int y = 0;
            while (y < 6)
            {
                if (Grid2D[x,y] != null)
                {
                    if (Grid2D[x, y].Colour == Player) count++;
                    else
                    {
                        count = 0;
                    }
                    if (count >= 4)
                    {
                        win = true;
                        break;
                    }
                    y++;
                }
            }

            //Horizontal Check

            if (win == false)
            {
                count = 0;
                y = 0;
                x = 0;
                while (y < 6)
                {
                    while (x < 7)
                    {
                        if (Grid2D[x, y] != null)
                        {
                            if (Grid2D[x, y].Colour == Player) count++;
                            else
                            {
                                count = 0;
                            }
                            if (count >= 4)
                            {
                                win = true;
                                break;
                            }
                            x++;
                        }
                        
                    }
                    if (win == true) break;
                    else y++;

                }
                
            }




            return win;
        }


    }
}
