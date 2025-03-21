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
        //Initialises Colour as needed for all players
        private string Colour;
        
         
        
        public Player(string Colour)
        {
            this.Colour = Colour;
        }

        //Protected to prevent it appearing in the Leaderboard
        protected string getColour
        {
            get { return Colour; }
        }



        //Creates abstract place counter method as all players can place counter
        public abstract string PlaceCounter(int C, Board b, string Difficulty);

        


    }
}
