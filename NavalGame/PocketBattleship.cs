using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class PocketBattleship : Unit
    {
        public PocketBattleship(Player player, Point position) : base(UnitType.PocketBattleship, player, position)
        {
        }
    }
}