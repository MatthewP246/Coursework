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
        public Human()
        {

        }
        public override void PlaceCounter(int C, Board b)
        {
            
        }
        public override void NewPlayer(string F, string S)
        {
            Firstname = F;
            Surname = S;
        }

    }
}
