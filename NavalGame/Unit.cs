using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public abstract class Unit
    {
        private float _Speed = 0;
        private float _MovesLeft = 0;
        private Point _Position = new Point(0, 0);
        private string _Bitmap = "";
        List<Order> _Abilities = new List<Order>();
        private Player _Player = null;
        private float _ViewDistance = 0;
        private Game _Game = null;
        private int _LightShotsLeft = 0;
        private float _LightRange = 0;
        private float _LightPower = 0;
        private int _HeavyShotsLeft = 0;
        private float _HeavyRange = 0;
        private float _HeavyPower = 0;
        private int _RepairsLeft = 0;
        private float _RepairPower = 0;
        private float _Health = 1;
        private float _Armour = 0;
        private string _Name = "Unit";
        private string _LargeBitmap = "";
        private UnitType _Type = UnitType.Destroyer;

        public List<Order> Abilities
        {
            get
            {
                return _Abilities;
            }
        }

        public float Speed
        {
            get
            {
                return (int)(_Speed * Player.SpeedMultiplier);
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
                return _ViewDistance * Player.ViewDistanceMultiplier;
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

        public int LightShotsLeft
        {
            get
            {
                return _LightShotsLeft;
            }

            set
            {
                if (value < 0)
                {
                    throw new Exception("Illegal shot.");
                }
                _LightShotsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public float LightRange
        {
            get
            {
                return _LightRange * Player.LightRangeMultiplier;
            }

            set
            {
                _LightRange = value;
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
                if (value < 0)
                {
                    throw new Exception("Illegal shot.");
                }
                _HeavyShotsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public float HeavyRange
        {
            get
            {
                return _HeavyRange * Player.HeavyRangeMultiplier;
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
                else if (value > 1) _Health = 1;
                else _Health = value;
                Game.FireChangedEvent();
            }
        }

        public float Armour
        {
            get
            {
                return _Armour * Player.ArmourMultiplier;
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
                return _HeavyPower * Player.HeavyPowerMultiplier;
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
                return _LightPower * Player.LightPowerMultiplier;
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

        public string LargeBitmap
        {
            get
            {
                return _LargeBitmap;
            }

            set
            {
                _LargeBitmap = value;
            }
        }

        public float RepairPower
        {
            get
            {
                return _RepairPower;
            }

            set
            {
                _RepairPower = value;
            }
        }

        public int RepairsLeft
        {
            get
            {
                return _RepairsLeft;
            }

            set
            {
                _RepairsLeft = value;
            }
        }

        public void NextMove()
        {
            MovesLeft = Speed;
            LightShotsLeft = 1;
            HeavyShotsLeft = 1;
            RepairsLeft = 1;
        }
    }
}