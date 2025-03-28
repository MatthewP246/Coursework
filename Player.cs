using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    //Abstract Class Player
    //Contains common attributes for all players e.g. Colour and Place Counter
    abstract class Player
    {
        //Initialises Colour as needed for all players
        private string Colour;
        private int ID;
        
         
        
        public Player(string Colour)
        {
            this.Colour = Colour;
        }

        //Protected to prevent it appearing in the Leaderboard
        protected string GetColour
        {
            get { return Colour; }
        }



        //Creates abstract place counter method as all players can place counter
        public abstract string PlaceCounter(int Column, Board Board, string Difficulty);

        


    }
}
