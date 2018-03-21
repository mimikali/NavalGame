using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;

namespace NavalGame
{
    public class Game
    {
        private List<Unit> _Units;
        private Terrain _Terrain;
        private Unit _SelectedUnit;
        private Dictionary<Order, string> OrderIcons = new Dictionary<Order, string>();
        private Dictionary<Order, string> OrderDescriptions = new Dictionary<Order, string>();
        private List<Player> _Players = new List<Player>();
        private Player _CurrentPlayer;
        public event Action Changed;
        public Random Random;

        public Unit SelectedUnit
        {
            get
            {
                return _SelectedUnit;
            }

            set
            {
                if (value == null || Units.Contains(value))
                {
                    _SelectedUnit = value;
                    if (Changed != null) Changed();
                }
                else
                {
                    throw new Exception("Bad unit selection.");
                }
            }
        }

        public IList<Unit> Units
        {
            get
            {
                return _Units.AsReadOnly();
            }
        }

        public Terrain Terrain
        {
            get
            {
                return _Terrain;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return _CurrentPlayer;
            }

            set
            {
                if (value != null && !Players.Contains(value)) throw new Exception("Invalid current player.");
                _CurrentPlayer = value;
                SelectedUnit = null;
                if (value != null)
                {
                    foreach (Unit unit in value.Units)
                    {
                        unit.NextMove();
                    }
                }
                if (Changed != null) Changed();
            }
        }

        public IList<Player> Players
        {
            get
            {
                return _Players.AsReadOnly();
            }
        }

        public Game()
        {
            Random = new Random();
            _Terrain = GenerateTerrain(96, 96, 675);
            _Units = new List<Unit>();
            _Players.Add(new Player(this, Faction.Japan));
            _Players.Add(new Player(this, Faction.Germany));
            _Players.Add(new Player(this, Faction.England));
            _Players.Add(new Player(this, Faction.USA));
            AddUnit(new Battleship(new Point(9, 12), Players[0]));
            AddUnit(new Battleship(new Point(12, 12), Players[1]));
            AddUnit(new Battleship(new Point(9, 9), Players[2]));
            AddUnit(new Battleship(new Point(12, 9), Players[3]));
            OrderIcons.Add(Order.Torpedo, "Data\\Torpedo.png");
            OrderIcons.Add(Order.Mine, "Data\\Mine.png");
            OrderIcons.Add(Order.Move, "Data\\Move.png");
            OrderIcons.Add(Order.DepthCharge, "Data\\DepthCharge.png");
            OrderIcons.Add(Order.LightArtillery, "Data\\LightArtillery.png");
            OrderIcons.Add(Order.HeavyArtillery, "Data\\HeavyArtillery.png");
        }

        static Terrain GenerateTerrain(int width, int height, int seed)
        {
            Random random = new Random(seed);
            Terrain terrain = new Terrain(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double chance = 0.03;
                    if (terrain.Get(x - 1, y) == TerrainType.Land) chance = chance + 0.45;
                    if (terrain.Get(x, y - 1) == TerrainType.Land) chance = chance + 0.45;
                    if (random.NextDouble() < chance) terrain.Set(x, y, TerrainType.Land);
                }
            }

            return terrain;
        }

        public void AddUnit(Unit unit)
        {
            if (Units.Contains(unit)) throw new Exception("Bad unit addition.");
            _Units.Add(unit);
            if (Changed != null) Changed();
        }

        public void RemoveUnit(Unit unit)
        {
            if (!Units.Contains(unit)) throw new Exception("Bad unit removal.");
            if (SelectedUnit == unit) SelectedUnit = null;
            _Units.Remove(unit);
            if (Changed != null) Changed();
        }

        public string GetOrderIconPath(Order order)
        {
            if (!OrderIcons.ContainsKey(order)) return "Data\\Torpedo.png";
            else return OrderIcons[order];
        }

        public void FireChangedEvent()
        {
            _Units.RemoveAll(unit => unit.Health <= 0);
            if (Changed != null) Changed();
        }

        public int LightArtillery(Point target, Unit shooter)
        {
            // Use a shot
            shooter.LightShotsLeft -= 1;

            // Determine whether there is a target unit
            Unit targetUnit = null;
            foreach(Unit unit in Units)
            {
                if (unit.Position == target)
                {
                    targetUnit = unit;
                    break;
                }
            }

            // Return as no hit if there is no target unit
            if (targetUnit == null)
            {
                return 0;
            }

            // Calculate the chances of a hit
            List<int> chances = new List<int>();
            for (int i = 0; i < MapDisplay.PointDifference(shooter.Position, target); i++)
            {
                chances.Add(0);
            }

            for (int i = 0; i < targetUnit.Speed; i++)
            {
                chances.Add(0);
            }

            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(2);

            // Choose if the shot missed, hit, or critically hit
            int result = chances[Random.Next(0, chances.Count)];

            // Calculate damage
            float basedamage = shooter.LightPower / targetUnit.Armour;
            float randomVariation = (float)((Random.NextDouble() * 2 - 1) * (basedamage / 3));

            // Take action according to the shot
            switch (result)
            {
                case 0:
                    return 0;
                case 1:
                    targetUnit.Health -= basedamage + randomVariation;
                    return (int)((basedamage + randomVariation) * 100);
                case 2:
                    targetUnit.Health -= 2 * basedamage + randomVariation;
                    return (int)((2 * basedamage + randomVariation) * 100);
            }
            return 0;
        }

        public int HeavyArtillery(Point target, Unit shooter)
        {
            // Use a shot
            shooter.HeavyShotsLeft -= 1;

            // Determine whether there is a target unit
            Unit targetUnit = null;
            foreach (Unit unit in Units)
            {
                if (unit.Position == target)
                {
                    targetUnit = unit;
                    break;
                }
            }

            // Return as no hit if there is no target unit
            if (targetUnit == null)
            {
                return 0;
            }

            // Calculate the chances of a hit
            List<int> chances = new List<int>();
            for (int i = 0; i < MapDisplay.PointDifference(shooter.Position, target); i++)
            {
                chances.Add(0);
            }

            for (int i = 0; i < targetUnit.Speed; i++)
            {
                chances.Add(0);
            }

            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(2);

            // Choose if the shot missed, hit, or critically hit
            int result = chances[Random.Next(0, chances.Count)];

            // Calculate damage
            float basedamage = shooter.HeavyPower / targetUnit.Armour;
            float randomVariation = (float)((Random.NextDouble() * 2 - 1) * (basedamage / 3));

            // Take action according to the shot
            switch (result)
            {
                case 0:
                    return 0;
                case 1:
                    targetUnit.Health -= basedamage + randomVariation;
                    return (int)((basedamage + randomVariation) * 100);
                case 2:
                    targetUnit.Health -= 2 * basedamage + randomVariation;
                    return (int)((2 * basedamage + randomVariation) * 100);
            }
            return 0;

        }
    }
}
