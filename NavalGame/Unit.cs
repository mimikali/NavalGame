using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public abstract class Unit
    {
        private int _Speed;
        private float _MovesLeft;
        private Point _Position;
        private string _Bitmap;
        List<Order> _Abilities = new List<Order>();

        public List<Order> Abilities
        {
            get
            {
                return _Abilities;
            }
        }

        public int Speed
        {
            get
            {
                return _Speed;
            }

            set
            {
                _Speed = value;
            }
        }

        public float MovesLeft
        {
            get
            {
                return _MovesLeft;
            }

            set
            {
                _MovesLeft = value;
            }
        }

        public Point Position
        {
            get
            {
                return _Position;
            }

            set
            {
                if (MapDisplay.PointDifference(_Position, value) <= MovesLeft)
                {
                    MovesLeft -= MapDisplay.PointDifference(_Position, value);
                    _Position = value;
                }
                else
                {
                    throw new Exception("Illegal move");
                }
            }
        }

        public string Bitmap
        {
            get
            {
                return _Bitmap;
            }

            set
            {
                _Bitmap = value;
            }
        }
    }
}