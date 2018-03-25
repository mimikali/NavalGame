using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class MediumCargo : Unit
    {

        public MediumCargo(Player player, Point position) : base(UnitType.MediumCargo, player, position)
        {
        }
    }
}