﻿using System;
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
        private float _SpeedMultiplier = 1;
        private float _ArmourMultiplier = 1;
        private float _ViewDistanceMultiplier = 1;
        private float _LightPowerMultiplier = 1;
        private float _LightRangeMultiplier = 1;
        private float _HeavyPowerMultiplier = 1;
        private float _HeavyRangeMultiplier = 1;

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

        public float SpeedMultiplier
        {
            get
            {
                return _SpeedMultiplier;
            }

            set
            {
                _SpeedMultiplier = value;
            }
        }

        public float ArmourMultiplier
        {
            get
            {
                return _ArmourMultiplier;
            }

            set
            {
                _ArmourMultiplier = value;
            }
        }

        public float ViewDistanceMultiplier
        {
            get
            {
                return _ViewDistanceMultiplier;
            }

            set
            {
                _ViewDistanceMultiplier = value;
            }
        }

        public float LightPowerMultiplier
        {
            get
            {
                return _LightPowerMultiplier;
            }

            set
            {
                _LightPowerMultiplier = value;
            }
        }

        public float LightRangeMultiplier
        {
            get
            {
                return _LightRangeMultiplier;
            }

            set
            {
                _LightRangeMultiplier = value;
            }
        }

        public float HeavyPowerMultiplier
        {
            get
            {
                return _HeavyPowerMultiplier;
            }

            set
            {
                _HeavyPowerMultiplier = value;
            }
        }

        public float HeavyRangeMultiplier
        {
            get
            {
                return _HeavyRangeMultiplier;
            }

            set
            {
                _HeavyRangeMultiplier = value;
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
                            if (MapDisplay.PointDifference(new Point(x, y), Units[i].Position) < Units[i].Type.ViewDistance)
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
    }
}