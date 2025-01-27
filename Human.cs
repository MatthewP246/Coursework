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
        public Human(string Colour) : base(Colour)
        {
            Colour = this.getColour;
        }
        public override void PlaceCounter(int C, Board b)
        {
            b.PlaceCounter(C);
        }
        public string getName
        {
            get { return name; }
        }
        public void NewPlayer(string N)
        {
            name = N;
        }

    }
}
