using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public abstract class Unit
    {
        public int Speed;
        public float MovesLeft;
        public Point Position;
        public string Bitmap;
        List<Order> _Abilities = new List<Order>();

        public List<Order> Abilities
        {
            get
            {
                return _Abilities;
            }
        }
    }
}