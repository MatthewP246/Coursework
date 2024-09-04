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


        public void PlaceCounter(int C, string v)
        {
            b.PlaceCounter(C,v);
        }



    }
}
