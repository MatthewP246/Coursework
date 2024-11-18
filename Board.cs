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


        public Board(string Player)
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
            CurrentPlayer = new Counter(Player);

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
            RecursivePlace(C);
            win = checkWin();
            

            UpdatePlayer();
            if(win == true)
            {
                CurrentPlayer.Colour = "0"; 
            }

        }


        private void RecursivePlace(int C)
        {
            //Recursive algorithm to place counter
            //prevents counters spilling over into other columns
            if (C<42)
            {
                //Checks for lowest clear space in column
                if (g[C].Colour == "0")
                {
                    g[C].Colour = CurrentPlayer.Colour;
                    gg[C%7,C/7].Colour = CurrentPlayer.Colour;
                }
                else RecursivePlace(C+7);
            }
        }


        private bool ExtraDiagonalCheck()
        {
            bool win = false;
            for(int x = 0; x < 3; x++)
            {
                if (gg[6, x].Colour == CurrentPlayer.Colour && gg[6, x].Colour == gg[5, x + 1].Colour && gg[5, x + 1].Colour == gg[4, x + 2].Colour && gg[4, x + 2].Colour == gg[3, x + 3].Colour)
                {
                    win = true;
                    break;
                }
            }

            return win;
        }






        private void UpdatePlayer()
        {
            //Updating which colour is placing a counter
            if (CurrentPlayer.Colour == "1") CurrentPlayer.Colour = "2";
            else CurrentPlayer.Colour = "1";
        }



        private bool checkWin()
        {
            for (int a = 0; a < 7; a++)
            {
                for (int b = 0; b < 6; b++)
                {
                    Console.WriteLine(Grid2D[a, b].Colour);
                }
            }

            bool win = false;
            string[] ArrayBoard = new string[42];

            // Build ArrayBoard for player pieces based on the CurrentPlayer's color
            for (int a = 0; a < 42; a++)
            {
                ArrayBoard[a] = (Grid[a].Colour == CurrentPlayer.Colour) ? "1" : "0";
            }

            // Concatenate ArrayBoard into a single binary string representing the board
            string StringBoard = string.Join("", ArrayBoard);

            // Convert the binary string into a 64-bit unsigned integer
            ulong Bitboard = Convert.ToUInt64(StringBoard.PadLeft(64, '0'), 2);

            // Horizontal check: Iterate over each row and apply bitwise shifts within each row's 7-bit section
            for (int row = 0; row < 6; row++) // 6 rows in Connect 4
            {
                int rowStart = row * 7;
                ulong rowBits = (Bitboard >> rowStart) & 0x7F; // Extract 7 bits for each row
                if ((rowBits & (rowBits >> 1) & (rowBits >> 2) & (rowBits >> 3)) != 0)
                {
                    win = true;
                    break;
                }
            }

            // Vertical check: Shift by 7 bits for alignment across rows
            ulong vertical = Bitboard & (Bitboard >> 7) & (Bitboard >> 14) & (Bitboard >> 21);
            if (vertical != 0) win = true;






            // Diagonal check (top-left to bottom-right): Shift by 6 bits for diagonal alignment

            //The check win doesnt work when a counter is placed in the first column as 6 binary shifts doesnt go over into the next row as designed
            ulong diagonal1 = Bitboard & (Bitboard >> 6) & (Bitboard >> 12) & (Bitboard >> 18);
            if (diagonal1 != 0 && ExtraDiagonalCheck() == true) win = true;




            // Diagonal check (bottom-left to top-right): Shift by 8 bits for the opposite diagonal
            ulong diagonal2 = Bitboard & (Bitboard >> 8) & (Bitboard >> 16) & (Bitboard >> 24);
            if (diagonal2 != 0 ) win = true;

            return win;
        }
    }
}
