using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    class Destroyer : Unit
    {
        public Destroyer(Player player, Point position) : base(UnitType.Destroyer, player, position)
        {
        }
    }
}
