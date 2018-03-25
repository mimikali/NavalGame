using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavalGame
{
    public partial class OrdersDisplay : UserControl
    {
        public OrdersDisplay()
        {
            InitializeComponent();

            MovePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MovePictureBox.Image = Bitmaps.Get("Data\\Move.png");
            LightArtilleryPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LightArtilleryPictureBox.Image = Bitmaps.Get("Data\\LightArtillery.png");
            HeavyArtilleryPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            HeavyArtilleryPictureBox.Image = Bitmaps.Get("Data\\HeavyArtillery.png");
            RepairPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            RepairPictureBox.Image = Bitmaps.Get("Data\\Repair.png");
            BuildPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            BuildPictureBox.Image = Bitmaps.Get("Data\\Build.png");
        }

        MapDisplay _MapDisplay;
        Player _NextPlayer;

        public MapDisplay MapDisplay
        {
            get
            {
                return _MapDisplay;
            }

            set
            {
                _MapDisplay = value;
            }
        }

        public void GameChanged()
        {
            if (MapDisplay.Game.CurrentPlayer != null)
            {
                FlagBox.Image = Game.GetFactionFlag(MapDisplay.Game.CurrentPlayer.Faction);
                GreetingText.Text = Game.GetFactionGreetings(MapDisplay.Game.CurrentPlayer.Faction);
                GreetingText.BackColor = Game.GetFactionColor(MapDisplay.Game.CurrentPlayer.Faction);
            }
            else
            {
                FlagBox.Image = null;
                GreetingText.Text = "Press the Begin Turn button.";
                GreetingText.BackColor = SystemColors.Control;
            }

            var selectedUnit = MapDisplay.Game.SelectedUnit;
            if (selectedUnit != null)
            {
                UnitPanel.Show();
                OrdersPanel.Show();
                InfoPanel.Show();

                UnitPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                UnitPictureBox.Image = MapDisplay.Game.SelectedUnit.Type.LargeBitmap;
                UnitTextBox.Text = selectedUnit.Name;
                UnitTextBox.Text += Environment.NewLine + selectedUnit.Type.Name;
                UnitTextBox.Text += Environment.NewLine + "Health: " + Math.Round(selectedUnit.Health * 100).ToString() + "%";
                if (selectedUnit.TurnsUntilCompletion > 0)
                {
                    UnitTextBox.Text += Environment.NewLine + "Turns until completion: " + selectedUnit.TurnsUntilCompletion;
                }

                if (selectedUnit.Type.Abilities.Contains(Order.Move)) MoveBox.Show(); else MoveBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.LightArtillery)) LightArtilleryBox.Show(); else LightArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.HeavyArtillery)) HeavyArtilleryBox.Show(); else HeavyArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Repair)) RepairBox.Show(); else RepairBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Build)) BuildBox.Show(); else BuildBox.Hide();
                MoveBox.Enabled = selectedUnit.MovesLeft >= 1;
                LightArtilleryBox.Enabled = selectedUnit.LightShotsLeft >= 1;
                HeavyArtilleryBox.Enabled = selectedUnit.HeavyShotsLeft >= 1;
                RepairBox.Enabled = selectedUnit.RepairsLeft >= 1;
                BuildBox.Enabled = selectedUnit.BuildsLeft >= 1;

                HealthBar.Value = (int)(selectedUnit.Health * 100);
            }
            else
            {
                UnitPanel.Hide();
                OrdersPanel.Hide();
                InfoPanel.Hide();
            }
            Invalidate();
        }

        public OrdersDisplay(MapDisplay mapDisplay)
        {
            _MapDisplay = mapDisplay;
        }

        private void MoveButtonClicked(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.Move;
        }

        private void NextTurnButtonClick(object sender, EventArgs e)
        {
            _NextPlayer = MapDisplay.Game.Players[(MapDisplay.Game.Players.IndexOf(MapDisplay.Game.CurrentPlayer) + 1) % MapDisplay.Game.Players.Count];
            MapDisplay.Game.CurrentPlayer = null;
            NextTurnButton.Hide();
            BeginTurnButton.Show();
        }

        private void BeginTurnButtonClick(object sender, EventArgs e)
        {
            MapDisplay.Game.CurrentPlayer = _NextPlayer ?? MapDisplay.Game.Players[0]; //_NextPlayer != null ? _NextPlayer : MapDisplay.Game.Players[0];
            BeginTurnButton.Hide();
            NextTurnButton.Show();
        }

        private void LightArtilleryButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.LightArtillery;
        }

        private void HeavyArtilleryButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.HeavyArtillery;
        }

        private void RepairButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.Repair;
        }

        private void BuildButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.Build;
        }
    }
}
