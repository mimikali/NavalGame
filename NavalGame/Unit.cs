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
        private Player _Player;
        private float _ViewDistance;
        private Game _Game;
        private int _LightShots;
        private int _LightShotsLeft;
        private float _LightRange;
        private int _HeavyShots;
        private int _HeavyShotsLeft;
        private float _HeavyRange;

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
                    Game.FireChangedEvent();
                }
                else
                {
                    throw new Exception("Illegal move.");
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

        public Player Player
        {
            get
            {
                return _Player;
            }

            set
            {
                _Player = value;
                Game.FireChangedEvent();
            }
        }

        public float ViewDistance
        {
            get
            {
                return _ViewDistance;
            }

            set
            {
                _ViewDistance = value;
                Game.FireChangedEvent();
            }
        }

        public Game Game
        {
            get
            {
                return _Game;
            }

            set
            {
                _Game = value;
                Game.FireChangedEvent();
            }
        }

        public int LightShots
        {
            get
            {
                return _LightShots;
            }

            set
            {
                _LightShots = value;
                Game.FireChangedEvent();
            }
        }

        public int LightShotsLeft
        {
            get
            {
                return _LightShotsLeft;
            }

            set
            {
                _LightShotsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public float LightRange
        {
            get
            {
                return _LightRange;
            }

            set
            {
                _LightRange = value;
                Game.FireChangedEvent();
            }
        }

        public int HeavyShots
        {
            get
            {
                return _HeavyShots;
            }

            set
            {
                _HeavyShots = value;
                Game.FireChangedEvent();
            }
        }

        public int HeavyShotsLeft
        {
            get
            {
                return _HeavyShotsLeft;
            }

            set
            {
                _HeavyShotsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public float HeavyRange
        {
            get
            {
                return _HeavyRange;
            }

            set
            {
                _HeavyRange = value;
                Game.FireChangedEvent();
            }
        }

        public void NextMove()
        {
            MovesLeft = Speed;
            LightShotsLeft = LightShots;
            HeavyShotsLeft = HeavyShots;
        }
    }
}