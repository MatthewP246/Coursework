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

        public Counter P
        {
            get { return CurrentPlayer; }
        }

        public void PlaceCounter(int C)
        {
            bool win = false;

            if (RecursivePlace(C) == true)
            { 
                win = checkWin();

                UpdatePlayer();
                if (win == true)
                {
                    CurrentPlayer.Colour = "0";
                    
                }
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
            if (CurrentPlayer.Colour == "1") CurrentPlayer.Colour = "2";
            else CurrentPlayer.Colour = "1";
        }





        private bool checkWin()
        {

            bool win = false;
            ulong[] Rows = new ulong[6];
            ulong Bitboard = 0;

            //Creates a bitboard for each row in the grid
            for (int x = 0; x < 7; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    if (Grid[x+y*7].Colour == CurrentPlayer.Colour)
                    {
                        Rows[y] = Rows[y] + Convert.ToUInt64(Math.Pow(2, 6-x));
                    }
                }
            }



            for (int i = 0; i < 6; i++)
            {
                //Horizontal Check:
                ulong Horizontal = Rows[i] & (Rows[i] << 1) & (Rows[i] << 2) & (Rows[i] << 3);


                //Prevents index out of bounds errors
                ulong Vertical = 0;
                ulong Diagonal1 = 0;
                ulong Diagonal2 = 0;
                if (i < 3)
                {
                    //Vertical Check:

                    Vertical = Rows[i] & Rows[i + 1] & Rows[i + 2] & Rows[i + 3];

                    //Diagonal Check (bottom-left to top-right)
                    Diagonal1 = Rows[i] & (Rows[i + 1] << (i + 1)) & (Rows[i + 2] << (i + 2)) & (Rows[i + 3] << (i + 3));

                    //Diagonal Check (bottom-right to top-left)
                    Diagonal2 = Rows[i] & (Rows[i + 1] >> (i + 1)) & (Rows[i + 2] >> (i + 2)) & (Rows[i + 3] >> (i + 3));
                }
                // Return true if any of the checks are non-zero, indicating a win
                if(Horizontal != 0 || Vertical != 0 || Diagonal1 != 0 || Diagonal2 != 0)
                {
                    win = true;
                    break;
                }
            }

            

            return win;
        }


        private void something()
        {
            bool win;
            ulong Bitboard = 0;
            Console.WriteLine(Bitboard);


            // Horizontal check: Shift by 1, 2, and 3 bits
            ulong vertical = Bitboard & (Bitboard >> 1) & (Bitboard >> 2) & (Bitboard >> 3);

            // Vertical check: Shift by 7, 14, and 21 bits (7 bits per row
            ulong horizontal = Bitboard & (Bitboard >> 7) & (Bitboard >> 14) & (Bitboard >> 21);

            // Diagonal check (bottom-right to top-left): Shift by 6, 12, and 18 
            ulong diagonal1 = Bitboard & (Bitboard >> 6) & (Bitboard >> 12) & (Bitboard >> 18);

            // Diagonal check (bottom-left to top-right): Shift by 8 bits for the opposite diagonal
            ulong diagonal2 = Bitboard & (Bitboard >> 8) & (Bitboard >> 16) & (Bitboard >> 24);

            // Return true if any of the checks are non-zero, indicating a win
            win = (horizontal != 0 || vertical != 0 || diagonal1 != 0 || diagonal2 != 0);

        }
    }
}