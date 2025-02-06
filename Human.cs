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
        public Human(string Colour, string Name) : base(Colour)
        {
            Colour = this.getColour;
            Name = name;
        }
        public override void PlaceCounter(int C, Board b)
        {
            b.PlaceCounter(C, false);
        }
        public string getName
        {
            get { return name; }
        }

    }
}
