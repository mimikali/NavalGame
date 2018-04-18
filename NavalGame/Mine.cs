using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

namespace NavalGame
{
    public class Mine
    {
        Game _Game;
        Point _Position;
        Faction _Faction;
        bool _IsVisible;

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

        public Point Position
        {
            get
            {
                return _Position;
            }

            set
            {
                _Position = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return _IsVisible;
            }

            set
            {
                _IsVisible = value;
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

        public Mine(Faction faction, Point position, Game game)
        {
            _Position = position;
            _Faction = faction;
            Game = game;
            game.Changed += OnGameChanged;
        }

        public static XElement Save(Mine mine)
        {
            XElement mineNode = new XElement("mineNode");

            mineNode.SetAttributeValue("Position", string.Format("{0}, {1}", mine.Position.X, mine.Position.Y));
            mineNode.SetAttributeValue("Faction", mine.Faction.ToString());
            mineNode.SetAttributeValue("IsVisible", mine.IsVisible);

            return mineNode;
        }

        public static Mine Load(XElement mineNode, Game game)
        {
            Point position = XmlUtils.GetAttributeValue<Point>(mineNode, "Position");
            Faction faction = XmlUtils.GetAttributeValue<Faction>(mineNode, "Faction");
            bool isVisible = XmlUtils.GetAttributeValue<bool>(mineNode, "IsVisible");

            Mine mine = new Mine(faction, position, game);
            mine.IsVisible = isVisible;

            return mine;
        }

        public void OnGameChanged()
        {
            bool isVisible = IsVisible;

            if (!IsVisible && new Random(Game.TurnIndex * GetHashCode()).NextDouble() < 0.25)
            {
                isVisible = Game.Units.Any(u => MapDisplay.PointDifference(u.Position, Position) <= 2 && u.Player.Faction != Faction);
            }

            IsVisible = isVisible;
        }
    }
}