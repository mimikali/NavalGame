using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Wreck : Unit
    {
        static string StaticBitmap = "Data\\Ships\\Miscellaneous\\Wreck.png";
        static string StaticBitmapLarge = "Data\\Ships\\Miscellaneous\\WreckLarge.png";

        public Wreck(Point position, Player player, string name)
        {
            Game = player.Game;
            Type = UnitType.Wreck;
            Health = 1;
            Armour = 0.01f;
            LightShotsLeft = 0;
            LightRange = 0;
            LightPower = 0;
            HeavyShotsLeft = 0;
            HeavyRange = 0;
            HeavyPower = 0;
            RepairPower = 0;
            RepairsLeft = 0;
            ViewDistance = 1;
            Player = player;
            MovesLeft = float.PositiveInfinity;
            Speed = 0;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            LargeBitmap = StaticBitmapLarge;
            Abilities.AddRange(new List<Order>() { });
            Name = name;
        }
    }
}