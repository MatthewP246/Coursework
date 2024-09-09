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

        private Counter CurrentPlayer;


        public Board()
        {
            Grid = new Counter[42];

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
            bool win = false;
            while (pos < C*6 + 6)
                if (Grid[pos].Colour == "0")
                {
                    Grid[pos].Colour = v;
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
            int pos = x * 6;

            //Vertical Check

            while(pos<x*6+6)
            {
                if(Grid[pos].Colour != "0")
                {
                    if (Grid[pos].Colour == Player) count++;
                    else
                    {
                        count = 0;
                    }  
                }
                pos++;

            }
            if(count >= 4)
            {
                win = true;
            }

            //Horizontal Check

            if (win == false)
            {
                count = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = i; j < 42; j = j + 6)
                    {
                        if (Grid[j].Colour != "0")
                        {
                            if (Grid[j].Colour == Player) count++;
                            else
                            {
                                count = 0;
                            }

                        }
                        if (count >= 4)
                        {
                            win = true;
                            break;
                        }
                    }
                }
            }
            
            
            
            
            return win;
        }


    }
}
