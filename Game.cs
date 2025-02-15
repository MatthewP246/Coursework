using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Game
    {
        private Board board;
        private Player[] Players = new Player[2];
        private bool AIGame;
        private string Player1;
        private string OpponentColour;

        public Game(string FirstPlayer, bool AI, string P1Name, string P2Name)
        {
            Player1=FirstPlayer;
            AIGame = AI;
            Players[0] = new Human(Player1,P1Name, 0,0);
            if (FirstPlayer == "R") OpponentColour = "Y";
            else OpponentColour = "R";

            if (AIGame) Players[1] = new Computer(OpponentColour);
            else
            {
                Players[1] = new Human(OpponentColour, P2Name,0,0);
            }

            board = new Board(FirstPlayer);
        }

        public Board b
        {
            get { return board; }
        }




        public void PlaceCounter(int C)
        {
            if(C == -1 && AIGame) Players[1].PlaceCounter(C,board); 
            else if (Player1 == board.p.Colour) Players[0].PlaceCounter(C, board);
            else Players[1].PlaceCounter(C, board);

            
        }

        public bool CheckWin()
        {
            return board.checkWin();
        }



    }
}
