using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player : Board
    {
        public string Name;
        public string Colour;


        public abstract void NewPlayer(string Name, string Colour);
        public abstract void PlaceCounter(int C);

    }
}
