﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_UI
{
    abstract class Player
    {
        public string Colour { get;  set; }

        public abstract void PlaceCounter(int C);


    }
}
