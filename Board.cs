using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xaml.Schema;

namespace Coursework_UI
{
    internal class Board
    {
        private Counter[] Grid;
        private Counter[,] Grid2D;
        private Counter CurrentPlayer;


        


        public Board(string FirstPlayer)
        {
            //initialising the grid as a blank array of 0's
            Grid = new Counter[42];
            Grid2D = new Counter[7, 6];

            
            for (int x = 0; x < Grid.Length; x++)
            {
                Grid[x] = new Counter("0");
                Grid2D[x % 7, x / 7] = new Counter("0");
            }
            //Assigning the first player based on input
            CurrentPlayer = new Counter(FirstPlayer);
        }
        public Counter[] grid
        {
            //get method for 1D grid
            //Used for data binding
            get { return Grid; }
        }



        public Counter[,] grid2D
        {
            //get method for 2D grid array
            get { return Grid2D; }
            set { Grid2D = value; }
        }

        public Counter player
        {
            //Get method for Player making the move
            //Used for data binding
            get { return CurrentPlayer; }
        }

        public string PlaceCounter(int Column, bool Temp)
        {
            
            string Status = "Ongoing";
            //Checks if the board is full
            if (GetValidLocations().Count() == 0)
            {
                Status = "Draw";
            }
            //Only checks for win if a counter is placed
            else if (RecursivePlace(Column))
            {
                //Doesnt check for a win if temporarily placing counter
                //Used in minmax algorithm
                if (Temp == false)
                {

                    if (CheckWin()) Status = "Win"; //Returns win based on check win function
                    else UpdatePlayer(); //If the player didn't win, update for it to be the next players go
                }
            }
            else Status = "No Move";

            return Status;

        }


        private bool RecursivePlace(int Column)
        {
            //Recursive algorithm to place counter
            bool Placed = true;
            if (Column <42)
            {

                if (Grid[Column].Colour == "0")
                {
                    Grid[Column].Colour = CurrentPlayer.Colour;
                    Grid2D[Column %7,Column/7].Colour = CurrentPlayer.Colour;
                }
                else 
                {
                    Placed = RecursivePlace(Column +7);

                }
                
            }
            else Placed = false;

            return Placed;
        }


        public LinkList GetValidLocations()
        {
            //Checks which Columnumns are a valid location and returns a list of valid Columnumns
            LinkList ValidLocations = new LinkList();

            for(int i = 0; i < 7; i++)
            {
                if (IsValidLocation(i)) ValidLocations.Add(i);

            }

            return ValidLocations;
        }


        private bool IsValidLocation(int Column)
        {
            //Checks if the top Row of each Column is empty
            if (Grid[35+Column].Colour == "0") return true;
            else return false;
        }




        private void UpdatePlayer()
        {
            //Updating which Columnour is placing a counter after each go
            if (CurrentPlayer.Colour == "R") CurrentPlayer.Colour = "Y";
            else CurrentPlayer.Colour = "R";
        }


        public bool CheckWin()
        {

            bool Win = false;
            ulong[] Rows = new ulong[6];

            //Creates a bitboard for each Row in the grid
            for (int x = 0; x < 7; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    if (Grid[x+y*7].Colour == CurrentPlayer.Colour)
                    {
                        Rows[y] = Rows[y] + Convert.ToUInt64(Math.Pow(2, x));
                    }
                }
            }
            
            //Intitialise all variables
            ulong Horizontal = 0;
            ulong Vertical = 0;
            ulong Diagonal1 = 0;
            ulong Diagonal2 = 0;


            for (int i = 0; i < 6; i++)
            {
                //Horizontal Check:
                Horizontal = Rows[i] & (Rows[i] << 1) & (Rows[i] << 2) & (Rows[i] << 3);


                //Prevents index out of bounds errors
                if (i < 3)
                {
                    //Vertical Check:

                    Vertical = Rows[i] & Rows[i + 1] & Rows[i + 2] & Rows[i + 3];

                    //Diagonal Check (bottom-left to top-right)
                    Diagonal1 = Rows[i] & (Rows[i + 1] << 1) & (Rows[i + 2] << 2) & (Rows[i + 3] << 3);

                    //Diagonal Check (bottom-right to top-left)
                    Diagonal2 = (Rows[i] << 3) & (Rows[i+1] << 2) & (Rows[i+2] << 1) & Rows[i+3];
                }
                // Return true if any of the checks are non-zero, indicating a win
                if(Horizontal != 0)
                {
                    Win = true;
                    //Calls method to change winning moves into counters to make it obvious where the win is
                    FindWinLocation("H");
                    
                    //exits out of loop as soon as win is found
                    break;
                }
                else if(Vertical != 0)
                {
                    Win = true;
                    FindWinLocation("V");
                    break;
                }
                else if (Diagonal1 != 0)
                {
                    Win = true;
                    FindWinLocation("D1");
                    break;
                }
                else if (Diagonal2 != 0)
                {
                    Win = true;
                    FindWinLocation("D2");
                    break;
                }
            }

            

            return Win;
        }

        private void FindWinLocation(string Method)
        {
            //Creates the code to indicate the type of strike through needed for a win
            string Colour = CurrentPlayer.Colour + Method;

            //Horizontal Check
            if (Method == "H")
            {
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        //Checks if the window is a winning location and Converts it to a counter with a strike through
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x+y*7].Colour = Colour;
                            Grid[(x+1) + y * 7].Colour = Colour;
                            Grid[(x + 2) + y * 7].Colour = Colour;
                            Grid[(x + 3) + y * 7].Colour = Colour;
                        }
                    }
                }
            }

            //Vertical Check
            else if (Method == "V")
            {
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x, y + 1].Colour == CurrentPlayer.Colour && Grid2D[x, y + 2].Colour == CurrentPlayer.Colour && Grid2D[x, y + 3].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x + y * 7].Colour = Colour;
                            Grid[x + (y+1) * 7].Colour = Colour;
                            Grid[x + (y + 2) * 7].Colour = Colour;
                            Grid[x + (y + 3) * 7].Colour = Colour;
                        }
                    }
                }
            }

            //Diagonal Check (Bottom right - Top left)
            else if (Method == "D1")
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 3; y < 6; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y - 1].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y - 2].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y - 3].Colour == CurrentPlayer.Colour)
                        {

                            Grid[x + y * 7].Colour = Colour;
                            Grid[(x + 1) + (y-1) * 7].Colour = Colour;
                            Grid[(x + 2) + (y-2) * 7].Colour = Colour;
                            Grid[(x + 3) + (y-3) * 7].Colour = Colour;
                        }
                    }
                }
            }
            //Diagonal Check (Bottom left - Top right)
            else if (Method == "D2")
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (Grid2D[x, y].Colour == CurrentPlayer.Colour && Grid2D[x + 1, y + 1].Colour == CurrentPlayer.Colour && Grid2D[x + 2, y + 2].Colour == CurrentPlayer.Colour && Grid2D[x + 3, y + 3].Colour == CurrentPlayer.Colour)
                        {
                            
                            Grid[x + y * 7].Colour = Colour;
                            Grid[(x + 1) + (y + 1) * 7].Colour = Colour;
                            Grid[(x + 2) + (y + 2) * 7].Colour = Colour;
                            Grid[(x + 3) + (y + 3) * 7].Colour = Colour;
                        }
                    }
                }
            }
        }
    }
}