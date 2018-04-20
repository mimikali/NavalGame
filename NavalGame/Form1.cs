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

        public Form1(Game game)
        {
            InitializeComponent();
            Game = game;
            Game.PlayerChanged += GamePlayerChanged;
            MapDisplay.Game = Game;
            MapDisplay.OrdersDisplay = OrdersDisplay;
            OrdersDisplay.MapDisplay = MapDisplay;
            WindowState = FormWindowState.Maximized;
            GamePlayerChanged();
        }

        private void GamePlayerChanged()
        {
            Text = "Naval Game - Turn " + (Game.TurnIndex + 1);
        }

        private void FormSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
