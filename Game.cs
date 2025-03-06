using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework_UI
{
    internal class Game
    {
        private Board board;
        private Player[] Players = new Player[2];
        private bool AIGame;
        private string Player1Colour;
        private string Player1Name;
        private string Player2Colour;
        private string Player2Name;
        private string Difficulty;
        private DatabaseAccess Database;

        public Game(string FirstPlayer, bool AI, string P1Name, string P2Name, string Diff)
        {
            Database = new DatabaseAccess();
            Player1Colour=FirstPlayer;
            Player1Name=P1Name;
            Player2Name=P2Name;
            AIGame = AI;
            Difficulty = Diff;

            Players[0] = new Human(Player1Colour,Player1Name, 0,0);
            if (FirstPlayer == "R") Player2Colour = "Y";
            else Player2Colour = "R";

            if (AIGame) Players[1] = new Computer(Player2Colour);
            else Players[1] = new Human(Player2Colour, Player2Name,0,0);


            board = new Board(FirstPlayer);
        }

        public Board b
        {
            get { return board; }
        }




        public string PlaceCounter(int C)
        {
            string Winner="";
            string Loser="";
            string Status;
            if (C == -1 && AIGame)
            {
                Status=Players[1].PlaceCounter(C, board, Difficulty);
                if (Status == "Win")
                {
                    Loser = Player1Name;
                }
            }
            else if (Player1Colour == board.p.Colour)
            {
                Status = Players[0].PlaceCounter(C, board, Difficulty);
                if (Status=="Win")
                {
                    Winner = Player1Name;
                    if(!AIGame) Loser = Player2Name;
                    
                }
            }
            else
            {
                Status = (Players[1].PlaceCounter(C, board, Difficulty));
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
            return b.checkWin();
        }

        



    }
}
