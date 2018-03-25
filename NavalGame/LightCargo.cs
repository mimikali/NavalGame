using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class LightCargo : Unit
    {

        public LightCargo(Player player, Point position) : base(UnitType.LightCargo, player, position)
        {
        }
    }
}