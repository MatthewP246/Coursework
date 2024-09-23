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
            //initialising the grid as a blank array of 0's
            Grid = new Counter[42];

            for (int x = 0; x < Grid.Length; x++)
            {
                Grid[x] = new Counter("0");
            }
            CurrentPlayer = new Counter("1");



        }
        public Counter[] l
        {
            //get method for data binding
            get { return Grid; }
        }



        public void PlaceCounter(int C)
        {
            //Placing counter in array
            //Pos is actual index into 1d array
            int pos = C * 6;
            bool win = false;
            RecursivePlace(C, pos, CurrentPlayer.Colour);
            win = checkWin(C);

            UpdatePlayer();
            if(win == true)
            {
                CurrentPlayer.Colour = "0";
            }

        }

        private void RecursivePlace(int C, int pos, string v)
        {
            //Recursive algorithm to place counter
            
            if (pos < C * 6 + 6)
            {
                if (Grid[pos].Colour == "0")
                {
                    Grid[pos].Colour = v;
                }
                else RecursivePlace(C, pos + 1, v);
            }
            
            
        }


        public Counter u
        {
            get { return CurrentPlayer; }
        }

        private void UpdatePlayer()
        {
            //Updating which colour is placing a counter
            if (CurrentPlayer.Colour == "1") CurrentPlayer.Colour = "2";
            else CurrentPlayer.Colour = "1";
        }



        private bool checkWin(int x)
        {
            bool win = false;
            int count = 0;
            int pos = x * 6;

            //Vertical Check

            while(pos<x*6+6)
            {
                if(Grid[pos].Colour != "0")
                {
                    if (Grid[pos].Colour == CurrentPlayer.Colour) count++;
                    else
                    {
                        count = 0;
                    }  
                }
                pos++;
                if (count >= 4)
                {
                    win = true;
                    break;
                }

            }
            //Horizontal Check
            if (win == false)
            {
                count = 0;
                for (int a = 0; a < 8; a++)
                {
                    for (int b = a; b < 42; b = b + 6)
                    {
                        if (Grid[b].Colour != "0")
                        {
                            if (Grid[b].Colour == CurrentPlayer.Colour) count++;
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
