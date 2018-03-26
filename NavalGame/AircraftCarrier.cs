using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class AircraftCarrier : Unit
    {
        public AircraftCarrier(Player player, Point position) : base(UnitType.AircraftCarrier, player, position)
        {
        }
    }
}