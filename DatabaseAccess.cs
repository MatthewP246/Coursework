using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Coursework_UI
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

        public List<Human> PlayersLeaderboard()
        {
            List<Human> Players = new List<Human>();
            using (var Conn = new SQLiteConnection(ConnectionString))
            {
                Conn.Open();
                string query = "SELECT Username, Wins, Losses FROM Players ORDER BY Wins DESC" ;
                using (var cmd = new SQLiteCommand(query, Conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Players.Add(new Human("R", reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                        
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
                string query = "UPDATE Players SET Wins=Wins+1 WHERE Username=@user";
                using (var cmd = new SQLiteCommand(query, Conn))
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
                string query = "UPDATE Players SET Losses=Losses+1 WHERE Username=@user";
                using (var cmd = new SQLiteCommand(query, Conn))
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
                string query = "INSERT INTO Players (Username) VALUES (@user)";
                using (var cmd = new SQLiteCommand(query, Conn))
                {
                    cmd.Parameters.AddWithValue("@user", Username);
                    cmd.ExecuteNonQuery();
                }
                Conn.Close();
            }

        }

        public void SaveGame(Board Board)
        {

        }

        public Board ReturnGame()
        {
            Board Board = new Board("R");
            return Board;
        }
    }
}
