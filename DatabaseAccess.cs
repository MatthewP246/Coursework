using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Connect4
{
    class DatabaseAccess
    {
        private string ConnectionString;

        public DatabaseAccess()
        {
            ConnectionString = "Data Source=|DataDirectory|Connect4DB.db; Version=3;";

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
            List<Human> Players = new List<Human>();
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "SELECT Username, Wins, Losses FROM Players ORDER BY Wins DESC" ;
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
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "UPDATE Players SET Wins=Wins+1 WHERE Username=@user";
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
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "UPDATE Players SET Losses=Losses+1 WHERE Username=@user";
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
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string Query = "INSERT INTO Players (Username) VALUES (@user)";
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
            string Grid = Game.board.grid.ToString();

			if (GameSaveID != 0)
            {
                using (var Conn = new SQLiteConnection(ConnectionString))
                {
                    Conn.Open();
                    string Query = "UPDATE SaveGame SET Grid=@grid, CurrentPlayer=@currentplayer WHERE GameSaveID=@id";
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
                    string Query = "INSERT INTO SaveGame (Player1, Player2, Grid, CurrentPlayer) VALUES (@player1, @player2, @grid, @currentplayer)";
                    using (var cmd = new SQLiteCommand(Query, Conn))
                    {
                        cmd.Parameters.AddWithValue("@player1", Game.Player1);
						cmd.Parameters.AddWithValue("@player2", Game.Player2);
						cmd.Parameters.AddWithValue("@grid", Grid);
						cmd.Parameters.AddWithValue("@currentplayer", Game.CurrentPlayer);
						cmd.ExecuteNonQuery();
						GameSaveID = Convert.ToInt32(cmd.ExecuteScalar());
					}
                    Conn.Close();
                }
            }
			return GameSaveID;
		}
        public Game LoadGame(int GameSaveID, string Player1, string Player2)
        {
            Game Game;
			string Grid="";
			string CurrentPlayer="";
            string Difficulty="";


			using (var Conn=new SQLiteConnection(ConnectionString))
            {
				Conn.Open();
				string Query = "SELECT Grid, CurrentPlayer, Difficulty " +
                    "FROM Players, PlayerGameSave, SaveGame " +
                    "WHERE Players.PlayerID=PlayerGameSave.PlayerID " +
                    "AND SaveGame.GameSaveID = PlayerGameSave.GameSaveID" +
                    "AND PayerGameSave.GameSaveID=@id";
				using (var cmd = new SQLiteCommand(Query, Conn))
                {
                    cmd.Parameters.AddWithValue("@id", GameSaveID);
                    using (var Reader = cmd.ExecuteReader())
                    {

                        while (Reader.Read())
                        {
                            Grid = Reader.GetString(0);
                            CurrentPlayer = Reader.GetString(1);
                            Difficulty = Reader.GetString(2);

                        }
                    }
                    
                }
                Conn.Close();
                    
			}


			Game=new Game(CurrentPlayer, Player1, Player2, Difficulty);
            for(int i = 0; i < 42; i++)
            {
                Game.board.grid[i].Colour= Grid[i].ToString();
			}
            


			return Game;
        }
    }
}
