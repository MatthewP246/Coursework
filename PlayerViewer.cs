using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /*
     * PLAYER VIEWER
     * 
     * Creates observable collections for the leaderboard and saved games
     * Contains methods for updating the leaderboard and saved games when players are added or games saved
     */
    internal class PlayerViewer
    {
        private DatabaseAccess Access;

        public ObservableCollection<Human> PlayerList {  get; private set; }
        public ObservableCollection<int> SavedGames { get; private set; }
        public PlayerViewer() 
        {
            //Initialising the collections as the database
            Access = new DatabaseAccess();
            PlayerList = new ObservableCollection<Human>(Access.GetPlayers());
            SavedGames = new ObservableCollection<int>(Access.GetGames(null));
        }

        public void AddPlayer(string name)
        {
            //Adds player to the Database then recreates the collection with the new player
            Access.AddPlayer(name);
            PlayerList.Clear();
            foreach(var Player in Access.GetPlayers())
            {
                PlayerList.Add(Player);
            }
            OnPropertyChanged(nameof(PlayerList));
        }

        public void AddWin(string Username)
        {
            //Adds a win to the player indictaed then updates leaderboard to show this
            Access.AddWin(Username);
            PlayerList.Clear();
            foreach (var Player in Access.GetPlayers())
            {
                PlayerList.Add(Player);
            }
            OnPropertyChanged(nameof(PlayerList));
        }

        public void AddLoss(string Username)
        {
            //Adds a loss to the player indictaed then updates leaderboard to show this
            Access.AddLoss(Username);
            PlayerList.Clear();
            foreach (var Player in Access.GetPlayers())
            {
                PlayerList.Add(Player);
            }
            OnPropertyChanged(nameof(PlayerList));
        }

        public int SaveGame(Game Game, int GameSaveID)
        {
            //Saves the game to the database and returns the ID of the saved game
            //Updates the list of saved games for the UI
            GameSaveID = Access.SaveGame(Game, GameSaveID);
            SavedGames.Clear();
            foreach (var item in Access.GetGames(null))
            {
                SavedGames.Add(item);
            }
            OnPropertyChanged(nameof(SavedGames));

            return GameSaveID;
        }

        public void DeleteGame(int GameSaveID)
        {
            //Deletes a saved game from the database and updates the list of saved games for the UI
            Access.DeleteGame(GameSaveID);
            SavedGames.Clear();
            foreach (var item in Access.GetGames(null))
            {
                SavedGames.Add(item);
            }
            OnPropertyChanged(nameof(SavedGames));
        }


        public void ChooseSavedGames()
        {
            //Displays only games against the computer
            SavedGames.Clear();
            foreach (var item in Access.GetGames("Computer"))
            {
                SavedGames.Add(item);
            }
            OnPropertyChanged(nameof(SavedGames));
        }

        //Calls an event to update the bindings
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
