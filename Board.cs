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


        public Board()
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
            CurrentPlayer = new Counter("1");



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
            int pos = C * 6;
            RecursivePlace(C, pos);
            win = checkWin();
            
            UpdatePlayer();
            if(win == true)
            {
                CurrentPlayer.Colour = "0";
            }

        }


    private void RecursivePlace(int C, int pos)
    {
        //Recursive algorithm to place counter

        if (pos < C * 6 + 6)
        {
            if (g[pos].Colour == "0")
            {
                g[pos].Colour = CurrentPlayer.Colour;
                gg[C, pos % 6].Colour = CurrentPlayer.Colour;
            }
            else RecursivePlace(C, pos + 1);
        }


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
            string[] ArrayBoard = { "0d","0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", };
            string StringBoard = "";



            for (int a = 1; a < 43; a++)
            {
                if (Grid[a-1].Colour == CurrentPlayer.Colour)
                {
                    ArrayBoard[42 - a] = "1";
                }
                StringBoard = StringBoard + ArrayBoard[a];
                Console.WriteLine(Grid[a-1].Colour);
            }

            //Converting the string into binary
            ulong Bitboard = Convert.ToUInt64(StringBoard);

            // Function to check if a player has won (either horizontally, vertically, or diagonally

            // Horizontal check: Shift by 1, 2, and 3 bits
            ulong horizontal = Bitboard & (Bitboard >> 1) & (Bitboard >> 2) & (Bitboard >> 3);

            // Vertical check: Shift by 7, 14, and 21 bits (7 bits per row
            ulong vertical = Bitboard & (Bitboard >> 7) & (Bitboard >> 14) & (Bitboard >> 21);

            // Diagonal check (bottom-left to top-right): Shift by 6, 12, and 18 
            ulong diagonal1 = Bitboard & (Bitboard >> 6) & (Bitboard >> 12) & (Bitboard >> 18);

            // Diagonal check (top-left to bottom-right): Shift by 8, 16, and 24 
            ulong diagonal2 = Bitboard & (Bitboard >> 8) & (Bitboard >> 16) & (Bitboard >> 24);

            // Return true if any of the checks are non-zero, indicating a win
            win = (horizontal != 0 || vertical != 0 || diagonal1 != 0 || diagonal2 != 0);

            return win;
        }
    }
}
