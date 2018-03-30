using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Factory: Unit
    {
        public Factory(Player player, Point position) : base(UnitType.Factory, player, position)
        {
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (!initialSetup) Cargo += 2;
        }
    }
}