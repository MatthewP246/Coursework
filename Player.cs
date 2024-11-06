using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player
    {
        public string Colour;

        public abstract void PlaceCounter(int C, Board b);
        public abstract void NewPlayer(string Firstname, string Surname);

    }
}
