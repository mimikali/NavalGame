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
            if (!initialSetup && Player.Faction == Faction.Neutral) Cargo += 2;
            else if (!initialSetup) Cargo += 3;
        }
    }
}