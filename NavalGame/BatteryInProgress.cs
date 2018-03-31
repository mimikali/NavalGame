using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class BatteryInProgress : Unit
    {
        public BatteryInProgress(Player player, Point position) : base(UnitType.BatteryInProgress, player, position)
        {
            TurnsUntilCompletion = 6;
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (!initialSetup) TurnsUntilCompletion--;
        }

        public override string Information
        {
            get
            {
                return TurnsUntilCompletion.ToString("0") + " turns left";
            }
        }

        public override void OnGameChanged()
        {
            base.OnGameChanged();

            if (TurnsUntilCompletion <= 0)
            {
                Game.RemoveUnit(this);
                Unit newUnit = UnitType.CoastalBattery.CreateUnit(Player, Position);
                newUnit.Name = Name;
                Game.AddUnit(newUnit);
            }
        }
    }
}