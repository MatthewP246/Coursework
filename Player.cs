using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player :Board
    {
        public string Colour;

        public abstract void PlaceCounter(int C, Board b);


    }
}
