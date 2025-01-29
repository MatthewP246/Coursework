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
        bool AIGame;

        public Game(string PlayerColour, bool AI)
        {
            string OpponentColour;
            AIGame = AI;
            Players[0] = new Human(PlayerColour);
            if (PlayerColour == "R") OpponentColour = "Y";
            else OpponentColour = "R";

            if (AIGame) Players[1] = new Computer(OpponentColour);
            else Players[1] = new Human(OpponentColour);

            board = new Board(PlayerColour);
        }

        public Board b
        {
            get { return board; }
        }




        public void PlaceCounter(int C)
        {
            if(C == -1 && AIGame) Players[1].PlaceCounter(C,board); 
            else if (Players[0].getColour == board.p.Colour) Players[0].PlaceCounter(C, board);
            else Players[1].PlaceCounter(C, board);

            
        }

        public bool CheckWin()
        {
            return board.checkWin();
        }



    }
}
