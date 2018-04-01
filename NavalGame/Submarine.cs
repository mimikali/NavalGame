using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

namespace NavalGame
{
    public class Submarine : Unit
    {
        private int _OxygenLeft;

        public int OxygenLeft
        {
            get
            {
                return _OxygenLeft;
            }

            private set
            {
                _OxygenLeft = value;
            }
        }

        public Submarine(Player player, Point position) : base(UnitType.Submarine, player, position)
        {

        }

        public override string Information
        {
            get
            {
                return (IsSubmerged ? "Submerged\n" : "") + base.Information;
            }
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (IsSubmerged)
            {
                OxygenLeft--;
                if (OxygenLeft <= 0)
                {
                    IsSubmerged = false;
                    DivesLeft = 0;
                }
            }
            if (!IsSubmerged)
                OxygenLeft = 8;
        }

        public override void Save(XElement unitNode)
        {
            base.Save(unitNode);
            unitNode.SetAttributeValue("OxygenLeft", OxygenLeft);
        }

        public override void Load(XElement unitNode)
        {
            base.Load(unitNode);
            OxygenLeft = XmlUtils.GetAttributeValue<int>(unitNode, "OxygenLeft");
        }
    }
}