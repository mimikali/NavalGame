using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class TorpedoBoat: Unit
    {
        public TorpedoBoat(Player player, Point position) : base(UnitType.TorpedoBoat, player, position)
        {
        }
    }
}