using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player
    {

        public string colour;
        
        public Player(string Colour)
        {
            colour = Colour;
        }

        

        public virtual string getColour
        {
            get { return colour; }
        }
        public abstract void PlaceCounter(int C, Board b);

        


    }
}
