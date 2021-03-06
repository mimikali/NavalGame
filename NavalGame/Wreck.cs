﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

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
                _TurnsToLive--;
                if (_TurnsToLive <= 0)
                {
                    Game.RemoveUnit(this);
                }
            }
        }


        public override void Save(XElement unitNode)
        {
            base.Save(unitNode);
            unitNode.SetAttributeValue("TurnsToLive", _TurnsToLive);
        }

        public override void Load(XElement unitNode)
        {
            base.Load(unitNode);
            _TurnsToLive = XmlUtils.GetAttributeValue<int>(unitNode, "TurnsToLive");
        }
    }
}