using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Wreck : Unit
    {
        int _TurnsToLive;

        public Wreck(Player player, Point position) : base(UnitType.Wreck, player, position)
        {
            _TurnsToLive = 3;
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (!initialSetup)
            {
                if (_TurnsToLive <= 0)
                {
                    Game.RemoveUnit(this);
                }
            }
            _TurnsToLive--;
        }
    }
}