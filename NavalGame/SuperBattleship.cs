using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class SuperBattleship : Unit
    {
        public SuperBattleship(Player player, Point position) : base(UnitType.SuperBattleship, player, position)
        {
        }
    }
}