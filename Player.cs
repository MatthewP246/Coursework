using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{

    abstract class Player
    {
        [Bindable(false)]
        private string Colour;
        
         
        
        public Player(string Colour)
        {
            this.Colour = Colour;
        }

        
        protected string getColour
        {
            get { return Colour; }
        }




        public abstract void PlaceCounter(int C, Board b);

        


    }
}
