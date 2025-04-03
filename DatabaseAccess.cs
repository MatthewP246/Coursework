using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Controls.Primitives;

namespace Connect4
{
    class DatabaseAccess
    {
        private string ConnectionString;

        public DatabaseAccess()
        {
            ConnectionString = "Data Source=|DataDirectory|Connect4DB.db; Version=3;";
            //If the file doesn't exist, create it
            if (!File.Exists(GetDatabasePath()))
            {
                SQLiteConnection.CreateFile(GetDatabasePath());
            }
        }

        private string GetDatabasePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Connect4DB");
        }

        public List<Human> GetPlayers()
        {
            //Gets the list of players for the leaderboard and selecting user
            List<Human> Players = new List<Human>();
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                //SQL query
                string Query = "SELECT Username,Wins,Losses " +
                    "FROM Players " +
                    "ORDER BY Wins DESC" ;
                using (var cmd = new SQLiteCommand(Query, Conn))
                using (var Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Players.Add(new Human("R", Reader.GetString(0), Reader.GetInt32(1), Reader.GetInt32(2)));
                        
                    }
                }
                Conn.Close();
            }
            return Players;
        }

        public void AddWin(string Username)
        {
            //Adds a win to the winner
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "UPDATE Players " +
                    "SET Wins=Wins+1 " +
                    "WHERE Username=@user";
                using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@user", Username);
                    cmd.ExecuteNonQuery();
                }
                Conn.Close();
            }
        }
        public void AddLoss(string Username)
        {
            //Adds a loss to the loser
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "UPDATE Players " +
                    "SET Losses=Losses+1 " +
                    "WHERE Username=@user";
                using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@user", Username);
                    cmd.ExecuteNonQuery();
                }
                Conn.Close();
            }
        }

        public void AddPlayer(string Username)
        {
            //Creates a new player
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "INSERT INTO Players (Username) " +
                    "VALUES (@user)";
                using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@user", Username);
                    cmd.ExecuteNonQuery();
                }
                Conn.Close();
            }

        }

        public int SaveGame(Game Game, int GameSaveID)
        {
            //Saves the game to the database
            //If the game is already saved, update it

            //Creates a string of the grid
            List<string> BoardGrid = new List<string>(42);
            for (int i=0; i < 42; i++)
            {
                BoardGrid.Add(Game.Board.grid[i].Colour);
            }
            string Grid= string.Join("", BoardGrid);

            //Checks if game already exists
            if (GameSaveID != 0)
            {
                using (var Conn = new SQLiteConnection(ConnectionString))
                {
                    Conn.Open();
                    string Query = "UPDATE SaveGame " +
                        "SET Grid=@grid, CurrentPlayer=@currentplayer " +
                        "WHERE GameSaveID=@id";
                    using (var cmd = new SQLiteCommand(Query, Conn))
                    {
                        cmd.Parameters.AddWithValue("@grid", Grid);
                        cmd.Parameters.AddWithValue("@currentplayer", Game.CurrentPlayer);
						cmd.Parameters.AddWithValue("@id", GameSaveID);
                        cmd.ExecuteNonQuery();
                    }
                    Conn.Close();
                }
            }
            else
            {
                using (var Conn = new SQLiteConnection(ConnectionString))
                {
                    Conn.Open();
                    string Query = "INSERT INTO SaveGame (Grid, CurrentPlayer, Difficulty) " +
                        "VALUES (@grid, @currentplayer, @difficulty)";
                    using (var cmd = new SQLiteCommand(Query, Conn))
                    {
						cmd.Parameters.AddWithValue("@grid", Grid);
						cmd.Parameters.AddWithValue("@currentplayer", Game.CurrentPlayer);
                        cmd.Parameters.AddWithValue("@difficulty", Game.Difficulty);
                        cmd.ExecuteNonQuery();
                    }
                    //Returns the ID of the game for updating save
                    Query = "SELECT GameSaveID " +
                        "FROM SaveGame " +
                        "WHERE Grid=@grid " +
                        "AND CurrentPlayer=@currentplayer " +
                        "AND Difficulty=@difficulty";
                    using(var cmd = new SQLiteCommand(Query, Conn))
                    {
                        cmd.Parameters.AddWithValue("@grid", Grid);
                        cmd.Parameters.AddWithValue("@currentplayer", Game.CurrentPlayer);
                        cmd.Parameters.AddWithValue("@difficulty", Game.Difficulty);
                        GameSaveID = Convert.ToInt32(cmd.ExecuteScalar());
                    }



                    //Creates records in the link table
                    Query = "INSERT INTO PlayerGameSave (PlayerID, GameSaveID) " +
                        "VALUES ((SELECT PlayerID FROM Players WHERE Username=@user), @id)";
                    using (var cmd = new SQLiteCommand(Query, Conn))
                    {
                        cmd.Parameters.AddWithValue("@id", GameSaveID);
                        cmd.Parameters.AddWithValue("@user", Game.Player1);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.AddWithValue("@id", GameSaveID);
                        cmd.Parameters.AddWithValue("@user", Game.Player2);
                        cmd.ExecuteNonQuery();
                    }
                        Conn.Close();
                }
            }
			return GameSaveID;
		}
        public Game LoadGame(int GameSaveID)
        {
            //Loads the game from the database
            Game Game;
			string Grid="";
			string CurrentPlayer="";
            string Difficulty="";
            string Player1 = "";
            string Player2 = "";


            using (var Conn=new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "SELECT Username, Grid, CurrentPlayer, Difficulty " +
                    "FROM Players, PlayerGameSave, SaveGame " +
                    "WHERE Players.PlayerID = PlayerGameSave.PlayerID " +
                    "AND SaveGame.GameSaveID = PlayerGameSave.GameSaveID " +
                    "AND PlayerGameSave.GameSaveID = 1";

                using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@id", GameSaveID);
                    using (var Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            Player1 = Reader.GetString(0);
                            Grid = Reader.GetString(1);
                            CurrentPlayer = Reader.GetString(2);
                            //Checks if the difficulty is null
                            if (!Reader.IsDBNull(3)) Difficulty = Reader.GetString(3);
                        }
                        Reader.NextResult();
                        while (Reader.Read())
                        {
                            Player2 = Reader.GetString(0);
                        }
                    }
                }
                Conn.Close();
                    
			}


			Game=new Game(CurrentPlayer, Player1, Player2, Difficulty);
            for(int i = 0; i < 42; i++)
            {
                Game.Board.grid[i].Colour= Grid[i].ToString();
			}
            
			return Game;
        }

        public List<int> GetGames(string Player1, string Player2)
        {
            //Returns a list of games for the load game menu
            List<int> Games = new List<int>();
            int GameSaveID;

            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "SELECT SaveGame.GameSaveID " +
                    "FROM Players, PlayerGameSave, SaveGame " +
                    "WHERE Players.PlayerID = PlayerGameSave.PlayerID " +
                    "AND SaveGame.GameSaveID = PlayerGameSave.GameSaveID " +
                    "AND (@player1 IS NULL OR Players.PlayerID=(SELECT PlayerID FROM Players WHERE Username = @player1) " +
                    "OR (@player2 IS NULL OR Players.PlayerID=(SELECT PlayerID FROM Players WHERE Username = @player2)))";
                using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@player1", Player1);
                    cmd.Parameters.AddWithValue("@player2", Player2);
                    using (var Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            GameSaveID = Reader.GetInt32(0);
                            Games.Add(GameSaveID);
                        }

                    }
                }
                Conn.Close();
            }
            return Games;
        }
    }
}
