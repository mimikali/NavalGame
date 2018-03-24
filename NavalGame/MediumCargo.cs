using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class MediumCargo : Unit
    {
        static string StaticBitmap = "Data\\Ships\\Miscellaneous\\MediumCargo.png";
        static string StaticBitmapLarge = "Data\\Ships\\Miscellaneous\\MediumCargoLarge.png";

        public MediumCargo(Point position, Player player)
        {
            Game = player.Game;
            Type = UnitType.MediumCargo;
            Health = 1;
            Armour = 3;
            LightShotsLeft = 0;
            LightRange = 0;
            LightPower = 0;
            HeavyShotsLeft = 0;
            HeavyRange = 0;
            HeavyPower = 0;
            RepairPower = 0;
            RepairsLeft = 0;
            ViewDistance = 8;
            Player = player;
            MovesLeft = float.PositiveInfinity;
            Speed = 3;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            LargeBitmap = StaticBitmapLarge;
            Abilities.AddRange(new List<Order>() { Order.Move });
            Player.NameUnit(this);
        }
    }
}