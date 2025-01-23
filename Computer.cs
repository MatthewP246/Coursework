using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Computer : Player
    {
        private Random Rgen = new Random();
        
       
        public override void PlaceCounter(int C, Board b)
        {
            C = BestMove(b);
            b.PlaceCounter(C);
        }

        private int Heuristic(Board b)
        {
            int HValue = 0;

            //Horizontal score
            for (int row = 0; row < 6; row++)
            {
                //creates an array of each individual row
                string[] rowArray = { b.gg[0, row].Colour, b.gg[1, row].Colour, b.gg[2, row].Colour, b.gg[3, row].Colour, b.gg[4, row].Colour, b.gg[5, row].Colour};
                for(int col = 0; col < 4; col++)
                {
                    //4 to prevent index out of bounds error
                    //creates a window of 4 spaces horizontally which could be used to win
                    string[] Window = { rowArray[col], rowArray[col + 1], rowArray[col + 2], rowArray[col + 3], rowArray[col + 4]};
                    //4 in the window
                    if ((Window.Count(s => s == b.CurrentPlayer.Colour) == 4))
                    {
                        HValue += 100;
                    }
                    //3 in the window
                    else if ((Window.Count(s => s == b.CurrentPlayer.Colour) == 3) && (Window.Count(s => s == "0")) == 1)
                    {
                        HValue += 10;
                    }

                }
            }
            //Vertical Score
            for (int col = 0; col < 7; col++)
            {
                //creates an array of each individual column
                string[] colArray = { b.gg[col, 0].Colour, b.gg[col, 1].Colour, b.gg[col, 2].Colour, b.gg[col, 3].Colour, b.gg[col, 4].Colour, b.gg[col, 5].Colour , b.gg[col,6].Colour };
                for (int row = 0; row < 3; col++)
                {
                    //3 to prevent index out of bounds error
                    //creates a window of 4 spaces Vertically which could be used to win
                    string[] Window = { colArray[row], colArray[row + 1], colArray[row + 2], colArray[row + 3]};
                    //4 in the window
                    if ((Window.Count(s => s == b.CurrentPlayer.Colour) == 4))
                    {
                        HValue += 100;
                    }
                    //3 in the window
                    else if ((Window.Count(s => s == b.CurrentPlayer.Colour) == 3) && (Window.Count(s => s == "0")) == 1)
                    {
                        HValue += 10;
                    }

                }
            }









            return HValue;
        }

        private int BestMove(Board b)
        {
            List<int> validLocation = b.getValidLocations();
            int bestScore = 0;
            int bestColumn = validLocation[Rgen.Next(validLocation.Count)];
            

            foreach(int l in validLocation)
            {
                Board tempBoard = b;
                tempBoard.PlaceCounter(l);
                int score = Heuristic(tempBoard, b.CurrentPlayer);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestColumn = l;
                }
            }

            return bestScore;
        }
    }
}
