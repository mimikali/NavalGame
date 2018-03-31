using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    class Frigate: Unit
    {
        public Frigate(Player player, Point position) : base(UnitType.Frigate, player, position)
        {
        }
    }
}
