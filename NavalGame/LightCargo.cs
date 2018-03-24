using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class LightCargo : Unit
    {
        static string StaticBitmap = "Data\\Ships\\Miscellaneous\\LightCargo.png";
        static string StaticBitmapLarge = "Data\\Ships\\Miscellaneous\\LightCargoLarge.png";

        public LightCargo(Point position, Player player)
        {
            Game = player.Game;
            Type = UnitType.Wreck;
            Health = 1;
            Armour = 2;
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
            Speed = 5;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            LargeBitmap = StaticBitmapLarge;
            Abilities.AddRange(new List<Order>() { Order.Move });
            Player.NameUnit(this);
        }
    }
}