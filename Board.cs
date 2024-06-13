using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework_UI
{
    internal class Board 
    {
        private int[,] GameBoard;
        public event PropertyChangedEventHandler PropertyChanged;

        public Board()
        {

        }

        public void CreateBoard(int Width, int Height)
        {
            GameBoard = new int[7,6];
        }


        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }

        public int[,] b
        {
            get { return GameBoard; }
            set { GameBoard = value; }
        }


    }
}
