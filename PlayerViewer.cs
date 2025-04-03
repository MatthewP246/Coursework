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
    internal class PlayerViewer
    {
        private DatabaseAccess Access;

        public ObservableCollection<Human> PlayerList {  get; private set; }
        public ObservableCollection<int> SavedGames { get; private set; }
        private string Player1Name;
        private string Player2Name;
        public PlayerViewer() 
        {
            Access = new DatabaseAccess();
            PlayerList = new ObservableCollection<Human>(Access.GetPlayers());
            SavedGames = new ObservableCollection<int>(Access.GetGames(Player1Name,Player2Name));
        }

        public void AddPlayer(string name)
        {
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
            GameSaveID=Access.SaveGame(Game, GameSaveID);
            SavedGames.Clear();
            foreach (var item in SavedGames)
            {
                SavedGames.Add(item);
            }
            OnPropertyChanged(nameof(SavedGames));

            return GameSaveID;
        }

        public void ChooseSavedGames(string Player1, string Player2)
        {
            Player1Name = Player1;
            Player2Name = Player2;
            SavedGames.Clear();
            foreach (var item in Access.GetGames(Player1, Player2))
            {
                SavedGames.Add(item);
            }
            OnPropertyChanged(nameof(SavedGames));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
