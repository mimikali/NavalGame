﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

namespace NavalGame
{
    public class UBoat : Submarine
    {
        public UBoat(Player player, Point position) : base(UnitType.UBoat, player, position)
        {

        }
    }
}