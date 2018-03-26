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
        private List<Player> _Players = new List<Player>();
        private Player _CurrentPlayer;
        public event Action Changed;
        public Random Random = new Random();

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
                        unit.ResetProperties(false);
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

        public Game(Terrain terrain)
        {
            //_Terrain = GenerateTerrain(20, 20, 1);
            _Terrain = terrain;
            _Units = new List<Unit>();
            _Players.Add(new Player(this, Faction.Japan));
            _Players.Add(new Player(this, Faction.USA));
            _Players.Add(new Player(this, Faction.Germany));
            _Players.Add(new Player(this, Faction.England));
            _Players.Add(new Player(this, Faction.Neutral));
            AddUnit(new Port(Players[0], new Point(10, 12)));
            AddUnit(new MediumCargo(Players[0], new Point(9, 12)));
            AddUnit(new MediumCargo(Players[1], new Point(9, 13)));
            AddUnit(new Factory(Players[4], new Point(10, 13)));
            //AddUnit(new Port(new Point(9, 9), Players[0]));
        }

        public static Terrain GenerateTerrain(int width, int height, int seed)
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

        public void FireChangedEvent()
        {
            List<Unit> toRemove = new List<Unit>();
            List<Unit> toBuild = new List<Unit>();

            foreach(Unit unit in Units)
            {
                if (unit.Health <= 0)
                {
                    toRemove.Add(unit);
                }
                if (unit.GetType() == typeof(ShipInProgress) && unit.TurnsUntilCompletion <= 0)
                {
                    toBuild.Add(unit);
                }
            }

            foreach(Unit unit in toRemove)
            {
                _Units.Remove(unit);
                Wreck wreck = new Wreck(unit.Player, unit.Position);
                wreck.Name = unit.Name;
                _Units.Add(wreck);
            }
            foreach(ShipInProgress unit in toBuild)
            {
                _Units.Remove(unit);
                Unit newUnit = unit.ShipType.CreateUnit(unit.Player, unit.Position);
                newUnit.Name = unit.Name;
                _Units.Add(newUnit);
            }
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

            for (int i = 0; i < targetUnit.Type.Speed; i++)
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
            chances.Add(1);
            chances.Add(1);
            chances.Add(2);

            // Choose if the shot missed, hit, or critically hit
            int result = chances[Random.Next(0, chances.Count)];

            // Calculate damage
            float basedamage = shooter.Type.LightPower / targetUnit.Type.Armour;
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

            for (int i = 0; i < targetUnit.Type.Speed; i++)
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
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(1);
            chances.Add(2);

            // Choose if the shot missed, hit, or critically hit
            int result = chances[Random.Next(0, chances.Count)];

            // Calculate damage
            float basedamage = shooter.Type.HeavyPower / targetUnit.Type.Armour;
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

        public int Repair(Point target, Unit repairer)
        {
            foreach(Unit unit in Units)
            {
                if (unit.Position == target)
                {
                    repairer.RepairsLeft -= 1;
                    unit.Health += repairer.Type.RepairPower / unit.Type.Armour;
                    unit.HeavyShotsLeft = 0;
                    unit.LightShotsLeft = 0;
                    unit.MovesLeft = 0;
                    unit.RepairsLeft = 0;
                    unit.LoadsLeft = 0;
                    return (int)Math.Truncate(repairer.Type.RepairPower/ unit.Type.Armour * 100);
                }
            }
            return 0;
        }

        public int Load(Point target, Unit loader)
        {
            foreach(Unit unit in Units)
            {
                if (unit.Player.Faction == CurrentPlayer.Faction || unit.Player.Faction == Faction.Neutral)
                {
                    if (unit.Position == target)
                    {
                        loader.HeavyShotsLeft = 0;
                        loader.LightShotsLeft = 0;
                        loader.MovesLeft = 0;
                        loader.RepairsLeft = 0;
                        loader.LoadsLeft = 0;

                        if (unit.Cargo >= loader.Type.Capacity - loader.Cargo)
                        {
                            unit.Cargo -= loader.Type.Capacity - loader.Cargo;
                            int r = loader.Type.Capacity - loader.Cargo;
                            loader.Cargo = loader.Type.Capacity;
                            return r;
                        }
                        else
                        {
                            loader.Cargo += unit.Cargo;
                            int r = unit.Cargo;
                            unit.Cargo = 0;
                            return r;
                        }
                    }
                }
            }
            return 0;
        }

        public int Unload(Point target, Unit unloader)
        {
            foreach (Unit unit in Units)
            {
                if (unit.Player.Faction == CurrentPlayer.Faction)
                {
                    if (unit.Position == target)
                    {
                        unloader.HeavyShotsLeft = 0;
                        unloader.LightShotsLeft = 0;
                        unloader.MovesLeft = 0;
                        unloader.RepairsLeft = 0;
                        unloader.LoadsLeft = 0;

                        if (unit.Type.Capacity - unit.Cargo >= unloader.Cargo)
                        {
                            unit.Cargo += unloader.Cargo;
                            int r = unloader.Cargo;
                            unloader.Cargo = 0;
                            return r;
                        }
                        else
                        {
                            unloader.Cargo -= unit.Type.Capacity - unit.Cargo;
                            int r = unit.Type.Capacity - unit.Cargo;
                            unit.Cargo = unit.Type.Capacity;
                            return r;
                        }
                    }
                }
            }
            return 0;
        }

        public static Bitmap GetFactionFlag(Faction faction)
        {
            switch(faction)
            {
                case Faction.England:
                    return Bitmaps.Get("Data\\FlagEngland.png");
                case Faction.Germany:
                    return Bitmaps.Get("Data\\FlagGermany.png");
                case Faction.Japan:
                    return Bitmaps.Get("Data\\FlagJapan.png");
                case Faction.USA:
                    return Bitmaps.Get("Data\\FlagUSA.png");
                case Faction.Neutral:
                    return Bitmaps.Get("Data\\ArrowRight.png");
                default:
                    throw new Exception("Bad Faction.");
            }
        }

        public static string GetFactionGreetings(Faction faction)
        {
            switch(faction)
            {
                case Faction.England:
                    return "Good evening, Your Majesty. The Royal Navy awaits to do its duty!";
                case Faction.Germany:
                    return "Greetings, Mein Fuhrer! The Kriegsmarine ships are waiting!";
                case Faction.Japan:
                    return "Issue your orders, Honourable Emperor. The samurai are prepared to die. Banzai!";
                case Faction.USA:
                    return "Good morning Mr. President. The US Navy is ready to fight!";
                case Faction.Neutral:
                    return "Press the Begin Turn button";
                default:
                    throw new Exception("Bad Faction.");
            }
        }

        public static Color GetFactionColor(Faction faction)
        {
            switch(faction)
            {
                case Faction.England:
                    return Color.FromArgb(153, 44, 176);
                case Faction.Germany:
                    return Color.FromArgb(45, 181, 102);
                case Faction.Japan:
                    return Color.FromArgb(157, 23, 23);
                case Faction.USA:
                    return Color.FromArgb(176, 92, 44);
                case Faction.Neutral:
                    return Color.FromArgb(128, 128, 128);
                default:
                    throw new Exception("Bad Faction.");
            }
        }
    }
}
