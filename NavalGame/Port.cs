﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Port : Unit
    {
        public Port(Player player, Point position) : base(UnitType.Port, player, position)
        {
            Cargo = 25;
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (!initialSetup) Cargo++;
        }
    }
}