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


        public Connect4()
        {
            b = new Board();
        }
        public Board board
        {
            get { return b; }
        }


        public void StartGame()
        {
            b.CreateBoard(7,6);
        }

        public void PlaceCounter(int C, int R, string v)
        {
            b.PlaceCounter(C,R,v);
        }
    }
}
