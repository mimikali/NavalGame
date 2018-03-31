using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class CoastalBattery : Unit
    {
        public CoastalBattery(Player player, Point position) : base(UnitType.CoastalBattery, player, position)
        {
        }
    }
}