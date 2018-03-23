﻿using System;
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
        Form Parent;

        public Form1(Terrain terrain, Form parent)
        {
            InitializeComponent();
            Parent = parent;
            Game = new Game(terrain);
            MapDisplay.Game = Game;
            MapDisplay.OrdersDisplay = OrdersDisplay;
            OrdersDisplay.MapDisplay = MapDisplay;
        }


        private void Form1Closed(object sender, FormClosedEventArgs e)
        {
            Parent.Show();
        }

        private void FormSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
