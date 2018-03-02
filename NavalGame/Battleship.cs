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

        public Battleship(Point position)
        {
            MovesLeft = float.PositiveInfinity;
            Speed = 3;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.HeavyArtillery, Order.LightArtillery });
        }
    }
}