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
        //Creates new random generator
        //assumes Player Colour is Yellow
        private string PlayerColour = "Y";
            

        public Computer(string Colour) : base( Colour)
        {
            //if Computer Columnour is yellow, Player Columnour is red and vice versa
            if (Colour == "Y") PlayerColour = "R";
        }

        public override string PlaceCounter(int Column, Board Board, string Difficulty)
        {
            //Alternate computer algorithm just checking each move on the given board, no looking more than 1 move aHead
            //C=BestMove(Board);

            //Checks difficulty selected before calling minmax with different depths for each difficulty
            //Minmax returns the best Column to place the counter
            if(Difficulty=="Hard") Column = MinMax(Board, 7, int.MinValue, int.MaxValue, true).Item1;
            else if (Difficulty=="Medium") Column = MinMax(Board, 5, int.MinValue, int.MaxValue, true).Item1;
            else Column = MinMax(Board, 2, int.MinValue, int.MaxValue, true).Item1;

            //Actually places the counter on the board and returns the state of the game
            return Board.PlaceCounter(Column, false);
        }

        

        private int Heuristic(Board Board)
        {
            int HValue = 0;

            //Centre Score
            //Adds extra weight to the centre Column as it has more options to win
            string[] CentreArray = { Board.grid2D[3, 0].Colour, Board.grid2D[3, 1].Colour, Board.grid2D[3, 2].Colour, Board.grid2D[3, 3].Colour, Board.grid2D[3, 4].Colour, Board.grid2D[3, 5].Colour };
            int CentreCount = 0;
            for (int i = 0;i < CentreArray.Length; i++)
            {
                //Increments a counter based on number of Player counters in the Middle Column
                if (CentreArray[i] == GetColour) CentreCount++;
            }
            HValue += CentreCount * 3;


            //Horizontal Score
            for (int Row = 0; Row < 6; Row++)
            {
                //creates an array of each individual Row
                string[] RowArray = { Board.grid2D[0, Row].Colour, Board.grid2D[1, Row].Colour, Board.grid2D[2, Row].Colour, Board.grid2D[3, Row].Colour, Board.grid2D[4, Row].Colour, Board.grid2D[5, Row].Colour, Board.grid2D[6, Row].Colour };
                for (int Column = 0; Column < 4; Column++)
                {
                    //4 to prevent index out of bounds error
                    //creates a window of 4 spaces horizontally which could be used to win
                    string[] Window = { RowArray[Column], RowArray  [Column + 1], RowArray[Column + 2], RowArray[Column + 3] };
                    //Assings a Value based on the number of Player counters in the window and adds to a total
                    HValue += WindowCheck(Window);




                }
            }
            //Vertical Score
            for (int Column = 0; Column < 7; Column++)
            {
                //creates an array of each individual Column
                string[] ColumnArray = { Board.grid2D[Column, 0].Colour, Board.grid2D[Column, 1].Colour, Board.grid2D[Column, 2].Colour, Board.grid2D[Column, 3].Colour, Board.grid2D[Column, 4].Colour, Board.grid2D[Column, 5].Colour };
                for (int Row = 0; Row < 3; Row++)
                {
                    //3 to prevent index out of bounds error
                    //creates a window of 4 spaces Vertically which could be used to win
                    string[] Window = { ColumnArray[Row], ColumnArray[Row + 1], ColumnArray[Row + 2], ColumnArray[Row + 3] };
                    //Assigns a Value to this window
                    HValue += WindowCheck(Window);

                }
            }

            //Diagonal 1 (Bottom Left -> Top Right)
            for (int Row = 0; Row < 3; Row++)
            {
                for (int Column = 0; Column < 4; Column++)
                {
                    string[] Window = { Board.grid2D[Column, Row].Colour, Board.grid2D[Column + 1, Row + 1].Colour, Board.grid2D[Column + 2, Row + 2].Colour, Board.grid2D[Column + 3, Row + 3].Colour };
                    HValue += WindowCheck(Window);
                }
            }
            //Diagonal 2 (Top Left -> Bottom Right)
            for (int Row = 0; Row < 3; Row++)
            {
                for (int Column = 0; Column < 4; Column++)
                {
                    string[] Window = { Board.grid2D[Column, Row+3].Colour, Board.grid2D[Column + 1, Row +2].Colour, Board.grid2D[Column + 2, Row +1].Colour, Board.grid2D[Column + 3, Row].Colour };
                    HValue += WindowCheck(Window);
                    
                }
            }

            return HValue;
        }

        private int WindowCheck(string[] Window)
        {
            //Assigns a Value to each window
            int HValue = 0;

            
            if (Window.Count(s => s == "0") == 4) ;//Don't check for any counters if the window is empty
            else
            {

                //if 4 Player counters in the window, increment the Score by the given Value
                if ((Window.Count(s => s == GetColour) == 4))
                {
                    //4 indicates a win so add a much greater Value for wins over anything else
                    HValue += 100000;
                }
                //3 in the window
                else if ((Window.Count(s => s == GetColour) == 3) && (Window.Count(s => s == "0")) == 1)
                {
                    HValue += 8;
                }
                //2 in window
                else if ((Window.Count(s => s == GetColour) == 2) && (Window.Count(s => s == "0")) == 2)
                {
                    HValue += 2;
                }


                //Opponent has 3 in window
                if ((Window.Count(s => s == PlayerColour) == 3) && (Window.Count(s => s == "0")) == 1)
                {
                    //Adds ability to block the Player winning over getting more in a Row for the computer
                    //Value is less than the Player winning so it always wins the game for itself first
                    HValue -= 6;
                }
                //Opponent has 2 in window
                else if ((Window.Count(s => s == PlayerColour) == 2) && (Window.Count(s => s == "0")) == 2)
                {
                    HValue -= 4;
                }
            }
            return HValue;

        }
        private int BestMove(Board Board)
        {
            LinkList ValidLocation = Board.GetValidLocations();
            Random Rgen = new Random();
            //Assings initial Value of -1 for best Column as this indicates the board is full
            int BestColumn = -1;
            int BestScore = 0;

            if (ValidLocation != null)
            {
                //if there are moves available, assign a random Column as the best Column
                BestColumn = ValidLocation.Peek(Rgen.Next(ValidLocation.Count()));



                for(int i = 0;i<ValidLocation.Count(); i++ )
                {
                    //Assings l to each location in valid Location s
                    int l = ValidLocation.Peek(i);
                    //Initialises a temporary board and assigns all counters to their respective locations based on the main board
                    Board TempBoard = new Board(Board.Player.Colour);
                    for (int j = 0; j < 42; j++)
                    {
                        if (Board.grid[j].Colour == "R")
                        {
                            TempBoard.grid[j].Colour = "R";
                            TempBoard.grid2D[j % 7, j / 7].Colour = "R";
                        }
                        else if (Board.grid[j].Colour == "Y")
                        {
                            TempBoard.grid[j].Colour = "Y";
                            TempBoard.grid2D[j % 7, j / 7].Colour = "Y";
                        }

                    }
                    //Places a counter in the temporary board
                    TempBoard.PlaceCounter(l, true);
                    //Assigns a Score to the board based on the heuristic values
                    int Score = Heuristic(TempBoard);
                    //Assigns best Column to the board which placing in that Column returns the greatest Score
                    if (Score > BestScore)
                    {
                        BestScore = Score;
                        BestColumn = l;
                    }
                }
            }

            return BestColumn;
        }

        private (int, int) MinMax(Board Board, int Depth, int Alpha, int Beta, bool MaximisingPlayer)
        {
            LinkList ValidLocations = Board.GetValidLocations();
            //Checks if the node of the minmax array is a terminal node
            bool Terminal = TerminalNode(Board);
            //initial condition is -1 as this indicates the board is full
            int BestColumn = -1;



            //Depth indicates how far the minmax algorithm can look aHead
            if (Depth == 0 || Terminal)
            {
                if (Terminal)
                {
                    //returns a Value based on if the computer is playing, the human is playing or its a draw
                    //also returns a Column of -1 to indicate the game is over
                    if (Board.Player.Colour == GetColour) return (-1, 1000000000);
                    else if (Board.Player.Colour == PlayerColour) return (-1, -1000000000);
                    else return (-1, 0);
                }
                else
                {
                    //Depth=0
                    return (-1, Heuristic(Board));
                }
            }
            //the Player is trying to maximise the heuristic Value of the board
            if (MaximisingPlayer)
            {
                int Value = int.MinValue;

                foreach (int l in ValidLocations)
                {
                    //creates a copy of the Board with the intended Player making a move
                    Board TempBoard = new Board(GetColour);
                    for (int i = 0; i < 42; i++)
                    {
                        if (Board.grid[i].Colour == "R")
                        {
                            TempBoard.grid[i].Colour = "R";
                            TempBoard.grid2D[i % 7, i / 7].Colour = "R";
                        }
                        else if (Board.grid[i].Colour == "Y")
                        {
                            TempBoard.grid[i].Colour = "Y";
                            TempBoard.grid2D[i % 7, i / 7].Colour = "Y";
                        }

                    }
                    //places a counter in the temporary board
                    TempBoard.PlaceCounter(l, true);
                    //Recursively calls the algorithm and assings an integer Value to the search aHead
                    //recursive call returns the heuristic Value of each move
                    int NewScore = MinMax(TempBoard, Depth - 1, Alpha, Beta, false).Item2;
                    if (NewScore > Value)
                    {
                        //if the next move is better than the previous, assing a new best Column
                        Value = NewScore;
                        BestColumn = l;
                    }
                    //use of alpha-beta pruning to increase the efficiency of the alogrithm
                    //eliminates certain checks from being needed based on already known information
                    Alpha = Math.Max(Value, Alpha);
                    if (Alpha >= Beta) break;

                }
                //Returns both the best Column and heuristic Value of placing in that Column
                //This allows the algorithm to call itself and update score but at the end it still returns the best column
                return (BestColumn, Value);
            }
            else //Minimising Player
            {
                int Value = int.MaxValue;


                foreach (int l in ValidLocations)
                {
                    Board TempBoard = new Board(PlayerColour);
                    for (int i = 0; i < 42; i++)
                    {
                        if (Board.grid[i].Colour == "R")
                        {
                            TempBoard.grid[i].Colour = "R";
                            TempBoard.grid2D[i % 7, i / 7].Colour = "R";
                        }
                        else if (Board.grid[i].Colour == "Y")
                        {
                            TempBoard.grid[i].Colour = "Y";
                            TempBoard.grid2D[i % 7, i / 7].Colour = "Y";
                        }

                    }
                    TempBoard.PlaceCounter(l, true);
                    int NewScore = MinMax(TempBoard, Depth - 1, Alpha, Beta, true).Item2;
                    if (NewScore < Value)
                    {
                        Value = NewScore;
                        BestColumn = l;
                    }
                    Beta = Math.Min(Beta, Value);
                    if (Alpha >= Beta) break;

                }
                return (BestColumn, Value);

            }

        }

        private bool TerminalNode(Board Board)
        {
            //Checks if the game is over or no more moves are possible
            if (Board.CheckWin() || Board.GetValidLocations().Count() == 0) return true;
            else return false;
        } 



        
    }
}
