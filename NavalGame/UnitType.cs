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
        public float ViewDistance;
        public float SonarRange;
        public float HeavyPower;
        public float HeavyRange;
        public float LightPower;
        public float LightRange;
        public int TorpedoPower;
        public float Armour;
        public int Capacity;
        public float RepairPower;
        public int BuildTime;
        public int Cost;
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

        //public static UnitType AircraftCarrier;

        public static UnitType Wreck;

        public static UnitType ShipInProgress;

        public static UnitType Port;

        public static UnitType Factory;

        public static UnitType TorpedoBoat;

        public static List<UnitType> UnitTypes;

        public static void InitializeUnitTypes()
        {
            Battleship = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Battleship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\BattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Battleship.png"),
                Speed = 4,
                ViewDistance = 9,
                SonarRange = 0,
                HeavyPower = 7,
                HeavyRange = 12,
                LightPower = 2,
                LightRange = 6,
                TorpedoPower = 0,
                Armour = 18,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 10,
                Cost = 30,
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
                SonarRange = 0,
                HeavyPower = 6,
                HeavyRange = 11,
                LightPower = 2,
                LightRange = 6,
                TorpedoPower = 0,
                Armour = 12,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 9,
                Cost = 25,
                CreateUnit = (player, position) => new Battlecruiser(player, position)
            };


            HeavyCruiser = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Heavy Cruiser",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\HeavyCruiserLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\HeavyCruiser.png"),
                Speed = 4,
                ViewDistance = 9,
                SonarRange = 0,
                HeavyPower = 5,
                HeavyRange = 9,
                LightPower = 3,
                LightRange = 6,
                TorpedoPower = 0,
                Armour = 12,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 7,
                Cost = 18,
                CreateUnit = (player, position) => new HeavyCruiser(player, position)
            };

            LightCruiser = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.LightArtillery },
                Name = "Light Cruiser",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\LightCruiserLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\LightCruiser.png"),
                Speed = 5,
                ViewDistance = 9,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 4,
                LightRange = 7,
                TorpedoPower = 9,
                Armour = 9,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 5,
                Cost = 13,
                CreateUnit = (player, position) => new LightCruiser(player, position)
            };

            Destroyer = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.LightArtillery, Order.Torpedo, Order.DepthCharge },
                Name = "Destroyer",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\DestroyerLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Destroyer.png"),
                Speed = 6,
                ViewDistance = 9,
                SonarRange = 2,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 2,
                LightRange = 5,
                TorpedoPower = 9,
                Armour = 4,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 7,
                CreateUnit = (player, position) => new Destroyer(player, position)
            };

            TorpedoBoat = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Torpedo },
                Name = "Torpedo Boat",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\TorpedoBoatLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\TorpedoBoat.png"),
                Speed = 7,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 9,
                Armour = 0.1f,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 1,
                Cost = 3,
                CreateUnit = (player, position) => new TorpedoBoat(player, position)
            };

            Minesweeper = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Mine, Order.LightArtillery },
                Name = "Minesweeper",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\MinesweeperLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Minesweeper.png"),
                Speed = 4,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 1,
                LightRange = 4,
                TorpedoPower = 0,
                Armour = 4,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 5,
                CreateUnit = (player, position) => new Minesweeper(player, position)
            };

            LightCargo = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Load, Order.Unload },
                Name = "Light Cargo Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\LightCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\LightCargo.png"),
                Speed = 5,
                ViewDistance = 8,
                SonarRange = 0,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 0,
                Armour = 2,
                Capacity = 5,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 5,
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
                TorpedoPower = 0,
                Armour = 3,
                Capacity = 8,
                RepairPower = 0,
                BuildTime = 3,
                Cost = 7,
                CreateUnit = (player, position) => new MediumCargo(player, position)
            };

            Submarine = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Torpedo, Order.LightArtillery, Order.DiveOrSurface },
                Name = "Submarine",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\SubmarineLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Submarine.png"),
                Speed = 4,
                ViewDistance = 8,
                SonarRange = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 1,
                LightRange = 4,
                TorpedoPower = 9,
                Armour = 2,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 2,
                Cost = 6,
                CreateUnit = (player, position) => new Submarine(player, position)
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
                TorpedoPower = 0,
                Armour = 0.1f,
                Capacity = 0,
                RepairPower = 0,
                BuildTime = 0,
                Cost = 0,
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
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 0,
                Armour = 100,
                Capacity = 80,
                RepairPower = 2.5f,
                BuildTime = 0,
                Cost = 0,
                CreateUnit = (player, position) => new Port(player, position)
            };

            Factory = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "Factory",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\FactoryLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Factory.png"),
                Speed = 0,
                ViewDistance = 100,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                TorpedoPower = 0,
                Armour = 100,
                Capacity = 20,
                RepairPower = 0,
                BuildTime = 0,
                Cost = 0,
                CreateUnit = (player, position) => new Factory(player, position)
            };

            UnitTypes = new List<UnitType>
            {
                LightCargo, MediumCargo, TorpedoBoat, Destroyer, LightCruiser, HeavyCruiser, Battlecruiser, Battleship, Minesweeper, Submarine, Wreck, ShipInProgress, Port, Factory
            };
        }
    }
}