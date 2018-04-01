using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Xml.Linq;

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
        public event Action Sinking;
        public event Action SubmarineDetected;
        public event Action PlayerChanged;
        public Player _NextPlayer;
        public int TurnIndex;
        public Random Random = new Random();
        public string ScenarioName;

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
                FirePlayerChangedEvent();
            }
        }

        public IList<Player> Players
        {
            get
            {
                return _Players.AsReadOnly();
            }
        }

        public Game(Bitmap map, string scenarioName)
        {
            _Units = new List<Unit>();
            _Players = new List<Player>();
            _Terrain = new Terrain(map);

            ScenarioName = scenarioName;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Color pixel = map.GetPixel(x, y);

                    if (pixel == GetFactionColor(Faction.England))
                    {
                        if (Players.Any(player => player.Faction == Faction.England))
                        {
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.England), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.England));
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.England), new Point(x, y)));
                        }
                    }
                    else if (pixel == Color.FromArgb(0, 0, 0))
                    {
                        if (Players.Any(player => player.Faction == Faction.Neutral))
                        {
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Neutral), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.Neutral));
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Neutral), new Point(x, y)));
                        }
                    }
                    else if (pixel == Color.FromArgb(128, GetFactionColor(Faction.England)))
                    {
                        if (Players.Any(player => player.Faction == Faction.England))
                        {
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.England), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.England));
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.England), new Point(x, y)));
                        }
                    }
                    else if (pixel == GetFactionColor(Faction.USA))
                    {
                        if (Players.Any(player => player.Faction == Faction.USA))
                        {
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.USA), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.USA));
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.USA), new Point(x, y)));
                        }
                    }
                    else if (pixel == Color.FromArgb(128, GetFactionColor(Faction.USA)))
                    {
                        if (Players.Any(player => player.Faction == Faction.USA))
                        {
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.USA), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.USA));
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.USA), new Point(x, y)));
                        }
                    }
                    else if (pixel == GetFactionColor(Faction.Germany))
                    {
                        if (Players.Any(player => player.Faction == Faction.Germany))
                        {
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Germany), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.Germany));
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Germany), new Point(x, y)));
                        }
                    }
                    else if (pixel == Color.FromArgb(128, GetFactionColor(Faction.Germany)))
                    {
                        if (Players.Any(player => player.Faction == Faction.Germany))
                        {
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.Germany), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.Germany));
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.Germany), new Point(x, y)));
                        }
                    }
                    else if (pixel == GetFactionColor(Faction.Japan))
                    {
                        if (Players.Any(player => player.Faction == Faction.Japan))
                        {
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Japan), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.Japan));
                            AddUnit(new Port(Players.ToList().Find(player => player.Faction == Faction.Japan), new Point(x, y)));
                        }
                    }
                    else if (pixel == Color.FromArgb(128, GetFactionColor(Faction.Japan)))
                    {
                        if (Players.Any(player => player.Faction == Faction.Japan))
                        {
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.Japan), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.USA));
                            AddUnit(new CoastalBattery(Players.ToList().Find(player => player.Faction == Faction.Japan), new Point(x, y)));
                        }
                    }
                    else if (pixel == GetFactionColor(Faction.Neutral))
                    {
                        if (Players.Any(player => player.Faction == Faction.Neutral))
                        {
                            AddUnit(new Factory(Players.ToList().Find(player => player.Faction == Faction.Neutral), new Point(x, y)));
                        }
                        else
                        {
                            _Players.Add(new Player(this, Faction.Neutral));
                            AddUnit(new Factory(Players.ToList().Find(player => player.Faction == Faction.Neutral), new Point(x, y)));
                        }
                    }
                }
            }

            _NextPlayer = Players[0];
        }

        public Game(Terrain terrain, string scenarioName)
        {
            _Terrain = terrain;
            _Players = new List<Player>();
            _Units = new List<Unit>();

            ScenarioName = scenarioName;
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

        public void NextPlayer()
        {
            CurrentPlayer = _NextPlayer;
            _NextPlayer = Players[(Players.IndexOf(CurrentPlayer) + 1) % Players.Count];
            if (_NextPlayer == Players[0])
            {
                TurnIndex++;
            }
            if (CurrentPlayer.Faction == Faction.Neutral)
            {
                NextPlayer();
            }
        }

        public void FireChangedEvent()
        {
            foreach (Unit unit in Units.ToArray())
            {
                unit.OnGameChanged();
            }

            if (Changed != null) Changed();
        }

        public void FireSinkingEvent()
        {
            if (Sinking != null) Sinking();
        }

        public void FireSubmarineDetectedEvent()
        {
            if (SubmarineDetected != null) SubmarineDetected();
        }

        public void FirePlayerChangedEvent()
        {
            if (PlayerChanged != null) PlayerChanged();
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
            float probability = MapDisplay.PointDifference(targetUnit.Position, shooter.Position) <= 3 ? 0.7f : 0.5f;
            if (Random.NextDouble() < probability)
            {
                // Calculate damage
                float damage = 0.65f * shooter.Type.LightPower / targetUnit.Type.Armour;
                damage *= (float)(1 + 0.2f * (Random.NextDouble() * 2 - 1));
                if (Random.NextDouble() < 0.07f) damage *= 1.5f;
                targetUnit.Health -= damage;
                return (int)(damage * 100);
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
            float probability = MapDisplay.PointDifference(targetUnit.Position, shooter.Position) <= 5 ? 0.7f : 0.5f;
            if (Random.NextDouble() < probability)
            {
                // Calculate damage
                float damage = 0.65f * shooter.Type.HeavyPower / targetUnit.Type.Armour;
                damage *= (float)(1 + 0.2f * (Random.NextDouble() * 2 - 1));
                if (Random.NextDouble() < 0.07f) damage *= 1.5f;
                targetUnit.Health -= damage;
                return (int)(damage * 100);
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
            if (torpedoer.TorpedoesLeft <= 0 || torpedoer.Torpedoes <= 0) return 0;

            torpedoer.TorpedoesLeft--;
            torpedoer.Torpedoes--;

            Unit targetUnit = GetUnitAt(target);

            if (targetUnit == null)
            {
                return 0;
            }

            int result = 0;

            if (Random.NextDouble() <= targetUnit.Type.TorpedoHitProbability)
            {
                if (Random.NextDouble() <= 0.1)
                {
                    result = 2;
                }
                else
                {
                    result = 1;
                }
            }

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

        public int LoadTorpedoes(Point target, Unit loader)
        {
            Unit targetUnit = GetUnitAt(target);

            if (targetUnit == null) return 0;
            if (targetUnit == loader) return 0;
            if (targetUnit.Player != loader.Player) return 0;
            if (targetUnit.Type != UnitType.Port) return 0;
            if (targetUnit.Cargo < 1) return 0;
            if (!IsUnitVisibleForPlayer(loader.Player, targetUnit)) return 0;
            if (targetUnit.IsSubmerged) return 0;

            targetUnit.Cargo--;
            loader.Torpedoes = loader.Type.MaxTorpedoes;
            return 1;
        }

        public int DepthCharge(Point target, Unit charger)
        {
            Unit targetUnit = GetUnitAt(target);
            charger.DepthChargesLeft--;

            if (charger == null) return 0;
            if (targetUnit == null) return 0;
            if (!targetUnit.IsSubmerged) return 0;
            if (targetUnit == charger) return 0;
            if (targetUnit.Player == charger.Player) return 0;
            if (charger.DepthChargesLeft < 1) return 0;

            float r = (float)Random.NextDouble();
            if (r < 0.1f)
            {
                targetUnit.Health = 0;
                return 100;
            }
            else if (r < 0.3f)
            {
                float r2 = (float)(Random.NextDouble() * 0.1f - 0.05f);
                targetUnit.Health -= (float)(0.6 + r2);
                return (int)(60 + r2 * 100);
            }
            else if (r < 0.6f)
            {
                float r2 = (float)(Random.NextDouble() * 0.1 - 0.05);
                targetUnit.Health -= (float)(0.3 + r2);
                return (int)(30 + r2 * 100);
            }
            else
            {
                return 0;
            }
        }

        public int InstallBattery(Point target, Unit installer)
        {
            if (installer == null) return 0;
            if (Terrain.Get(target.X, target.Y, TerrainType.Sea) == TerrainType.Sea) return 0;
            if (MapDisplay.PointDifference(target, installer.Position) >= 1.5) return 0;
            if (installer.IsSubmerged) return 0;
            if (installer.InstallsLeft < 1) return 0;

            installer.InstallsLeft--;
            RemoveUnit(installer);

            Unit newUnit = new BatteryInProgress(installer.Player, target);
            AddUnit(newUnit);
            return 1;
        }

        public int Capture(Point target, Unit captor)
        {
            Unit targetUnit = GetUnitAt(target);

            if (targetUnit == null) return 0;
            if (targetUnit.IsSubmerged) return 0;
            if (!IsUnitVisibleForPlayer(captor.Player, targetUnit)) return 0;
            if (captor == null) return 0;
            if (Terrain.Get(target.X, target.Y, TerrainType.Sea) == TerrainType.Sea) return 0;
            if (MapDisplay.PointDifference(target, captor.Position) >= 1.5) return 0;
            if (captor.IsSubmerged) return 0;
            if (captor.CapturesLeft < 1) return 0;

            captor.CapturesLeft--;

            float chance = 0;

            if (targetUnit.Player.Faction == Faction.Neutral)
            {
                if (targetUnit.Type == UnitType.Factory)
                {
                    chance = 0.6f;
                }
                else
                {
                    chance = 0.3f;
                }
            }
            else
            {
                if (targetUnit.Type == UnitType.Factory)
                {
                    chance = 0.4f;
                }
                else
                {
                    chance = 0.2f;
                }
            }

            if (Random.NextDouble() > chance)
            {
                captor.Health -= 0.1f;
                return 0;
            }

            targetUnit.Player = captor.Player;
            targetUnit.Name = captor.Player.GetUnitName(targetUnit);
            RemoveUnit(captor);
            return 1;
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
                        Unit testUnit = GetUnitAt(new Point(x, y));

                        if (testUnit == null || !IsUnitVisibleForPlayer(unit.Player, testUnit))
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

            if (!unit.IsSubmerged)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.LightRange)
                        {
                            Unit target = GetUnitAt(new Point(x, y));

                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && !target.IsSubmerged && target.Player != unit.Player && target.Type.Armour != float.PositiveInfinity)
                            {
                                if (unit.LightShotsLeft >= 1 && new Point(x, y) != unit.Position) possibleShots.Add(new Point(x, y));
                            }
                        }

                    }
                }
            }

            return possibleShots;
        }

        public HashSet<Point> GetPossibleHeavyShots(Unit unit)
        {
            HashSet<Point> possibleShots = new HashSet<Point>();
            if (!unit.IsSubmerged)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.HeavyRange)
                        {
                            Unit target = GetUnitAt(new Point(x, y));

                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && !target.IsSubmerged && target.Player != unit.Player && target.Type.Armour != float.PositiveInfinity)
                            {
                                if (unit.HeavyShotsLeft >= 1 && new Point(x, y) != unit.Position) possibleShots.Add(new Point(x, y));
                            }
                        }

                    }
                }
            }

            return possibleShots;
        }

        public HashSet<Point> GetPossibleRepairs(Unit unit)
        {
            HashSet<Point> possibleRepairs = new HashSet<Point>();

            if (!unit.IsSubmerged)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5f)
                        {
                            Unit target = GetUnitAt(new Point(x, y));
                            if (target != null && !target.IsSubmerged && target.Player == unit.Player &&
                                unit.RepairsLeft >= 1 && new Point(x, y) != unit.Position)
                            {
                                possibleRepairs.Add(new Point(x, y));
                            }
                        }

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
                            if (Terrain.Get(x, y, TerrainType.Land) == TerrainType.Sea)
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
                                if (!target.IsSubmerged)
                                {
                                    possibleLoads.Add(new Point(x, y));
                                }
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
                                if (!target.IsSubmerged)
                                {
                                    possibleLoads.Add(new Point(x, y));
                                }
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
                        if (unit.TorpedoesLeft >= 1 && new Point(x, y) != unit.Position && Terrain.Get(x, y) == TerrainType.Sea)
                        {

                            Unit target = GetUnitAt(new Point(x, y));

                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && !target.IsSubmerged && target.Player != unit.Player)
                            {
                                possibleTorpedoes.Add(new Point(x, y));
                            }
                        }
                    }

                }
            }

            return possibleTorpedoes;
        }

        public HashSet<Point> GetPossibleTorpedoLoads(Unit unit)
        {
            HashSet<Point> possibleLoads = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) < 2)
                    {
                        if (new Point(x, y) != unit.Position)
                        {
                            Unit target = GetUnitAt(new Point(x, y));

                            if (target != null && IsUnitVisibleForPlayer(unit.Player, target) && !target.IsSubmerged && target.Player == unit.Player && target.Type == UnitType.Port && target.Cargo >= 1)
                            {
                                possibleLoads.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }

            return possibleLoads;

        }

        public HashSet<Point> GetPossibleDepthCharges(Unit unit)
        {
            HashSet<Point> possibleCharges = new HashSet<Point>();

            if (unit.DepthChargesLeft >= 1)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) < 2)
                        {
                            if (new Point(x, y) != unit.Position && Terrain.Get(x, y) == TerrainType.Sea)
                            {
                                possibleCharges.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }

            return possibleCharges;
        }

        public HashSet<Point> GetPossibleBatteryInstallations(Unit unit)
        {
            HashSet<Point> possibleBatteries = new HashSet<Point>();

            if (unit.InstallsLeft >= 1)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) < 2)
                        {
                            if (new Point(x, y) != unit.Position && Terrain.Get(x, y) == TerrainType.Land)
                            {
                                possibleBatteries.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }

            return possibleBatteries;
        }

        public HashSet<Point> GetPossibleCaptures(Unit unit)
        {
            HashSet<Point> possibleCaptures = new HashSet<Point>();

            if (unit.CapturesLeft >= 1)
            {
                for (int x = 0; x < Terrain.Width; x++)
                {
                    for (int y = 0; y < Terrain.Height; y++)
                    {
                        if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) < 2)
                        {
                            if (new Point(x, y) != unit.Position && Terrain.Get(x, y) == TerrainType.Land)
                            {
                                possibleCaptures.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }


            return possibleCaptures;
        }

        public HashSet<Point> GetMoveRange(Unit unit)
        {
            HashSet<Point> range = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.MovesLeft)
                    {
                        range.Add(new Point(x, y));
                    }
                }
            }

            return range;
        }

        public HashSet<Point> GetLightArtilleryRange(Unit unit)
        {
            HashSet<Point> range = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.LightRange)
                    {
                        range.Add(new Point(x, y));
                    }
                }
            }

            return range;
        }

        public HashSet<Point> GetHeavyArtilleryRange(Unit unit)
        {
            HashSet<Point> range = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= unit.Type.HeavyRange)
                    {
                        range.Add(new Point(x, y));
                    }
                }
            }

            return range;
        }

        public HashSet<Point> GetTorpedoRange(Unit unit)
        {
            HashSet<Point> range = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 2)
                    {
                        range.Add(new Point(x, y));
                    }
                }
            }

            return range;
        }

        public HashSet<Point> GetDepthChargeRange(Unit unit)
        {
            HashSet<Point> range = new HashSet<Point>();

            for (int x = 0; x < Terrain.Width; x++)
            {
                for (int y = 0; y < Terrain.Height; y++)
                {
                    if (MapDisplay.PointDifference(unit.Position, new Point(x, y)) <= 1.5)
                    {
                        range.Add(new Point(x, y));
                    }
                }
            }

            return range;
        }

        public static XElement Save(Game game)
        {
            XElement gameNode = new XElement("Game");
            XElement terrainNode = Terrain.Save(game.Terrain);
            gameNode.Add(terrainNode);

            XElement playersNode = new XElement("Players");
            gameNode.Add(playersNode);
            foreach(Player player in game.Players)
            {
                playersNode.Add(Player.Save(player));
            }

            if (game.CurrentPlayer != null) gameNode.SetAttributeValue("CurrentPlayer", game.Players.IndexOf(game.CurrentPlayer));
            else gameNode.SetAttributeValue("CurrentPlayer", -1);

            gameNode.SetAttributeValue("NextPlayer", game.Players.IndexOf(game._NextPlayer));

            gameNode.SetAttributeValue("ScenarioName", game.ScenarioName);
        
            return gameNode;
        }

        public static Game Load(XElement gameNode)
        {
            XElement terrainNode = gameNode.Element("Terrain");
            Terrain terrain = Terrain.Load(terrainNode);

            Game game = new Game(terrain, XmlUtils.GetAttributeValue<string>(gameNode, "ScenarioName"));

            XElement playersNode = gameNode.Element("Players");
            foreach (XElement playerNode in playersNode.Elements())
            {
                game._Players.Add(Player.Load(game, playerNode));
            }

            if (XmlUtils.GetAttributeValue<int>(gameNode, "CurrentPlayer") == -1) game.CurrentPlayer = null;
            else game.CurrentPlayer = game.Players[XmlUtils.GetAttributeValue<int>(gameNode, "CurrentPlayer")];

            game._NextPlayer = game.Players[XmlUtils.GetAttributeValue<int>(gameNode, "NextPlayer")];

            return game;
        }
    }
}
