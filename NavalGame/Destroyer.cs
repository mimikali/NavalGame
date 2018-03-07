using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NavalGame
{
    class Destroyer : Unit
    {
        static string StaticBitmap = "Data\\Destroyer.png";

        public Destroyer(Point position, Player player)
        {
            Game = player.Game;
            ViewDistance = 9;
            Player = player;
            Speed = 5;
            MovesLeft = float.PositiveInfinity;
            Position = position;
            MovesLeft = Speed;
            Bitmap = StaticBitmap;
            Abilities.AddRange(new List<Order>() { Order.Move, Order.LightArtillery, Order.Torpedo, Order.DepthCharge});
            MovesLeft = Speed;
        }
    }
}
