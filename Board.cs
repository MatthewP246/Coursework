﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xaml.Schema;

namespace Coursework_UI
{
    internal class Board 
    {
        private Counter[] Locations;

        public Board()
        {
            Locations = new Counter[42];
            for (int x = 0; x < Locations.Length; x++)
            {
                Locations[x] = new Counter("0");
            }

        }
        public Counter[] l
        {
            get { return Locations; }
        }

        public void CreateBoard()
        {

        }



        public bool CheckWin()
        {
            bool Win = false;




            return Win;
        }



        public void PlaceCounter(int C, string v)
        {
            int pos = C * 6;
            while (pos < C*6 + 6)
                if (Locations[pos].Number == "0")
                {
                    Locations[pos].Number = v;
                    break;
                }
                else
                {
                    pos++;
                }

        }




    }
}
