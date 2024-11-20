﻿using System;
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
            int pos = C * 6;


            RecursivePlace(C, pos);
            win = checkWin();

            UpdatePlayer();
            if (win == true)
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

            // Diagonal check (bottom-left to top-right): Shift by 6 bits for diagonal alignment
            ulong diagonal1 = Bitboard & (Bitboard >> 6) & (Bitboard >> 12) & (Bitboard >> 18);
            if (diagonal1 != 0) win = true;

            // Diagonal check (top-left to bottom-right): Shift by 8 bits for the opposite diagonal
            ulong diagonal2 = Bitboard & (Bitboard >> 8) & (Bitboard >> 16) & (Bitboard >> 24);
            if (diagonal2 != 0) win = true;


            return win;
        }
    }
}