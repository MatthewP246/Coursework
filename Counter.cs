﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string colour;
        public Counter(string x)
        {
            colour = x;
        }
        public string Colour
        {
            get
            {
                return colour;
            }

            set
            {
                colour = value;
                OnPropertyChanged("Colour");
            }
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
