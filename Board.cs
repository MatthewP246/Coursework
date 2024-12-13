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

        public Counter CurrentPlayer;
        private Human h;


        public Board(string FirstPlayer)
        {
            //initialising the grid as a blank array of 0's
            Grid = new Counter[42];
            Grid2D = new Counter[7, 6];


            for (int x = 0; x < Grid.Length; x++)
            {
                Grid[x] = new Counter("0");
            }


            for (int a = 0; a < 7; a++)
            {
                for (int b = 0; b < 6; b++)
                {
                    Grid2D[a, b] = new Counter("0");
                }
            }
            CurrentPlayer = new Counter(FirstPlayer);
        }
        public Counter[] g
        {
            //get method for data binding
            get { return Grid; }
        }



        public Counter[,] gg
        {
            //get method for 2D array
            get { return Grid2D; }
        }

        public Counter p
        {
            get { return CurrentPlayer; }
        }

        public void PlaceCounter(int C)
        {
            bool win = false;

            if (RecursivePlace(C) == true)
            { 
                win = checkWin();
                if (win == true)
                {
                    CurrentPlayer.Colour = "0";
                    
                } 
                else UpdatePlayer();
            }

        }


        private bool RecursivePlace(int C)
        {
            //Recursive algorithm to place counter
            bool Placed = true;
            if (C<42)
            {

                if (g[C].Colour == "0")
                {
                    g[C].Colour = CurrentPlayer.Colour;
                    gg[C%7,C/7].Colour = CurrentPlayer.Colour;
                }
                else 
                {
                    Placed = RecursivePlace(C+7);

                }
                
            }
            else Placed = false;

            return Placed;
        }



        private void UpdatePlayer()
        {
            //Updating which colour is placing a counter
            if (CurrentPlayer.Colour == "R") CurrentPlayer.Colour = "Y";
            else CurrentPlayer.Colour = "R";
        }


        public bool checkWin()
        {

            bool win = false;
            ulong[] Rows = new ulong[6];

            //Creates a bitboard for each row in the grid
            for (int x = 0; x < 7; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    if (Grid[x+y*7].Colour == CurrentPlayer.Colour)
                    {
                        Rows[y] = Rows[y] + Convert.ToUInt64(Math.Pow(2, x));
                    }
                }
            }
            
            //Intitialise all variables
            ulong Horizontal = 0;
            ulong Vertical = 0;
            ulong Diagonal1 = 0;
            ulong Diagonal2 = 0;


            for (int i = 0; i < 6; i++)
            {
                //Horizontal Check:
                Horizontal = Rows[i] & (Rows[i] << 1) & (Rows[i] << 2) & (Rows[i] << 3);


                //Prevents index out of bounds errors
                if (i < 3)
                {
                    //Vertical Check:

                    Vertical = Rows[i] & Rows[i + 1] & Rows[i + 2] & Rows[i + 3];

                    //Diagonal Check (bottom-left to top-right)
                    Diagonal1 = Rows[i] & (Rows[i + 1] << 1) & (Rows[i + 2] << 2) & (Rows[i + 3] << 3);

                    //Diagonal Check (bottom-right to top-left)
                    Diagonal2 = (Rows[i] << 3) & (Rows[i+1] << 2) & (Rows[i+2] << 1) & Rows[i+3];
                }
                // Return true if any of the checks are non-zero, indicating a win
                if(Horizontal != 0)
                {
                    win = true;
                    FindWinLocation("H");
                    
                    //exits out of loop as soon as win is found
                    break;
                }
                if(Vertical != 0)
                {
                    win = true;
                    FindWinLocation("V");
                    break;
                }
                if (Diagonal1 != 0)
                {
                    win = true;
                    FindWinLocation("D1");
                    break;
                }
                if (Diagonal2 != 0)
                {
                    win = true;
                    FindWinLocation("D2");
                    break;
                }
            }

            

            return win;
        }

        private void FindWinLocation(string Method)
        {
            string Colour = CurrentPlayer.Colour + Method;

            //Horizontal Check
            if (Method == "H")
            {
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x+y*7].Colour = Colour;
                            Grid[(x+1) + y * 7].Colour = Colour;
                            Grid[(x + 2) + y * 7].Colour = Colour;
                            Grid[(x + 3) + y * 7].Colour = Colour;
                        }
                    }
                }
            }

            if (Method == "V")
            {
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x, y + 1].Colour == CurrentPlayer.Colour && Grid2D[x, y + 2].Colour == CurrentPlayer.Colour && Grid2D[x, y + 3].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x + y * 7].Colour = Colour;
                            Grid[x + (y+1) * 7].Colour = Colour;
                            Grid[x + (y + 2) * 7].Colour = Colour;
                            Grid[x + (y + 3) * 7].Colour = Colour;
                        }
                    }
                }
            }

            if (Method == "D1")
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 3; y < 6; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y - 1].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y - 2].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y - 3].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x + y * 7].Colour = Colour;
                            Grid[(x + 1) + (y-1) * 7].Colour = Colour;
                            Grid[(x + 2) + (y-2) * 7].Colour = Colour;
                            Grid[(x + 3) + (y-3) * 7].Colour = Colour;
                        }
                    }
                }
            }

            if (Method == "D2")
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y + 1].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y + 2].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y + 3].Colour == CurrentPlayer.Colour)
                        {
                            
                            Grid[x + y * 7].Colour = Colour;
                            Grid[(x + 1) + (y + 1) * 7].Colour = Colour;
                            Grid[(x + 2) + (y + 2) * 7].Colour = Colour;
                            Grid[(x + 3) + (y + 3) * 7].Colour = Colour;
                        }
                    }
                }
            }
        }
    }
}