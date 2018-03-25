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
        public float HeavyPower;
        public float HeavyRange;
        public float LightPower;
        public float LightRange;
        public float Armour;
        public float RepairPower;
        public int BuildTime;
        public Func<Player, Point, Unit> CreateUnit;

        public static UnitType Battleship;

        public static UnitType Destroyer;

        public static UnitType Minesweeper;

        public static UnitType LightCargo;

        public static UnitType MediumCargo;

        public static UnitType Wreck;

        public static UnitType ShipInProgress;

        public static UnitType Port;

        public static List<UnitType> UnitTypes;

        public static void InitializeUnitTypes()
        {
            Battleship = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.HeavyArtillery, Order.LightArtillery },
                Name = "Battleship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\US\\BattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\US\\Battleship.png"),
                Speed = 4,
                ViewDistance = 9,
                HeavyPower = 7,
                HeavyRange = 12,
                LightPower = 2,
                LightRange = 6,
                Armour = 20,
                RepairPower = 0,
                BuildTime = 10,
                CreateUnit = (player, position) => new Battleship(player, position)
            };

            Destroyer = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.LightArtillery, Order.Torpedo, Order.DepthCharge },
                Name = "Destroyer",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\US\\BattleshipLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\US\\Battleship.png"),
                Speed = 6,
                ViewDistance = 9,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 2,
                LightRange = 5,
                Armour = 4,
                RepairPower = 0,
                BuildTime = 3,
                CreateUnit = (player, position) => new Destroyer(player, position)
            };

            Minesweeper = new UnitType
            {
                Abilities = new List<Order> { Order.Move, Order.Mine },
                Name = "Minesweeper",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\MediumCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\MediumCargo.png"),
                Speed = 4,
                ViewDistance = 8,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 4,
                RepairPower = 0,
                BuildTime = 3,
                CreateUnit = (player, position) => new Minesweeper(player, position)
            };

            LightCargo = new UnitType
            {
                Abilities = new List<Order> { Order.Move },
                Name = "Light Cargo Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\LightCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\LightCargo.png"),
                Speed = 5,
                ViewDistance = 8,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 2,
                RepairPower = 0,
                BuildTime = 2,
                CreateUnit = (player, position) => new LightCargo(player, position)
            };

            MediumCargo = new UnitType
            {
                Abilities = new List<Order> { Order.Move },
                Name = "Medium Cargo Ship",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\MediumCargoLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\MediumCargo.png"),
                Speed = 3,
                ViewDistance = 8,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 3,
                RepairPower = 0,
                BuildTime = 3,
                CreateUnit = (player, position) => new LightCargo(player, position)
            };

            Wreck = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "Wreck",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\WreckLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\Wreck.png"),
                Speed = 0,
                ViewDistance = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 0.1f,
                RepairPower = 0,
                BuildTime = 0,
                CreateUnit = (player, position) => new Wreck(player, position)
            };

            ShipInProgress = new UnitType
            {
                Abilities = new List<Order> { },
                Name = "ShipInProgress",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\ShipInProgressLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\ShipInProgress.png"),
                Speed = 0,
                ViewDistance = 1,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 0.1f,
                RepairPower = 0,
                BuildTime = 0,
                CreateUnit = (player, position) => new ShipInProgress(player, position)
            };

            Port = new UnitType
            {
                Abilities = new List<Order> { Order.Build, Order.Repair },
                Name = "Port",
                LargeBitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\PortLarge.png"),
                Bitmap = Bitmaps.Get("Data\\Ships\\Miscellaneous\\Port.png"),
                Speed = 0,
                ViewDistance = 10,
                HeavyPower = 0,
                HeavyRange = 0,
                LightPower = 0,
                LightRange = 0,
                Armour = 100,
                RepairPower = 0,
                BuildTime = 0,
                CreateUnit = (player, position) => new Port(player, position)
            };

            UnitTypes = new List<UnitType>
            {
                LightCargo, MediumCargo, Destroyer, Battleship, Minesweeper, Wreck, ShipInProgress, Port
            };
        }
    }
}