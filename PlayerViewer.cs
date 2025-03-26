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

        public PlayerViewer() 
        {
            Access = new DatabaseAccess();
            PlayerList = new ObservableCollection<Human>(Access.GetPlayers());
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
