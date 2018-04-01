using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

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
                Name = Player.GetUnitName(this);
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
                Unit newUnit = ShipType.CreateUnit(Player, Position);
                newUnit.Name = Name;
                Game.AddUnit(newUnit);
            }
        }

        public override void Save(XElement unitNode)
        {
            base.Save(unitNode);

            unitNode.SetAttributeValue("ShipType", _ShipType.Name);
        }

        public override void Load(XElement unitNode)
        {
            base.Load(unitNode);

            string typeName = XmlUtils.GetAttributeValue<string>(unitNode, "ShipType");
            _ShipType = UnitType.UnitTypes.First(t => t.Name == typeName);
        }
    }
}