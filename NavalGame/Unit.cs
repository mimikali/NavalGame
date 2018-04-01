using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

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
        private int _DepthChargesLeft;
        private int _TurnsUntilCompletion;
        private int _Cargo;
        private int _Torpedoes;
        private int _LoadsLeft;
        private int _TorpedoesLeft;
        private int _DivesLeft;
        private int _InstallsLeft;
        private int _CapturesLeft;

        protected Unit(UnitType type, Player player, Point position)
        {
            _Type = type;
            _Player = player;
            _Position = position;
            _IsDetected = true;
            _Torpedoes = Type.MaxTorpedoes;
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
                Player.Game.FireChangedEvent();
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
                Player.Game.FireChangedEvent();
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
                Player.Game.FireChangedEvent();
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
                Player.Game.FireChangedEvent();
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
                return Health < 1 ? (Health * 100).ToString("0") + "%" : "";
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
                Game.FireChangedEvent();
            }
        }

        public int DepthChargesLeft
        {
            get
            {
                return _DepthChargesLeft;
            }

            set
            {
                _DepthChargesLeft = value;
                Game.FireChangedEvent();
            }
        }

        public int InstallsLeft
        {
            get
            {
                return _InstallsLeft;
            }

            set
            {
                _InstallsLeft = value;
            }
        }

        public int CapturesLeft
        {
            get
            {
                return _CapturesLeft;
            }

            set
            {
                _CapturesLeft = value;
            }
        }

        public bool Move(Point destination)
        {
            var distance = MapDisplay.PointDifference(_Position, destination);
            if (distance <= MovesLeft)
            {
                Unit unit = Game.GetUnitAt(destination);
                if (unit == null)
                {
                    MovesLeft -= distance;
                    _Position = destination;
                    return true;
                }
                else
                {
                    if (!unit.IsDetected && unit.IsSubmerged) Game.FireSubmarineDetectedEvent();
                    unit.IsDetected = true;
                }
            }
            return false;
        }

        public void DiveOrSurface()
        {
            if (DivesLeft >= 1)
            {
                IsSubmerged = !IsSubmerged;
                DivesLeft--;
            }

            if (IsSubmerged) MovesLeft = Math.Min(MovesLeft, Type.SubmergedSpeed);
        }

        public virtual void ResetProperties(bool initialSetup)
        {
            MovesLeft = IsSubmerged ? Type.SubmergedSpeed : Type.Speed;
            if (Health < 0.6) MovesLeft = (float)Math.Round(Math.Max(1, MovesLeft * 0.7f));
            LightShotsLeft = 1;
            HeavyShotsLeft = 1;
            RepairsLeft = 1;
            LoadsLeft = 1;
            TorpedoesLeft = 1;
            DivesLeft = 1;
            DepthChargesLeft = 1;
            CapturesLeft = 1;

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
                Game.FireSinkingEvent();
            }

            bool isDetected = IsDetected;
            if (!IsSubmerged)
            {
                isDetected = true;
            }
            else if (!IsDetected && new Random(Game.TurnIndex * GetHashCode()).NextDouble() < 0.5)
            {
                isDetected = Game.Units.Any(u => MapDisplay.PointDifference(u.Position, Position) <= u.Type.SonarRange && u.Player.Faction != Player.Faction);
            }
            if (IsDetected != isDetected && isDetected && IsSubmerged) Game.FireSubmarineDetectedEvent(); 
            IsDetected = isDetected;
        }

        public virtual void Save(XElement unitNode)
        {
            unitNode.SetAttributeValue("MovesLeft", MovesLeft);
            unitNode.SetAttributeValue("LightShotsLeft", LightShotsLeft);
            unitNode.SetAttributeValue("HeavyShotsLeft", HeavyShotsLeft);
            unitNode.SetAttributeValue("TorpedoesLeft", TorpedoesLeft);
            unitNode.SetAttributeValue("DivesLeft", DivesLeft);
            unitNode.SetAttributeValue("LoadsLeft", LoadsLeft);
            unitNode.SetAttributeValue("DepthChargesLeft", DepthChargesLeft);
            unitNode.SetAttributeValue("InstallsLeft", InstallsLeft);
            unitNode.SetAttributeValue("CapturesLeft", CapturesLeft);
            unitNode.SetAttributeValue("RepairsLeft", RepairsLeft);

            unitNode.SetAttributeValue("Name", Name);
            unitNode.SetAttributeValue("Position", string.Format("{0}, {1}", Position.X, Position.Y));
            unitNode.SetAttributeValue("Health", Health);
            unitNode.SetAttributeValue("IsSubmerged", IsSubmerged);
            unitNode.SetAttributeValue("IsDetected", IsDetected);
            unitNode.SetAttributeValue("Cargo", Cargo);
            unitNode.SetAttributeValue("Torpedoes", Torpedoes);
            unitNode.SetAttributeValue("TurnsUntilCompletion", TurnsUntilCompletion);
        }

        public virtual void Load(XElement unitNode)
        {
            MovesLeft = XmlUtils.GetAttributeValue<float>(unitNode, "MovesLeft");
            LightShotsLeft = XmlUtils.GetAttributeValue<int>(unitNode, "LightShotsLeft");
            HeavyShotsLeft = XmlUtils.GetAttributeValue<int>(unitNode, "HeavyShotsLeft");
            TorpedoesLeft = XmlUtils.GetAttributeValue<int>(unitNode, "TorpedoesLeft");
            DivesLeft = XmlUtils.GetAttributeValue<int>(unitNode, "DivesLeft");
            LoadsLeft = XmlUtils.GetAttributeValue<int>(unitNode, "LoadsLeft");
            DepthChargesLeft = XmlUtils.GetAttributeValue<int>(unitNode, "DepthChargesLeft");
            InstallsLeft = XmlUtils.GetAttributeValue<int>(unitNode, "InstallsLeft");
            CapturesLeft = XmlUtils.GetAttributeValue<int>(unitNode, "CapturesLeft");
            RepairsLeft = XmlUtils.GetAttributeValue<int>(unitNode, "RepairsLeft");

            Name = XmlUtils.GetAttributeValue<string>(unitNode, "Name");
            Position = XmlUtils.GetAttributeValue<Point>(unitNode, "Position");
            Health = XmlUtils.GetAttributeValue<float>(unitNode, "Health");
            IsSubmerged = XmlUtils.GetAttributeValue<bool>(unitNode, "IsSubmerged");
            IsDetected = XmlUtils.GetAttributeValue<bool>(unitNode, "IsDetected");
            Cargo = XmlUtils.GetAttributeValue<int>(unitNode, "Cargo");
            Torpedoes = XmlUtils.GetAttributeValue<int>(unitNode, "Torpedoes");
            TurnsUntilCompletion = XmlUtils.GetAttributeValue<int>(unitNode, "TurnsUntilCompletion");
        }
    }
}