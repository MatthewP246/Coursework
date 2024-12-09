using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Human : Player
    {
        private string Firstname;
        private string Surname;
        public Human()
        {

        }
        public override void PlaceCounter(int C, Board b)
        {
            b.PlaceCounter(C);
        }
        public void NewPlayer(string F, string S)
        {
            Firstname = F;
            Surname = S;
        }

    }
}
