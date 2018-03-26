using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class HeavyCruiser : Unit
    {
        public HeavyCruiser(Player player, Point position) : base(UnitType.HeavyCruiser, player, position)
        {
        }
    }
}