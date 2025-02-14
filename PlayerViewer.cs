using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class PlayerViewer
    {
        private DatabaseAccess Access;

        public ObservableCollection<Human> PlayerList {  get; private set; }

        public PlayerViewer() 
        {
            Access = new DatabaseAccess();
            PlayerList = new ObservableCollection<Human>(Access.PlayersLeaderboard());
        }

        public void AddPlayer(string name)
        {
            Access.AddPlayer(name);
            PlayerList.Clear();
            foreach(var  player in Access.PlayersLeaderboard())
            {
                PlayerList.Add(player);
            }
            OnPropertyChanged(nameof(PlayerList));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
