using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private int Heuristic(Board b, Counter Current)
        {
            int HValue = 0;

            //Horizontal score
            for (int r = 0; r < 6; r++)
            {
                //creates an array of each individual row
                string[] rowArray = { b.gg[0, r].Colour, b.gg[1, r].Colour, b.gg[2, r].Colour, b.gg[3, r].Colour, b.gg[4, r].Colour, b.gg[5, r].Colour};
                for(int c = 0; c < 4; c++)
                {
                    //4 to prevent index out of bounds error
                    //creates a window of 4 spaces horizontally which could be used to win
                    string[] Window = { rowArray[c], rowArray[c + 1], rowArray[c + 2], rowArray[c + 3], rowArray[c + 4]};
                    //4 in the window
                    if ((Window.Count(s => s == Current.Colour) == 4))
                    {
                        HValue += 100;
                    }
                    //3 in the window
                    else if ((Window.Count(s => s == Current.Colour) == 3) && (Window.Count(s => s == "0")) == 1)
                    {
                        HValue += 10;
                    }

                }
            }
            

            






            return HValue;
        }
    }
}
