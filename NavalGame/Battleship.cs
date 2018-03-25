using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Battleship : Unit
    {
        public Battleship(Player player, Point position) : base(UnitType.Battleship, player, position)
        {
        }
    }
}