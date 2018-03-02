using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NavalGame
{
    public class Game
    {
        private List<Unit> _Units;
        private Terrain _Terrain;
        private Unit _SelectedUnit;
        private Dictionary<Order, string> OrderIcons = new Dictionary<Order, string>();
        private Dictionary<Order, string> OrderDescriptions = new Dictionary<Order, string>();

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

        public Game()
        {
            _Terrain = GenerateTerrain(64, 64, 0);
            _Units = new List<Unit>();
            AddUnit(new Destroyer(new Point(5, 6)));
            AddUnit(new Destroyer(new Point(7, 2)));
            AddUnit(new Destroyer(new Point(2, 4)));
            AddUnit(new Destroyer(new Point(7, 7)));
            AddUnit(new Battleship(new Point(2, 8)));
            AddUnit(new Minesweeper(new Point(14, 8)));
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
        }

        public void RemoveUnit(Unit unit)
        {
            if (!Units.Contains(unit)) throw new Exception("Bad unit removal.");
            if (SelectedUnit == unit) SelectedUnit = null;
            _Units.Remove(unit);
        }

        public string GetOrderIconPath(Order order)
        {
            if (!OrderIcons.ContainsKey(order)) return "Data\\Torpedo.png";
            else return OrderIcons[order];
        }
    }
}
