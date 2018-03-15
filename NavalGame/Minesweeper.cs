using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Minesweeper : Unit
    {
        static string StaticBitmap = "Data\\Minesweeper.png";

        public Minesweeper(Point position, Player player)
        {
            Game = player.Game;
            Armour = 2;
            Health = 1;
            LightShots = 0;
            LightShotsLeft = LightShots;
            LightRange = 0;
            LightPower = 0;
            HeavyShots = 0;
            HeavyShotsLeft = HeavyShots;
            HeavyRange = 0;
            HeavyPower = 0;
            ViewDistance = 0;
            Player = player;
            Speed = 3;
            MovesLeft = float.PositiveInfinity;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.Mine });
        }
    }
}