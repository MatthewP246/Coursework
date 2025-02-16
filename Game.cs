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
        private string Player2Name;
        private string OpponentColour;
        private DatabaseAccess Database;

        public Game(string FirstPlayer, bool AI, string P1Name, string P2Name)
        {
            Database = new DatabaseAccess();
            Player1Colour=FirstPlayer;
            Player1Name=P1Name;
            Player2Name=P2Name;
            AIGame = AI;

            Players[0] = new Human(Player1Colour,Player1Name, 0,0);
            if (FirstPlayer == "R") OpponentColour = "Y";
            else OpponentColour = "R";

            if (AIGame) Players[1] = new Computer(OpponentColour);
            else Players[1] = new Human(OpponentColour, Player2Name,0,0);


            board = new Board(FirstPlayer);
        }

        public Board b
        {
            get { return board; }
        }




        public void PlaceCounter(int C)
        {
            string Winner="";
            string Losser="";
            if (C == -1 && AIGame)
            {
                if(Players[1].PlaceCounter(C, board) == "Win")
                {
                    Losser = Player1Name;
                }
            }
            else if (Player1Colour == board.p.Colour)
            {
                if(Players[0].PlaceCounter(C, board)=="Win")
                {
                    Winner = Player1Name;
                    if(!AIGame) Losser = Player2Name;
                    
                }
            }
            else
            {
                if (Players[1].PlaceCounter(C, board) == "Win")
                {
                    Winner= Player2Name;
                    Losser = Player1Name;
                }
            }

            if (Winner != "") Database.AddWin(Winner);
            Database.AddLoss(Losser);

            
        }

        public bool CheckWin()
        {
            return b.checkWin();
        }

        



    }
}
