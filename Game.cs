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
        private Board board;
        //Array for the 2 players, can be computer or human
        private Player[] Players;
        private bool AI;
        private string Player1Colour;
        private string Player1Name;
        private string Player2Colour;
        private string Player2Name;
        private string difficulty;

        public Game(string FirstPlayer, string P1Name, string P2Name, string Diff)
        {
            //Assinging values to the variables
            Players = new Player[2];
            Player1Colour=FirstPlayer;
            Player1Name=P1Name;
            Player2Name=P2Name;
            AI = (P2Name == "Computer");
            difficulty = Diff;
            Player2Colour= FirstPlayer== "R" ? "Y" : "R";

            board = new Board(FirstPlayer);

            //Player 1 is always a human
            Players[0] = new Human(Player1Colour,Player1Name, 0,0);
            
            //Creates either a computer Player or another human based on selection made
            if (AI) Players[1] = new Computer(Player2Colour);
            else Players[1] = new Human(Player2Colour, Player2Name,0,0);


            
        }

        public Board Board
        {
            get { return board; }
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
        public string Difficulty
        {
            get { return difficulty; }
        }




		public (string, string, string) PlaceCounter(int Column)
        {
            string Winner="";
            string Loser="";
            string Status;
            if (Column == -1 && AI)
            {
                //Computer is making the move
                Status=Players[1].PlaceCounter(Column, board, difficulty);
                if (Status == "Win")
                {
                    Loser = Player1Name;
                }
            }
            else if (Player1Colour == Board.Player.Colour)
            {
                //Player making the move is player 1
                Status = Players[0].PlaceCounter(Column, board, difficulty);
                if (Status=="Win")
                {
                    Winner = Player1Name;
                    if(!AI) Loser = Player2Name;
                    
                }
            }
            else
            {
                //Player making the move is player 2
                Status = (Players[1].PlaceCounter(Column, board, difficulty));
                if (Status == "Win")
                {
                    Winner= Player2Name;
                    Loser = Player1Name;
                }
            }

            return (Status, Winner, Loser);
        }

        public bool CheckWin()
        {
            return Board.CheckWin();
        }

        



    }
}
