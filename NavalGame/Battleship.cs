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
            Speed = 3;
            Position = position;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.HeavyArtillery, Order.LightArtillery });
        }
    }
}