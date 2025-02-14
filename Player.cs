using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player
    {
        private string Colour { get; set; }
        

        
        public Player(string Colour)
        {
            this.Colour = Colour;
        }

        public string getColour
        {
            get { return Colour; }
        }




        public abstract void PlaceCounter(int C, Board b);

        


    }
}
