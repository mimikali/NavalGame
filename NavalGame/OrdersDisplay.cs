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
            LoadPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LoadPictureBox.Image = Bitmaps.Get("Data\\Load.png");
            UnloadPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            UnloadPictureBox.Image = Bitmaps.Get("Data\\Unload.png");
            TorpedoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            TorpedoPictureBox.Image = Bitmaps.Get("Data\\Torpedo.png");
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
            MovePictureBox.BorderStyle = BorderStyle.None;
            BuildPictureBox.BorderStyle = BorderStyle.None;
            LoadPictureBox.BorderStyle = BorderStyle.None;
            UnloadPictureBox.BorderStyle = BorderStyle.None;
            RepairPictureBox.BorderStyle = BorderStyle.None;
            LightArtilleryPictureBox.BorderStyle = BorderStyle.None;
            HeavyArtilleryPictureBox.BorderStyle = BorderStyle.None;
            TorpedoPictureBox.BorderStyle = BorderStyle.None;

            switch (MapDisplay.CurrentOrder)
            {
                case null:
                    break;

                case Order.Move:
                    MovePictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Build:
                    BuildPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.LightArtillery:
                    LightArtilleryPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.HeavyArtillery:
                    HeavyArtilleryPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Repair:
                    RepairPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Load:
                    LoadPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Unload:
                    UnloadPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Torpedo:
                    TorpedoPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;
            }

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
                if (MapDisplay.Game.CurrentPlayer != null && MapDisplay.Game.CurrentPlayer.Faction != Faction.Neutral)
                {
                    if (MapDisplay.Game.CurrentPlayer == selectedUnit.Player)
                    {
                        OrdersPanel.Show();
                    }
                    UnitPanel.Show();
                    InfoPanel.Show();
                }

                UnitPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                UnitPictureBox.Image = MapDisplay.Game.SelectedUnit.Type.LargeBitmap;
                UnitTextBox.Text = selectedUnit.Name;
                UnitTextBox.Text += Environment.NewLine + selectedUnit.Type.Name;
                UnitTextBox.Text += Environment.NewLine + "Health: " + Math.Round(selectedUnit.Health * 100).ToString() + "%";
                if (selectedUnit.Type.Capacity >= 1) UnitTextBox.Text += Environment.NewLine + "Cargo: " + selectedUnit.Cargo + "/" + selectedUnit.Type.Capacity;
                if (selectedUnit.TurnsUntilCompletion > 0) UnitTextBox.Text += Environment.NewLine + "Turns until completion: " + selectedUnit.TurnsUntilCompletion;

                if (selectedUnit.Type.Abilities.Contains(Order.Move)) MoveBox.Show(); else MoveBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.LightArtillery)) LightArtilleryBox.Show(); else LightArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.HeavyArtillery)) HeavyArtilleryBox.Show(); else HeavyArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Repair)) RepairBox.Show(); else RepairBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Build)) BuildBox.Show(); else BuildBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Load)) LoadBox.Show(); else LoadBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Unload)) UnloadBox.Show(); else UnloadBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Torpedo)) TorpedoBox.Show(); else TorpedoBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.DiveOrSurface)) DiveBox.Show(); else DiveBox.Hide();

                if (selectedUnit.MovesLeft >= 1) MoveBox.Enabled = true;
                else
                {
                    MoveBox.Enabled = false;
                }
                if (selectedUnit.LightShotsLeft >= 1) LightArtilleryBox.Enabled = true;
                else
                {
                    LightArtilleryBox.Enabled = false;
                }
                if (selectedUnit.HeavyShotsLeft >= 1) HeavyArtilleryBox.Enabled = true;
                else
                {
                    HeavyArtilleryBox.Enabled = false;
                }
                if (selectedUnit.BuildsLeft >= 1) BuildBox.Enabled = true;
                else
                {
                    BuildBox.Enabled = false;
                }
                if (selectedUnit.RepairsLeft >= 1) RepairBox.Enabled = true;
                else
                {
                    RepairBox.Enabled = false;
                }
                if (selectedUnit.LoadsLeft >= 1) LoadBox.Enabled = true;
                else
                {
                    LoadBox.Enabled = false;
                }
                if (selectedUnit.LoadsLeft >= 1) UnloadBox.Enabled = true;
                else
                {
                    UnloadBox.Enabled = false;
                }
                if (selectedUnit.TorpedoesLeft >= 1) TorpedoBox.Enabled = true;
                else
                {
                    TorpedoBox.Enabled = false;
                }
                if (selectedUnit.DivesLeft >= 1) DiveBox.Enabled = true;
                else
                {
                    DiveBox.Enabled = false;
                }

                if (MapDisplay.CurrentOrder == Order.Move && selectedUnit.MovesLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.LightArtillery && selectedUnit.LightShotsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.HeavyArtillery && selectedUnit.HeavyShotsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Build && selectedUnit.BuildsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Repair && selectedUnit.RepairsLeft< 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Load && selectedUnit.LoadsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Unload && selectedUnit.LoadsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Torpedo && selectedUnit.TorpedoesLeft < 1) MapDisplay.CurrentOrder = null;

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
            if (MapDisplay.CurrentOrder == Order.Move) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Move;
            GameChanged();
        }

        private void NextTurnButtonClick(object sender, EventArgs e)
        {
            _NextPlayer = MapDisplay.Game.Players[(MapDisplay.Game.Players.IndexOf(MapDisplay.Game.CurrentPlayer) + 1) % MapDisplay.Game.Players.Count];
            if (_NextPlayer.Faction == Faction.Neutral) _NextPlayer = MapDisplay.Game.Players[(MapDisplay.Game.Players.IndexOf(MapDisplay.Game.CurrentPlayer) + 2) % MapDisplay.Game.Players.Count];
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
            if (MapDisplay.CurrentOrder == Order.LightArtillery) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.LightArtillery;
            GameChanged();
        }

        private void HeavyArtilleryButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.HeavyArtillery) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.HeavyArtillery;
            GameChanged();
        }

        private void RepairButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.Repair) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Repair;
            GameChanged();
        }

        private void BuildButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.Build) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Build;
            GameChanged();
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.Load) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Load;
            GameChanged();
        }

        private void UnloadButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.Unload) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Unload;
            GameChanged();
        }

        private void TorpedoButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.CurrentOrder == Order.Torpedo) MapDisplay.CurrentOrder = null;
            else MapDisplay.CurrentOrder = Order.Torpedo;
            GameChanged();
        }

        private void DiveButtonClick(object sender, EventArgs e)
        {
            if (MapDisplay.Game.SelectedUnit.DivesLeft >= 1)
            {
                MapDisplay.Game.SelectedUnit.DiveOrSurface();
            }
            GameChanged();
        }
    }
}
