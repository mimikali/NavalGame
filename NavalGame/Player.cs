using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Player
    {
        private List<bool> _VisibleTiles;
        private List<Unit> _Units = new List<Unit>();
        private Game _Game;
        private Faction _Faction;
        private List<string> _UnitNames;
        static int _NameIteration;

        public IList<Unit> Units
        {
            get
            {
                if (_Units == null)
                {
                    _Units = new List<Unit>();
                    for (int i = 0; i < Game.Units.Count; i++)
                    {
                        if (Game.Units[i].Player == this) _Units.Add(Game.Units[i]);
                    }
                }
                return _Units.AsReadOnly();
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
            }
        }

        public Faction Faction
        {
            get
            {
                return _Faction;
            }

            set
            {
                _Faction = value;
            }
        }

        public List<string> UnitNames
        {
            get
            {
                return _UnitNames;
            }

            set
            {
                _UnitNames = value;
            }
        }

        public Player(Game game, Faction faction)
        {
            Game = game;
            Faction = faction;
            _VisibleTiles = new List<bool>();
            _Units = new List<Unit>();
            Game.Changed += GameChanged;
        }

        private void GameChanged()
        {
            _VisibleTiles = null;
            _Units = null;
        }

        public bool IsTileVisible(Point position)
        {
            if (_VisibleTiles == null)
            {
                _VisibleTiles = new List<bool>();
                for (int y = 0; y < Game.Terrain.Height; y++)
                {
                    for (int x = 0; x < Game.Terrain.Width; x++)
                    {
                        bool a = false;
                        for (int i = 0; i < Units.Count; i++)
                        {
                            if (MapDisplay.PointDifference(new Point(x, y), Units[i].Position) < Units[i].ViewDistance)
                            {
                                a = true;
                                break;
                            }
                        }
                        _VisibleTiles.Add(a);
                    }
                }
            }
            return _VisibleTiles[position.X + position.Y * Game.Terrain.Width];
        }

        public void NameUnit(Unit unit)
        {
            //switch (unit.Player.Faction)
            //{
            //    case Faction.USA:
            //        switch (unit.Type)
            //        {
            //            case UnitType.Destroyer:
            //                while (Units.Any(i => i.Name == unit.Name))
            //                {
            //                    unit.Name = UnitNames[0 * Enum.GetNames(typeof(Faction)).Length * Enum.GetNames(typeof(UnitType)).Length * 3 + 0 * Enum.GetNames(typeof(UnitType)).Length * 3 + _NameIteration];
            //                    if (_NameIteration == 2) _NameIteration = 0;
            //                    else _NameIteration++;
            //                }
            //                break;

            //            case UnitType.Battleship:
            //                unit.Name = UnitNames[0 * Enum.GetNames(typeof(Faction)).Length * Enum.GetNames(typeof(UnitType)).Length * 3 + 1 * Enum.GetNames(typeof(UnitType)).Length * 3 + _NameIteration];
            //                if (_NameIteration == 2) _NameIteration = 0;
            //                else _NameIteration++;
            //                break;

            //            case UnitType.Minesweeper:
            //                unit.Name = UnitNames[0 * Enum.GetNames(typeof(Faction)).Length * Enum.GetNames(typeof(UnitType)).Length * 3 + 2 * Enum.GetNames(typeof(UnitType)).Length * 3 + _NameIteration];
            //                if (_NameIteration == 2) _NameIteration = 0;
            //                else _NameIteration++;
            //                break;

            //            default:
            //                unit.Name = "Unnamed unit";
            //                break;
            //        }
            //        break;
            //}

            unit.Name = "Unit";
        }
    }
}