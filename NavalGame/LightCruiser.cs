using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class LightCruiser : Unit
    {
        public LightCruiser(Player player, Point position) : base(UnitType.LightCruiser, player, position)
        {
        }
    }
}