using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Connect4
{
    public class Game
    {
        //Initialising variables
        private Board Board;
        //Array for the 2 players, can be computer or human
        private Player[] Players;
        private bool AI;
        private string Player1Colour;
        private string Player1Name;
        private string Player2Colour;
        private string Player2Name;
        private string Difficulty;
        private DatabaseAccess Database;

        public Game(string FirstPlayer, string P1Name, string P2Name, string Diff)
        {
            //Assinging values to the variables
            Players = new Player[2];
            Database = new DatabaseAccess();
            Player1Colour=FirstPlayer;
            Player1Name=P1Name;
            Player2Name=P2Name;
            AI = (P2Name == "Computer");
            Difficulty = Diff;
            Player2Colour= FirstPlayer== "R" ? "Y" : "R";

            Board = new Board(FirstPlayer);

            //Player 1 is always a human
            Players[0] = new Human(Player1Colour,Player1Name, 0,0);
            
            //Creates either a computer Player or another human based on selection made
            if (AI) Players[1] = new Computer(Player2Colour);
            else Players[1] = new Human(Player2Colour, Player2Name,0,0);


            
        }

        public Board board
        {
            get { return Board; }
        }

        public string Player1
		{
			get { return Player1Name; }
		}
        public string Player2
        {
			get { return Player2Name; }
		}

        public string CurrentPlayer
		{
			get { return Player1Colour; }
		}




		public string PlaceCounter(int Column)
        {
            string Winner="";
            string Loser="";
            string Status;
            if (Column == -1 && AI)
            {
                //Computer is making the move
                Status=Players[1].PlaceCounter(Column, Board, Difficulty);
                if (Status == "Win")
                {
                    Loser = Player1Name;
                }
            }
            else if (Player1Colour == Board.Player.Colour)
            {
                //Player making the move is player 1
                Status = Players[0].PlaceCounter(Column, Board, Difficulty);
                if (Status=="Win")
                {
                    Winner = Player1Name;
                    if(!AI) Loser = Player2Name;
                    
                }
            }
            else
            {
                //Player making the move is player 2
                Status = (Players[1].PlaceCounter(Column, Board, Difficulty));
                if (Status == "Win")
                {
                    Winner= Player2Name;
                    Loser = Player1Name;
                }
            }

            if (Winner != "") Database.AddWin(Winner);
            Database.AddLoss(Loser);

            return Status;
        }

        public bool CheckWin()
        {
            return Board.CheckWin();
        }

        



    }
}
