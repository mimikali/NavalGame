using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Port : Unit
    {
        static string StaticBitmap = "Data\\Ships\\Miscellaneous\\Port.png";
        static string StaticBitmapLarge = "Data\\Ships\\Miscellaneous\\PortLarge.png";

        public Port(Point position, Player player)
        {
            Game = player.Game;
            Type = UnitType.Wreck;
            Health = 1;
            Armour = 100f;
            LightShotsLeft = 0;
            LightRange = 0;
            LightPower = 0;
            HeavyShotsLeft = 0;
            HeavyRange = 0;
            HeavyPower = 0;
            RepairPower = 0.3f;
            RepairsLeft = 1;
            ViewDistance = 10;
            Player = player;
            MovesLeft = float.PositiveInfinity;
            Speed = 0;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            LargeBitmap = StaticBitmapLarge;
            Abilities.AddRange(new List<Order>() { Order.Repair });
            Player.NameUnit(this);
        }
    }
}