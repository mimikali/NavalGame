using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Submarine : Unit
    {
        public Submarine(Player player, Point position) : base(UnitType.Submarine, player, position)
        {
        }
    }
}