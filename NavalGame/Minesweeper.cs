using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavalGame
{
    public class Minesweeper : Unit
    {
        public Minesweeper(Player player, Point position) : base(UnitType.Minesweeper, player, position)
        {
        }
    }
}