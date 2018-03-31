using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class TroopShip: Unit
    {
        public TroopShip(Player player, Point position) : base(UnitType.TroopShip, player, position)
        {
        }
    }
}