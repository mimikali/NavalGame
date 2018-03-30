using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
                return (IsSubmerged ? "Submerged" : "") + IsDetected;
            }
        }

        public override void ResetProperties(bool initialSetup)
        {
            base.ResetProperties(initialSetup);
            if (IsSubmerged)
                OxygenLeft--;
            else
                OxygenLeft = 15;
            if (OxygenLeft <= 0) IsSubmerged = false;
        }
    }
}