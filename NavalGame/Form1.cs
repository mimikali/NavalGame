using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavalGame
{
    public partial class Form1 : Form
    {
        Game Game;

        public Form1()
        {
            InitializeComponent();
            Game = new Game();
            MapDisplay.Game = Game;
            MapDisplay.OrdersDisplay = OrdersDisplay;
            OrdersDisplay.MapDisplay = MapDisplay;
        }

        private void FormSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
