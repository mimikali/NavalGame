using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Submarine : Unit
    {
        public Submarine(Player player, Point position) : base(UnitType.Submarine, player, position)
        {

        }

        public override string Information
        {
            get
            {
                return IsSubmerged ? "(Submerged)" : "";
            }
        }

        public override bool IsDetected
        {
            get
            {
                if (!IsSubmerged) return true;
                Random r = new Random(Game.TurnIndex);
                foreach (Unit unit in Game.Units)
                {
                    if (MapDisplay.PointDifference(unit.Position, Position) <= unit.Type.SonarRange && unit.Player.Faction != Player.Faction)
                    {
                        if (r.NextDouble() < 0.5) return true;
                    }
                }
                return false;
            }
        }
    }
}