using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Game
    {
        private Board board;
        private Player P1;
        private Player P2;

        public Game()
        {
            
        }

        public Board b
        {
            get { return board; }
        }

        public void NewGame()
        {
            board = new Board();
        }

        public void PlaceCounter(int C)
        {
            board.PlaceCounter(C);
        }
    }
}
