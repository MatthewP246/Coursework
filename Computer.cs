using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Coursework_UI
{
    internal class Computer : Player
    {
        private Random Rgen = new Random();
        private string OpponentColour = "R";
            

        public Computer(string Colour) : base( Colour)
        {
            Colour = this.getColour;
            if (Colour == "R") OpponentColour = "Y";
        }

        public override void PlaceCounter(int C, Board b)
        {
            //C=BestMove(b);
            C = MinMax(b, 6,int.MinValue, int.MaxValue, true).Item1;
            b.PlaceCounter(C, false);
        }

        private int BestMove(Board b)
        {
            List<int> validLocation = b.getValidLocations();
            int bestColumn = -1;
            int bestScore = 0;

            if (validLocation != null)
            {
                bestColumn = validLocation[Rgen.Next(validLocation.Count)];



                foreach (int l in validLocation)
                {
                    Board tempBoard = new Board(b.p.Colour);
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 6; y++)
                        {
                            if (b.gg[x, y].Colour == "R") tempBoard.gg[x, y].Colour = "R";
                            else if (b.gg[x, y].Colour == "Y") tempBoard.gg[x, y].Colour = "Y";
                            else tempBoard.gg[x, y].Colour = "0";

                        }
                    }
                    for (int i = 0; i < 42; i++)
                    {
                        if (b.g[i].Colour == "R") tempBoard.g[i].Colour = "R";
                        else if (b.g[i].Colour == "Y") tempBoard.g[i].Colour = "Y";
                        else tempBoard.g[i].Colour = "0";
                    }
                    tempBoard.PlaceCounter(l, true);
                    int score = Heuristic(tempBoard);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestColumn = l;
                    }
                }
            }

            return bestColumn;
        }

        private int Heuristic(Board b)
        {
            int HValue = 0;

            //Centre Score
            string[] CentreArray = { b.gg[3, 0].Colour, b.gg[3, 1].Colour, b.gg[3, 2].Colour, b.gg[3, 3].Colour, b.gg[3, 4].Colour, b.gg[3, 5].Colour };
            int CentreCount = 0;
            for (int i = 0;i < CentreArray.Length; i++)
            {
                if (CentreArray[i] == getColour) CentreCount++;
            }
            HValue += CentreCount * 3;


            //Horizontal score
            for (int row = 0; row < 6; row++)
            {
                //creates an array of each individual row
                string[] rowArray = { b.gg[0, row].Colour, b.gg[1, row].Colour, b.gg[2, row].Colour, b.gg[3, row].Colour, b.gg[4, row].Colour, b.gg[5, row].Colour, b.gg[6, row].Colour };
                for (int col = 0; col < 4; col++)
                {
                    //4 to prevent index out of bounds error
                    //creates a window of 4 spaces horizontally which could be used to win
                    string[] Window = { rowArray[col], rowArray[col + 1], rowArray[col + 2], rowArray[col + 3] };
                    HValue += WindowCheck(Window, b);




                }
            }
            //Vertical Score
            for (int col = 0; col < 7; col++)
            {
                //creates an array of each individual column
                string[] colArray = { b.gg[col, 0].Colour, b.gg[col, 1].Colour, b.gg[col, 2].Colour, b.gg[col, 3].Colour, b.gg[col, 4].Colour, b.gg[col, 5].Colour };
                for (int row = 0; row < 3; row++)
                {
                    //3 to prevent index out of bounds error
                    //creates a window of 4 spaces Vertically which could be used to win
                    string[] Window = { colArray[row], colArray[row + 1], colArray[row + 2], colArray[row + 3] };
                    HValue += WindowCheck(Window, b);

                }
            }

            //Diagonal 1 (Bottom Left - Top Right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string[] Window = { b.gg[col, row].Colour, b.gg[col + 1, row + 1].Colour, b.gg[col + 2, row + 2].Colour, b.gg[col + 3, row + 3].Colour };
                    HValue += WindowCheck(Window, b);
                }
            }
            //Diagonal 2 (Top Left - Bottom Right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string[] Window = { b.gg[col, row+3].Colour, b.gg[col + 1, row +2].Colour, b.gg[col + 2, row +1].Colour, b.gg[col + 3, row].Colour };
                    HValue += WindowCheck(Window, b);
                    
                }
            }

            return HValue;
        }

        private int WindowCheck(string[] Window, Board b)
        {
            int HValue = 0;
            
            //4 in window
            if ((Window.Count(s => s == getColour) == 4))
            {
                HValue += 100000;
            }
            //3 in the window
            else if ((Window.Count(s => s == getColour) == 3) && (Window.Count(s => s == "0")) == 1)
            {
                HValue += 8;
            }
            //2 in window
            else if ((Window.Count(s => s == getColour) == 2) && (Window.Count(s => s == "0")) == 2)
            {
                HValue += 2;
            }


            //Opponent has 3 in window
            if ((Window.Count(s => s == OpponentColour) == 3) && (Window.Count(s => s == "0")) == 1)
            {
                HValue -= 6;
            }
            //Opponent has 2 in window
            else if ((Window.Count(s => s == OpponentColour) == 2) && (Window.Count(s => s == "0")) == 2)
            {
                HValue -= 4;
            }
            return HValue;

        }

        private (int,int) MinMax(Board b, int depth,int alpha, int beta, bool MaximisingPlayer)
        {
            List<int> ValidLocations = b.getValidLocations();
            bool Terminal = TerminalNode(b);

            


            if (depth == 0 || Terminal)
            {
                if (Terminal)
                {
                    if (getColour == b.p.Colour) return (-1, 1000000000);
                    else if (getColour != b.p.Colour) return (-1,-1000000000);
                    else return (-1, 0);
                }
                else 
                {
                    //Depth=0
                    return (-1, Heuristic(b));
                }
            }
            if (MaximisingPlayer)
            {
                int value=int.MinValue;
                int bestColumn = ValidLocations[Rgen.Next(ValidLocations.Count)];

                foreach (int l in ValidLocations)
                {
                    //creates a copy of the Board with the intended player making a move
                    Board tempBoard = new Board(getColour);
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 6; y++)
                        {
                            if (b.gg[x, y].Colour == "R") tempBoard.gg[x, y].Colour = "R";
                            else if (b.gg[x, y].Colour == "Y") tempBoard.gg[x, y].Colour = "Y";
                            else tempBoard.gg[x, y].Colour = "0";

                        }
                    }
                    for (int i = 0; i < 42; i++)
                    {
                        if (b.g[i].Colour == "R") tempBoard.g[i].Colour = "R";
                        else if (b.g[i].Colour == "Y") tempBoard.g[i].Colour = "Y";
                        else tempBoard.g[i].Colour = "0";
                    }
                    tempBoard.PlaceCounter(l, true);
                    int newScore = MinMax(tempBoard, depth - 1, alpha, beta, false).Item2;
                    if (newScore > value)
                    {
                        value = newScore;
                        bestColumn = l;
                    }
                    alpha = Math.Max(value, alpha);
                    if (alpha >= beta) break;
                    
                }
                return (bestColumn, value);
            }
            else //Minimising Player
            {
                int value = int.MaxValue;
                int bestColumn = ValidLocations[Rgen.Next(ValidLocations.Count)];


                foreach (int l in ValidLocations)
                {
                    Board tempBoard = new Board(OpponentColour);
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 6; y++)
                        {
                            if (b.gg[x, y].Colour == "R") tempBoard.gg[x, y].Colour = "R";
                            else if (b.gg[x, y].Colour == "Y") tempBoard.gg[x, y].Colour = "Y";
                            else tempBoard.gg[x, y].Colour = "0";

                        }
                    }
                    for (int i = 0; i < 42; i++)
                    {
                        if (b.g[i].Colour == "R") tempBoard.g[i].Colour = "R";
                        else if (b.g[i].Colour == "Y") tempBoard.g[i].Colour = "Y";
                        else tempBoard.g[i].Colour = "0";
                    }
                    tempBoard.PlaceCounter(l, true);
                    int newScore = MinMax(tempBoard, depth - 1, alpha, beta, true).Item2;
                    if (newScore < value)
                    {
                        value = newScore;
                        bestColumn = l;
                    }
                    beta = Math.Min(beta, value);
                    if (alpha >= beta) break;
                    
                }
                return (bestColumn, value);

            }

        }

        private bool TerminalNode(Board b)
        {
            if (b.checkWin() || b.getValidLocations().Count == 0) return true;
            else return false;
        } 



        
    }
}
