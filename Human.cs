using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Human : Player
    {
        private string name;
        private int wins;
        private int losses;

        public Human(string Colour, string Name, int win, int loss) : base(Colour)
        {
            name = Name;
            wins = win;
            losses = loss;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }
        public int Losses
        {
            get { return losses; }
            set { wins = losses; }
        }

        public int WinLossRatio
        {
            get 
            { if (wins == 0 || losses == 0) return 0;
                else return wins / losses; 
            }
        }

        public override string PlaceCounter(int C, Board b)
        {
            return b.PlaceCounter(C, false);
        }

         
        
    }
}
