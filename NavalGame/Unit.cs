using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public abstract class Unit
    {
        private UnitType _Type;
        private Player _Player;
        private string _Name = "Unit";
        private Point _Position;
        private bool _IsSubmerged;
        private bool _IsDetected;
        private float _Health = 1;
        private float _MovesLeft;
        private int _LightShotsLeft;
        private int _HeavyShotsLeft;
        private int _RepairsLeft;
        private int _BuildsLeft;
        private int _TurnsUntilCompletion;
        private int _Cargo;
        private int _Torpedoes;
        private int _LoadsLeft;
        private int _TorpedoesLeft;
        private int _DivesLeft;

        protected Unit(UnitType type, Player player, Point position)
        {
            _Type = type;
            _Player = player;
            _Position = position;
            _IsDetected = true;
            Name = player.GetUnitName(this);
            ResetProperties(true);
        }

        public Game Game
        {
            get
            {
                return _Player.Game;
            }
        }

        public UnitType Type
        {
            get
            {
                return _Type;
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

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                Game.FireChangedEvent();
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
                _Position = value;
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

        public float MovesLeft
        {
            get
            {
                return _MovesLeft;
            }
            set
            {
                _MovesLeft = value;
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

        public int RepairsLeft
        {
            get
            {
                return _RepairsLeft;
            }

            set
            {
                _RepairsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public int BuildsLeft
        {
            get
            {
                return _BuildsLeft;
            }

            set
            {
                _BuildsLeft = value;
                Game.FireChangedEvent();
            }
        }

        public int TurnsUntilCompletion
        {
            get
            {
                return _TurnsUntilCompletion;
            }

            set
            {
                _TurnsUntilCompletion = value;
                Game.FireChangedEvent();
            }
        }

        public int Cargo
        {
            get
            {
                return _Cargo;
            }

            set
            {
                _Cargo = Math.Min(Type.Capacity, Math.Max(0, value));
            }
        }

        public int LoadsLeft
        {
            get
            {
                return _LoadsLeft;
            }

            set
            {
                _LoadsLeft = value;
            }
        }

        public int TorpedoesLeft
        {
            get
            {
                return _TorpedoesLeft;
            }

            set
            {
                _TorpedoesLeft = value;
            }
        }

        public int DivesLeft
        {
            get
            {
                return _DivesLeft;
            }

            set
            {
                _DivesLeft = value;
            }
        }

        public bool IsSubmerged
        {
            get
            {
                return _IsSubmerged;
            }

            set
            {
                _IsSubmerged = value;
                IsDetected = !value;
                Player.Game.FireChangedEvent();
            }
        }

        public virtual string Information
        {
            get
            {
                return "";
            }
        }

        public virtual bool IsDetected
        {
            get
            {
                return _IsDetected;
            }

            set
            {
                if (value != _IsDetected)
                {
                    _IsDetected = value;
                    Game.FireChangedEvent();
                }
            }
        }

        public int Torpedoes
        {
            get
            {
                return _Torpedoes;
            }

            set
            {
                _Torpedoes = value;
            }
        }

        public void Move(Point destination)
        {
            var distance = MapDisplay.PointDifference(_Position, destination);
            if (distance <= MovesLeft)
            {
                Unit unit = Game.GetUnitAt(destination);
                if (unit == null)
                {
                    MovesLeft -= distance;
                    _Position = destination;
                }
                else
                {
                    unit.IsDetected = true;
                }
            }
            else
            {
                throw new Exception("Illegal move.");
            }
        }

        public void DiveOrSurface()
        {
            if (DivesLeft >= 1)
            {
                IsSubmerged = !IsSubmerged;
                DivesLeft--;
            }
        }

        public virtual void ResetProperties(bool initialSetup)
        {
            MovesLeft = Type.Speed;
            LightShotsLeft = 1;
            HeavyShotsLeft = 1;
            RepairsLeft = 1;
            BuildsLeft = 1;
            LoadsLeft = 1;
            TorpedoesLeft = 1;
            DivesLeft = 1;

            if (IsSubmerged && new Random(GetHashCode()).NextDouble() <= 0.7)
            {
                IsDetected = false;
            }
        }

        public virtual void OnGameChanged()
        {
            if (Health <= 0)
            {
                Game.RemoveUnit(this);
                Wreck wreck = new Wreck(Player, Position);
                wreck.Name = Name;
                Game.AddUnit(wreck);
            }

            bool isDetected = IsDetected;
            if (!IsSubmerged)
            {
                isDetected = true;
            }
            else if (!IsDetected && new Random(Game.TurnIndex * GetHashCode()).NextDouble() < 0.3)
            {
                isDetected = Game.Units.Any(u => MapDisplay.PointDifference(u.Position, Position) <= u.Type.SonarRange && u.Player.Faction != Player.Faction);
            }
            IsDetected = isDetected;
        }
    }
}