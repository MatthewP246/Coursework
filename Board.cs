using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework_UI
{
    internal class Board 
    {
        private int[,] GameBoard;

        public Board()
        {

        }

        public void CreateBoard(int Width, int Height)
        {
            GameBoard = new int[Width, Height];
        }
        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }
    }
}
