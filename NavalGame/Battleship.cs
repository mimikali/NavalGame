using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Battleship : Unit
    {
        static string StaticBitmap = "Data\\Battleship.png";

        public Battleship(Point position, Player player)
        {
            Game = player.Game;
            Health = 0;
            Armour = 12;
            LightShots = 4;
            LightShotsLeft = LightShots;
            LightRange = 6;
            LightPower = 2;
            HeavyShots = 3;
            HeavyShotsLeft = HeavyShots;
            HeavyRange = 12;
            HeavyPower = 5;
            ViewDistance = 9;
            Player = player;
            MovesLeft = float.PositiveInfinity;
            Speed = 3;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.HeavyArtillery, Order.LightArtillery });
        }
    }
}