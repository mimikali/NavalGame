using System;
using System.Collections.Generic;
using System.Drawing;


namespace NavalGame
{
    public class UnitType
    {
        public List<Order> Abilities;
        public string Name { get; set; }
        public Bitmap LargeBitmap;
        public Bitmap Bitmap;
        public float Speed;
        public float SubmergedSpeed;
        public float ViewDistance;
        public bool AlwaysVisible;
        public float SonarRange;
        public float HeavyPower;
        public float HeavyRange;
        public float LightPower;
        public float LightRange;
        public int TorpedoPower;
        public int MaxTorpedoes;
        public int MaxMines;
        public float TorpedoHitProbability;
        public float Armour;
        public int Capacity;
        public float RepairPower;
        public int BuildTime;
        public int Cost;
        public List<Faction> Factions;
        public Func<Player, Point, Unit> CreateUnit;

        public static UnitType Battleship;

        public static UnitType Battlecruiser;

        public static UnitType HeavyCruiser;

        public static UnitType LightCruiser;

        public static UnitType Destroyer;

        public static UnitType Minesweeper;

        public static UnitType LightCargo;

        public static UnitType MediumCargo;

        public static UnitType Submarine;

        public static UnitType UBoat;

        //public static UnitType AircraftCarrier;

        public static UnitType Wreck;

        public static UnitType ShipInProgress;

        public static UnitType Port;

        public static UnitType Factory;

        public static UnitType TorpedoBoat;

        public static UnitType BatteryBarge;

        public static UnitType BatteryInProgress;

        public static UnitType CoastalBattery;

        public static UnitType Frigate;

        public static UnitType TroopShip;

        public static UnitType PocketBattleship;

        public static UnitType SuperBattleship;

        public static List<UnitType> UnitTypes;

        public static void InitializeUnitTypes()
        {
            SuperBattleship = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Super Battleship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\SuperBattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\SuperBattleship.png"),
                Speed = 4,
                ViewDistance = 9,
                HeavyPower = 18,
                HeavyRange = 9,
                LightPower = 4,
                LightRange = 7,
                TorpedoHitProbability = 0.4f,
                Armour = 38,
                BuildTime = 10,
                Cost = 40,
                Factions = new List<Faction> { Faction.Japan, Faction.USA },
                CreateUnit = (player, position) => new SuperBattleship(player, position)
            };

            Battleship = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Battleship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\BattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Battleship.png"),
                Speed = 4,
                ViewDistance = 9,
                HeavyPower = 15,
                HeavyRange = 9,
                LightPower = 4,
                LightRange = 7,
                TorpedoHitProbability = 0.4f,
                Armour = 30,
                BuildTime = 9,
                Cost = 32,
                Factions = new List<Faction> { Faction.Germany, Faction.England, Faction.Japan, Faction.USA },
                CreateUnit = (player, position) => new Battleship(player, position)
            };

            Battlecruiser = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Battlecruiser",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\BattlecruiserLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Battlecruiser.png"),
                Speed = 5,
                ViewDistance = 9,
                HeavyPower = 15,
                HeavyRange = 9,
                LightPower = 4,
                LightRange = 7,
                TorpedoHitProbability = 0.4f,
                Armour = 20,
                BuildTime = 9,
                Cost = 32,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Battlecruiser(player, position)
            };

            PocketBattleship = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Pocket Battleship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\PocketBattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\PocketBattleship.png"),
                Speed = 4,
                ViewDistance = 9,
                HeavyPower = 12,
                HeavyRange = 9,
                LightPower = 4,
                LightRange = 7,
                TorpedoHitProbability = 0.4f,
                Armour = 18,
                BuildTime = 8,
                Cost = 24,
                Factions = new List<Faction> { Faction.Germany },
                CreateUnit = (player, position) => new PocketBattleship(player, position)
            };

            HeavyCruiser = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Heavy Cruiser",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\HeavyCruiserLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\HeavyCruiser.png"),
                Speed = 5,
                ViewDistance = 9,
                HeavyPower = 7,
                HeavyRange = 8,
                LightPower = 2,
                LightRange = 6,
                TorpedoHitProbability = 0.4f,
                Armour = 12,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 7,
                Cost = 16,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new HeavyCruiser(player, position)
            };

            LightCruiser = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Light Cruiser",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\LightCruiserLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\LightCruiser.png"),
                Speed = 5,
                ViewDistance = 9,
                HeavyPower = 4,
                HeavyRange = 7,
                LightPower = 1,
                LightRange = 5,
                TorpedoHitProbability = 0.2f,
                Armour = 8,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 5,
                Cost = 10,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new LightCruiser(player, position)
            };

            Destroyer = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.LightArtillery, Order.Torpedo, Order.DepthCharge, Order.LoadTorpedoes },
                Name = "Destroyer",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\DestroyerLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Destroyer.png"),
                Speed = 6,
                ViewDistance = 9,
                SonarRange = 2,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 2,
                LightRange = 6,
                TorpedoPower = 9,
                TorpedoHitProbability = 0.2f,
                MaxTorpedoes = 4,
                Armour = 4,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 7,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Destroyer(player, position)
            };

            Frigate = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.LightArtillery, Order.DepthCharge },
                Name = "Frigate",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\FrigateLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Frigate.png"),
                Speed = 5,
                ViewDistance = 9,
                SonarRange = 2,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 1,
                LightRange = 5,
                TorpedoHitProbability = 0.2f,
                Armour = 3,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 4,
                Factions = new List<Faction> { Faction.England },
                CreateUnit = (player, position) => new Frigate(player, position)
            };

            TorpedoBoat = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Torpedo, Order.LoadTorpedoes },
                Name = "Torpedo Boat",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\TorpedoBoatLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\TorpedoBoat.png"),
                Speed = 6,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 7,
                MaxTorpedoes = 2,
                TorpedoHitProbability = 0.2f,
                Armour = 1,
                BuildTime = 2,
                Cost = 3,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new TorpedoBoat(player, position)
            };

            Minesweeper = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Mine, Order.Sweep, Order.SearchMines, Order.LoadMines, Order.LightArtillery },
                Name = "Minesweeper",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\MinesweeperLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Minesweeper.png"),
                Speed = 4,
                ViewDistance = 8,
                LightPower = 1,
                LightRange = 5,
                TorpedoHitProbability = 0.2f,
                Armour = 4,
                Capacity = 0,
                MaxMines = 3,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 6,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Minesweeper(player, position)
            };

            TroopShip = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Capture },
                Name = "Troop Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\TroopShipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\TroopShip.png"),
                Speed = 3,
                ViewDistance = 8,
                TorpedoHitProbability = 0.4f,
                Armour = 4,
                Cost = 10,
                BuildTime = 5,
                Factions = new List<Faction> { Faction.England, Faction.USA, Faction.Germany, Faction.Japan, Faction.Neutral },
                CreateUnit = (player, position) => new TroopShip(player, position)
            };

            LightCargo = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Load, Order.Unload },
                Name = "Light Cargo Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\LightCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\LightCargo.png"),
                Speed = 4,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoHitProbability = 0.4f,
                Armour = 3,
                Capacity = 5,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 5,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new LightCargo(player, position)
            };

            MediumCargo = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Load, Order.Unload },
                Name = "Medium Cargo Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\MediumCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\MediumCargo.png"),
                Speed = 3,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoHitProbability = 0.4f,
                Armour = 6,
                Capacity = 10,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 6,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new MediumCargo(player, position)
            };

            Submarine = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Torpedo, Order.LightArtillery, Order.DiveOrSurface, Order.LoadTorpedoes },
                Name = "Submarine",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\SubmarineLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Submarine.png"),
                Speed = 4,
                SubmergedSpeed = 2.5f,
                ViewDistance = 8,
                SonarRange = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 1,
                LightRange = 5,
                TorpedoPower = 9,
                MaxTorpedoes = 4,
                TorpedoHitProbability = 0.2f,
                Armour = 2,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 6,
                Factions = new List<Faction> { Faction.England, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Submarine(player, position)
            };

            UBoat = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Torpedo, Order.LightArtillery, Order.DiveOrSurface, Order.LoadTorpedoes },
                Name = "U-Boat",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\SubmarineLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Submarine.png"),
                Speed = 4,
                SubmergedSpeed = 2.5f,
                ViewDistance = 8,
                SonarRange = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 1,
                LightRange = 5,
                TorpedoPower = 9,
                MaxTorpedoes = 4,
                TorpedoHitProbability = 0.2f,
                Armour = 2,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 4,
                Factions = new List<Faction> { Faction.Germany },
                CreateUnit = (player, position) => new UBoat(player, position)
            };

            CoastalBattery = new UnitType
            {
                Abilities = new List<Order> { Order.HeavyArtillery, Order.LightArtillery },
                Name = "Coastal Battery",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\CoastalBatteryLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\CoastalBattery.png"),
                ViewDistance = 9,
                AlwaysVisible = true,
                LightPower = 4,
                LightRange = 7,
                HeavyPower = 6,
                HeavyRange = 9,
                TorpedoHitProbability = 0f,
                Armour = 24,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new CoastalBattery(player, position)
            };

            BatteryInProgress = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "Battery Under Construction",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\BatteryInProgressLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\BatteryInProgress.png"),
                ViewDistance = 1,
                TorpedoHitProbability = 0f,
                Armour = 5,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new BatteryInProgress(player, position)
            };

            BatteryBarge = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.InstallBattery },
                Name = "Battery Installation Barge",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\BatteryBargeLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\BatteryBarge.png"),
                Speed = 3,
                ViewDistance = 8,
                TorpedoHitProbability = 0.4f,
                Armour = 2,
                BuildTime = 5,
                Cost = 10,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new BatteryBarge(player, position)
            };

            //AircraftCarrier = new UnitType
            //{
            //    Abilities = new List<Order> { Order.Move, Order.LightArtillery },
            //    Name = "Aircraft Carrier",
            //    LargeBitmap = Bitmaps.Get("Data\\Ships\\AircraftCarrierLarge.png"),
            //    Bitmap = Bitmaps.Get("Data\\Ships\\AircraftCarrier.png"),
            //    Speed = 4,
            //    ViewDistance = 9,
            //    HeavyPower = 0,
            //    HeavyRange = 0,
            //    LightPower = 3,
            //    LightRange = 6,
            //    TorpedoPower = 0,
            //    Armour = 10,
            //    Capacity = 0,
            //    RepairPower = 0,
            //    BuildTime = 12,
            //    Cost = 35,
            //    Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
            //    CreateUnit = (player, position) => new AircraftCarrier(player, position)
            //};

            Wreck = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "Wreck",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\WreckLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Wreck.png"),
                Speed = 0,
                ViewDistance = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 0,
                Armour = float.PositiveInfinity,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 0,
                Cost = 0,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Wreck(player, position)
            };

            ShipInProgress = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "ShipInProgress",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\ShipInProgressLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\ShipInProgress.png"),
                Speed = 0,
                ViewDistance = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoHitProbability = 0.8f,
                Armour = 2f,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 0,
                Cost = 0,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new ShipInProgress(player, position)
            };

            Port = new UnitType
            {
                Abilities = new List<Order> { Order.Build, Order.Repair },
                Name = "Port",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\PortLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Port.png"),
                Speed = 0,
                ViewDistance = 10,
                AlwaysVisible = true,
                Armour = float.PositiveInfinity,
                Capacity = 80,
                RepairPower = 2.5f,
                BuildTime = 0,
                Cost = 0,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Port(player, position)
            };

            Factory = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "Factory",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\FactoryLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Factory.png"),
                Speed = 0,
                ViewDistance = 9,
                AlwaysVisible = true,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 0,
                Armour = float.PositiveInfinity,
                Capacity = 20,
                RepairPower = 0,
                BuildTime = 0,
                Cost = 0,
                Factions = new List<Faction> { Faction.England, Faction.Germany, Faction.Japan, Faction.USA, Faction.Neutral },
                CreateUnit = (player, position) => new Factory(player, position)
            };

            UnitTypes = new List<UnitType>
            {
                LightCargo, MediumCargo, TorpedoBoat, Frigate, Destroyer, LightCruiser, HeavyCruiser, PocketBattleship, Battlecruiser, Battleship, SuperBattleship, Minesweeper, Submarine, UBoat, TroopShip, BatteryBarge, BatteryInProgress, Wreck, ShipInProgress, CoastalBattery, Port, Factory
            };
        }
    }
}