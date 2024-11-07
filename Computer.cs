using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Computer : Player
    {
        
        
       
        public override void PlaceCounter(int C, Board b)
        {
            
        }

        private int Heuristic()
        {
            int HValue = 0;
            ulong Bitboard = 0;

            for (int a = 0; a < 42; a++)
            {
                if (g[a].Colour == p.Colour)
                {
                    Bitboard = Bitboard + Convert.ToUInt64(Math.Pow(2, a));
                }

            }

            






            return HValue;
        }
    }
}
