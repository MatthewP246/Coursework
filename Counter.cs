using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    internal class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string number;
        public Counter(string x)
        {
            number = x;
        }
        public string Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
