using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    class Destroyer : Unit
    {
        static string StaticBitmap = "Data\\Destroyer.png";

        public Destroyer(Point position, Player player)
        {
            Game = player.Game;
            Type = UnitType.Destroyer;
            Health = 1;
            Armour = 4;
            LightShotsLeft = 1;
            LightRange = 6;
            LightPower = 2;
            HeavyShotsLeft = 1;
            HeavyRange = 0;
            HeavyPower = 0;
            ViewDistance = 9;
            Player = player;
            Speed = 6;
            MovesLeft = float.PositiveInfinity;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.LightArtillery, Order.Torpedo, Order.DepthCharge});
            Player.NameUnit(this);
        }
    }
}
