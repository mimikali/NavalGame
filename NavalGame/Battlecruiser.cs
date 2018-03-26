using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Battlecruiser : Unit
    {
        public Battlecruiser(Player player, Point position) : base(UnitType.Battlecruiser, player, position)
        {
        }
    }
}