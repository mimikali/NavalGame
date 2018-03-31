using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class BatteryBarge : Unit
    {
        public BatteryBarge(Player player, Point position) : base(UnitType.BatteryBarge, player, position)
        {
            InstallsLeft = 1;
        }
    }
}