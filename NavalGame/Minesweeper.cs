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
            ViewDistance = 6;
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