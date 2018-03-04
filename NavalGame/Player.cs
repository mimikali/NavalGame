using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Player
    {
        private List<bool> VisibleTiles;
        private List<Unit> _Units = new List<Unit>();
        private bool IsVisibilityValid = false;
        private bool AreUnitsValid = false;
        private Game _Game;

        public List<Unit> Units
        {
            get
            {
                if (AreUnitsValid)
                {
                    return _Units;
                }
                else
                {
                    _Units.Clear();
                    for (int i = 0; i < Game.Units.Count; i++)
                    {
                        if (Game.Units[i].Player == this) _Units.Add(Game.Units[i]);
                    }
                    return _Units;
                }
            }

            set
            {
                _Units = value;
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
            VisibleTiles = new List<bool>();
            Units = new List<Unit>();
            Game.Change += GameChange;
        }

        private void GameChange(Game game, EventArgs e)
        {
            IsVisibilityValid = false;
            AreUnitsValid = false;
        }

        public bool IsTileVisible(Point position)
        {
            if (IsVisibilityValid)
            {
                return VisibleTiles[position.X + position.Y * Game.Terrain.Width];
            }
            else
            {
                VisibleTiles.Clear();
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
                        VisibleTiles.Add(a);
                    }
                }
                IsVisibilityValid = true;
                return VisibleTiles[position.X + position.Y * Game.Terrain.Width];
            }
        }
    }
}