using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player
    {
        private string name;
        private string colour;
        public Player(string Name, string Colour)
        {
            name = Name;
            colour = Colour;
        }

        public string getName
        {
            get { return name; }
        }

        public string getColour
        {
            get { return colour; }
        }
        public abstract void PlaceCounter(int C, Board b);


    }
}
