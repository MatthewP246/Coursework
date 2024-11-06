using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Human : Player
    {
        string Firstname;
        string Surname;
        public Human(String F, String S)
        {
            Firstname = F;
            Surname = S;
        }
        public override void PlaceCounter(int C, Board b)
        {
            
        }
        public void NewPlayer(string Name, string Colour)
        {

        }

    }
}
