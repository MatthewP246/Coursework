﻿using System;
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
        //Creates new random generator
        private Random Rgen = new Random();
        //assumes player colour is red
        private string PlayerColour = "Y";
            

        public Computer(string Colour) : base( Colour)
        {
            //if Computer colour is red, player colour is yellow instead
            if (Colour == "Y") PlayerColour = "R";
        }

        public override string PlaceCounter(int C, Board b, string Difficulty)
        {
            //Alternate computer algorithm just checking each move on the given board, no looking more than 1 move ahead
            //C=BestMove(b);

            //Checks difficulty selected before calling minmax with different depths for each difficulty
            if(Difficulty=="Hard") C = MinMax(b, 7, int.MinValue, int.MaxValue, true).Item1;
            else if (Difficulty=="Medium") C = MinMax(b, 5, int.MinValue, int.MaxValue, true).Item1;
            else C = MinMax(b, 2, int.MinValue, int.MaxValue, true).Item1;

            //Actually places the counter on the board and returns the state of the game
            return b.PlaceCounter(C, false);
        }

        

        private int Heuristic(Board b)
        {
            int HValue = 0;

            //Centre Score
            //Adds extra weight to the centre column as it has more options to win
            string[] CentreArray = { b.gg[3, 0].Colour, b.gg[3, 1].Colour, b.gg[3, 2].Colour, b.gg[3, 3].Colour, b.gg[3, 4].Colour, b.gg[3, 5].Colour };
            int CentreCount = 0;
            for (int i = 0;i < CentreArray.Length; i++)
            {
                //Increments a counter based on number of player counters in the Middle column
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
                    //Assings a value based on the number of player counters in the window and adds to a total
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
                    //Assigns a value to this window
                    HValue += WindowCheck(Window, b);

                }
            }

            //Diagonal 1 (Bottom Left -> Top Right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string[] Window = { b.gg[col, row].Colour, b.gg[col + 1, row + 1].Colour, b.gg[col + 2, row + 2].Colour, b.gg[col + 3, row + 3].Colour };
                    HValue += WindowCheck(Window, b);
                }
            }
            //Diagonal 2 (Top Left -> Bottom Right)
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
            
            //if 4 player counters in the window, increment the score by the given value
            if ((Window.Count(s => s == getColour) == 4))
            {
                //4 indicates a win so add a much greater value for wins over anything else
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
            if ((Window.Count(s => s == PlayerColour) == 3) && (Window.Count(s => s == "0")) == 1)
            {
                //Adds ability to block the player winning over getting more in a row for the computer
                HValue -= 6;
            }
            //Opponent has 2 in window
            else if ((Window.Count(s => s == PlayerColour) == 2) && (Window.Count(s => s == "0")) == 2)
            {
                HValue -= 4;
            }
            return HValue;

        }
        private int BestMove(Board b)
        {
            LinkList validLocation = b.getValidLocations();
            //Assings initial value of -1 for best column as this indicates the board is full
            int bestColumn = -1;
            int bestScore = 0;

            if (validLocation != null)
            {
                //if there are moves available, assing a random column as the best column
                bestColumn = validLocation.peek(Rgen.Next(validLocation.Count()));



                for(int i = 0;i<validLocation.Count(); i++ )
                {
                    //Assings l to each location in valid Location s
                    int l = validLocation.peek(i);
                    //Initialises a temporary board and assings all counter to their respective locations based on the main board
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
                    for (int x = 0; x < 42; x++)
                    {
                        if (b.g[x].Colour == "R") tempBoard.g[x].Colour = "R";
                        else if (b.g[x].Colour == "Y") tempBoard.g[x].Colour = "Y";
                        else tempBoard.g[x].Colour = "0";
                    }
                    //Places a counter in the temporary board
                    tempBoard.PlaceCounter(l, true);
                    //Assigns a score to the board based on the heuristic values
                    int score = Heuristic(tempBoard);
                    //Assigns best column to the board which placing in that column returns the greatest score
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestColumn = l;
                    }
                }
            }

            return bestColumn;
        }

        private (int, int) MinMax(Board b, int depth, int alpha, int beta, bool MaximisingPlayer)
        {
            LinkList ValidLocations = b.getValidLocations();
            //Checks if the node of the minmax array is a terminal node
            bool Terminal = TerminalNode(b);
            //-1 indicates the board is full
            int bestColumn = -1;



            //Depth indicates how far the minmax algorithm can look ahead
            if (depth == 0 || Terminal)
            {
                if (Terminal)
                {
                    //returns a value based on if the computer is playing, the human is playing or its a draw
                    if (b.p.Colour == getColour) return (-1, 1000000000);
                    else if (b.p.Colour == PlayerColour) return (-1, -1000000000);
                    else return (-1, 0);
                }
                else
                {
                    //Depth=0
                    return (-1, Heuristic(b));
                }
            }
            //the player is trying to maximise the heuristic value of the board
            if (MaximisingPlayer)
            {
                int value = int.MinValue;

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
                    //places a counter in the temporary board
                    tempBoard.PlaceCounter(l, true);
                    //Recursively calls the algorithm and assings an integer value to the search ahead
                    int newScore = MinMax(tempBoard, depth - 1, alpha, beta, false).Item2;
                    if (newScore > value)
                    {
                        //if the next move is better than the previous, assing a new best column
                        value = newScore;
                        bestColumn = l;
                    }
                    //use of alpha-beta pruning to increase the efficiency of the alogrithm
                    alpha = Math.Max(value, alpha);
                    if (alpha >= beta) break;

                }
                return (bestColumn, value);
            }
            else //Minimising Player
            {
                int value = int.MaxValue;


                foreach (int l in ValidLocations)
                {
                    Board tempBoard = new Board(PlayerColour);
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
            if (b.checkWin() || b.getValidLocations().Count() == 0) return true;
            else return false;
        } 



        
    }
}
