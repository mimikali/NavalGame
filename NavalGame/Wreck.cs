﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Wreck : Unit
    {
        public Wreck(Player player, Point position) : base(UnitType.Wreck, player, position)
        {
        }
    }
}