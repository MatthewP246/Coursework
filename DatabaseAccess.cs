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
        private string connectionString;

        public DatabaseAccess()
        {
            connectionString = "Data Source=|DataDirectory|Connect4DB.db; Version=3;";

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
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Username, Wins, Losses FROM Players";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Players.Add(new Human("R", reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                        
                    }
                }
                conn.Close();
            }
            return Players;
        }

        public void AddPlayer(string Username)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Players (Username) VALUES (@user)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", Username);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
    }
}
