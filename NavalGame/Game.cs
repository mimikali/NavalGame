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
        public int TurnIndex;
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

                    foreach (Unit unit in Units.OfType<Factory>())
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

        public Game(Terrain terrain, List<Faction> factions, List<UnitType> units, List<Faction> unitOwners, List<Point> unitPositions)
        {
            //_Terrain = GenerateTerrain(20, 20, 1);
            _Units = new List<Unit>();
            _Players = new List<Player>();
            _Terrain = terrain;
            foreach(Faction faction in factions)
            {
                _Players.Add(new Player(this, faction));
            }
            for (int i = 0; i < units.Count(); i++)
            {
                AddUnit(units[i].CreateUnit(_Players[factions.IndexOf(unitOwners[i])], unitPositions[i]));
            }
            AddUnit(UnitType.HeavyCruiser.CreateUnit(Players[0], new Point(15, 15)));
            AddUnit(UnitType.Submarine.CreateUnit(Players[1], new Point(17, 15)));
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
            FireChangedEvent();
        }

        public void RemoveUnit(Unit unit)
        {
            if (!Units.Contains(unit)) throw new Exception("Bad unit removal.");
            if (SelectedUnit == unit) SelectedUnit = null;
            _Units.Remove(unit);
            FireChangedEvent();
        }

        public void FireChangedEvent()
        {
            foreach (Unit unit in Units.ToArray())
            {
                unit.OnGameChanged();
            }

            if (Changed != null) Changed();
        }

        public int LightArtillery(Point target, Unit shooter)
        {
            // Use a shot
            shooter.LightShotsLeft -= 1;

            // Determine whether there is a target unit
            Unit targetUnit = GetUnitAt(target);

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
            Unit targetUnit = GetUnitAt(target);

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
            Unit unit = GetUnitAt(target);

            if (unit != null)
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
            Unit unit = GetUnitAt(target);

            if (unit != null && unit.Player.Faction == CurrentPlayer.Faction)
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
            return 0;
        }

        public int Torpedo(Point target, Unit torpedoer)
        {
            torpedoer.TorpedoesLeft--;

            Unit targetUnit = GetUnitAt(target);

            if (targetUnit == null)
            {
                return 0;
            }

            List<int> chances = new List<int>();
            for (int i = 0; i < MapDisplay.PointDifference(torpedoer.Position, target); i++)
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
            chances.Add(2);

            int result = chances[Random.Next(0, chances.Count)];

            float basedamage = torpedoer.Type.TorpedoPower / targetUnit.Type.Armour;
            float randomVariation = (float)((Random.NextDouble() * 2 - 1) * (basedamage / 3));

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

        public bool IsUnitVisibleForPlayer(Player player, Unit unit)
        {
            return unit.Player == player || (player.IsTileVisible(unit.Position) && unit.IsDetected);
        }

        public Unit GetUnitAt(Point position)
        {
            foreach(Unit unit in Units)
            {
                if (unit.Position == position)
                {
                    return unit;
                }
            }
            return null;
        }

        public HashSet<Point> GetPossibleMoves(Unit unit)
        {
            HashSet<Point> possibleMoves = new HashSet<Point>();

            for(int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.MovesLeft)
                    {
                        if (GetUnitAt(new Point(x, y)) == null)
                        {
                            if (Terrain.Get(x, y, TerrainType.Land) == TerrainType.Sea)
                            {
                                if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) < 2) possibleMoves.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }

            return possibleMoves;
        }

        public HashSet<Point> GetPossibleLightShots(Unit unit)
        {
            HashSet<Point> possibleShots = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.LightRange)
                    {
                        Unit target = GetUnitAt(new Point(x, y));

                        if (target != null && IsUnitVisibleForPlayer(unit.Player, target))
                        {
                            if (unit.LightShotsLeft >= 1 && new Point(x, y) != unit.Position) possibleShots.Add(new Point(x, y));
                        }
                    }

                }
            }

            return possibleShots;
        }

        public HashSet<Point> GetPossibleHeavyShots(Unit unit)
        {
            HashSet<Point> possibleShots = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.HeavyRange)
                    {
                        Unit target = GetUnitAt(new Point(x, y));

                        if (target != null && IsUnitVisibleForPlayer(unit.Player, target))
                        {
                            if (unit.HeavyShotsLeft >= 1 && new Point(x, y) != unit.Position) possibleShots.Add(new Point(x, y));
                        }
                    }

                }
            }

            return possibleShots;
        }

        public HashSet<Point> GetPossibleRepairs(Unit unit)
        {
            HashSet<Point> possibleRepairs = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5)
                    {
                        if (unit.RepairsLeft >= 1 && unit.Position != new Point(x, y)) possibleRepairs.Add(new Point(x, y));
                    }

                }
            }

            return possibleRepairs;
        }

        public HashSet<Point> GetPossibleBuilds(Unit unit)
        {
            HashSet<Point> possibleBuilds = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5)
                    {
                        if (GetUnitAt(new Point(x, y)) == null)
                        {
                            if (unit.BuildsLeft >= 1 && Terrain.Get(x, y, TerrainType.Land) == TerrainType.Sea)
                            {
                                possibleBuilds.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }

            return possibleBuilds;
        }

        public HashSet<Point> GetPossibleLoads(Unit unit)
        {
            HashSet<Point> possibleLoads = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5)
                    {
                        if (unit.LoadsLeft >= 1 && new Point(x, y) != unit.Position)
                        {
                            Unit target = GetUnitAt(new Point(x, y));
                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && (unit.Type.Capacity >= 1 && (target.Player == unit.Player || target.Player.Faction == Faction.Neutral)))
                            {
                                possibleLoads.Add(new Point(x, y));
                            }
                        }
                    }

                }
            }

            return possibleLoads;
        }

        public HashSet<Point> GetPossibleUnloads(Unit unit)
        {
            HashSet<Point> possibleLoads = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5)
                    {
                        if (unit.LoadsLeft >= 1 && new Point(x, y) != unit.Position)
                        {
                            Unit target = GetUnitAt(new Point(x, y));
                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && (unit.Type.Capacity >= 1 && (target.Player == unit.Player || target.Player.Faction == Faction.Neutral)))
                            {
                                possibleLoads.Add(new Point(x, y));
                            }
                        }
                    }

                }
            }

            return possibleLoads;
        }

        public HashSet<Point> GetPossibleTorpedoes(Unit unit)
        {
            HashSet<Point> possibleTorpedoes = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 2)
                    {
                        if (unit.TorpedoesLeft >= 1 && new Point(x, y) != unit.Position)
                        {

                            Unit target = GetUnitAt(new Point(x, y));

                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target))
                            {
                                possibleTorpedoes.Add(new Point(x, y));
                            }
                        }
                    }

                }
            }

            return possibleTorpedoes;
        }
    }
}
