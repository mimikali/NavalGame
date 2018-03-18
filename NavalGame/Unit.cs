using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public abstract class Unit
    {
        private int _Speed = 0;
        private float _MovesLeft = 0;
        private Point _Position = new Point(0, 0);
        private string _Bitmap = "";
        List<Order> _Abilities = new List<Order>();
        private Player _Player = null;
        private float _ViewDistance = 0;
        private Game _Game = null;
        private int _LightShots = 0;
        private int _LightShotsLeft = 0;
        private float _LightRange = 0;
        private float _LightPower = 0;
        private int _HeavyShots = 0;
        private int _HeavyShotsLeft = 0;
        private float _HeavyRange = 0;
        private float _HeavyPower = 0;
        private float _Health = 1;
        private float _Armour = 0;
        private string _Name = "Unit";
        private UnitType _Type = UnitType.Destroyer;

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

        public float Health
        {
            get
            {
                return _Health;
            }

            set
            {
                if (value < 0) _Health = 0;
                else _Health = value;
                Game.FireChangedEvent();
            }
        }

        public float Armour
        {
            get
            {
                return _Armour;
            }

            set
            {
                _Armour = value;
            }
        }

        public float HeavyPower
        {
            get
            {
                return _HeavyPower;
            }

            set
            {
                _HeavyPower = value;
            }
        }

        public float LightPower
        {
            get
            {
                return _LightPower;
            }

            set
            {
                _LightPower = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        public UnitType Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
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