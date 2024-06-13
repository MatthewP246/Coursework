using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Connect4
    {
        private Board b;
        private Player P1;
        private Player P2;

        public Connect4()
        {
            b = new Board();
        }
        public Board board
        {
            get { return b; }
            set { b = value; }
        }


        public void StartGame()
        {
            b.CreateBoard(7,6);
        }
    }
}
