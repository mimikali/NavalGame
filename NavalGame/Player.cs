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

        public Player(Game game)
        {
            Game = game;
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
    }
}