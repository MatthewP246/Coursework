using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Human : Player
    {
        
        public Human()
        {
        }

        public override void NewPlayer(string Name, string Colour)
        {

        }
        public override void PlaceCounter(int C)
        {
            //Placing counter in array
            //Pos is actual index into 1d array
            int pos = C * 6;
            RecursivePlace(C, pos);
        }

        private void RecursivePlace(int C, int pos)
        {
            //Recursive algorithm to place counter

            if (pos < C * 6 + 6)
            {
                if (l[pos].Colour == "0")
                {
                    l[pos].Colour = CurrentPlayer.Colour;
                    ll[C, pos % 6].Colour = CurrentPlayer.Colour;
                }
                else RecursivePlace(C, pos + 1);
            }


        }
    }
}
