using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class ShipInProgress : Unit
    {
        private UnitType _ShipType = UnitType.Wreck;

        public UnitType ShipType
        {
            get
            {
                return _ShipType;
            }

            set
            {
                _ShipType = value;
            }
        }

        public ShipInProgress(Player player, Point position) : base(UnitType.ShipInProgress, player, position)
        {
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (!initialSetup) TurnsUntilCompletion--;
        }

        public override void OnGameChanged()
        {
            base.OnGameChanged();

            if (TurnsUntilCompletion <= 0)
            {
                Game.RemoveUnit(this);
                Unit newUnit = ShipType.CreateUnit(Player, Position);
                newUnit.Name = Name;
                Game.AddUnit(newUnit);
            }
        }
    }
}