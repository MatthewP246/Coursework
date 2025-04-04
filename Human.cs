using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    internal class Human : Player
    {
        private string name;
        private double wins;
        private double losses;

        public Human(string Colour, string Name, double win, double loss) : base(Colour)
        {
            name = Name;
            wins = win;
            losses = loss;
        }
        //Get/Set methods for binding
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Wins
        {
            get { return wins; }
            set { wins = value; }
        }
        public double Losses
        {
            get { return losses; }
            set { wins = losses; }
        }

        public double WinLossRatio
        {
            get
            { //If either wins or losses is 0, return 0 to prevent division by 0 error
                if (wins == 0 || losses == 0) return 0;
                //Rounds to 3 decimal places
                else return Math.Round(wins / losses,3); 
            }
        }
        //Place Counter method for human players
        public override string PlaceCounter(int Column, Board Board, string Difficulty)
        {
            return Board.PlaceCounter(Column, false);
        }

         
        
    }
}
