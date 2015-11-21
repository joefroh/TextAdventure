﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureLib
{
    public class Node
    {
        public string Label { get; set; }

        public string Text { get; set; }

        public List<UserInput> Inputs{ get; set; }
    }
}
