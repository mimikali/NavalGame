using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

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
            DivePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            DivePictureBox.Image = Bitmaps.Get("Data\\Dive.png");
            LoadTorpedoesPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LoadTorpedoesPictureBox.Image = Bitmaps.Get("Data\\LoadTorpedoes.png");
            DepthChargePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            DepthChargePictureBox.Image = Bitmaps.Get("Data\\DepthCharge.png");
            InstallBatteryPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            InstallBatteryPictureBox.Image = Bitmaps.Get("Data\\InstallBattery.png");
        }

        MapDisplay _MapDisplay;

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
            DivePictureBox.BorderStyle = BorderStyle.None;
            LoadTorpedoesPictureBox.BorderStyle = BorderStyle.None;
            DepthChargePictureBox.BorderStyle = BorderStyle.None;
            InstallBatteryPictureBox.BorderStyle = BorderStyle.None;
            CapturePictureBox.BorderStyle = BorderStyle.None;

            if (MapDisplay.Game.SelectedUnit != null) DivePictureBox.Image = MapDisplay.Game.SelectedUnit.IsSubmerged ? Bitmaps.Get("Data\\Surface.png") : Bitmaps.Get("Data\\Dive.png");

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

                case Order.DiveOrSurface:
                    DivePictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.LoadTorpedoes:
                    LoadTorpedoesPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.DepthCharge:
                    DepthChargePictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.InstallBattery:
                    InstallBatteryPictureBox.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case Order.Capture:
                    CapturePictureBox.BorderStyle = BorderStyle.Fixed3D;
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
                UnitTextBox.Text += " : " + selectedUnit.Type.Name;
                UnitTextBox.Text += Environment.NewLine + "Integrity: " + Math.Round(selectedUnit.Health * 100).ToString() + "%";
                if (selectedUnit.Type.Capacity >= 1) UnitTextBox.Text += Environment.NewLine + "Cargo: " + selectedUnit.Cargo + "/" + selectedUnit.Type.Capacity;
                if (selectedUnit.TurnsUntilCompletion > 0) UnitTextBox.Text += Environment.NewLine + "Turns until completion: " + selectedUnit.TurnsUntilCompletion;
                if (selectedUnit.Type == UnitType.Submarine && selectedUnit.IsSubmerged) UnitTextBox.Text += Environment.NewLine + "Oxygen left: " + ((Submarine)selectedUnit).OxygenLeft.ToString();
                if (selectedUnit.Type.Abilities.Contains(Order.Torpedo)) UnitTextBox.Text += Environment.NewLine + "Torpedo salvoes left: " + selectedUnit.Torpedoes;
                if (selectedUnit.Type.Abilities.Contains(Order.Move)) UnitTextBox.Text += Environment.NewLine + "Moves Left: " + selectedUnit.MovesLeft.ToString("0.0");
                if (selectedUnit.Type.Abilities.Contains(Order.LightArtillery)) UnitTextBox.Text += Environment.NewLine + "Light Artillery " + "Power: " + selectedUnit.Type.LightPower + ", Range: " + selectedUnit.Type.LightRange;
                if (selectedUnit.Type.Abilities.Contains(Order.HeavyArtillery)) UnitTextBox.Text += Environment.NewLine + "Heavy Artillery " + "Power: " + selectedUnit.Type.HeavyPower + ", Range: " + selectedUnit.Type.HeavyRange;
                if (!float.IsPositiveInfinity(selectedUnit.Type.Armour)) UnitTextBox.Text += Environment.NewLine + "Armour: " + selectedUnit.Type.Armour;

                if (selectedUnit.Type.Abilities.Contains(Order.Move)) MoveBox.Show(); else MoveBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.LightArtillery)) LightArtilleryBox.Show(); else LightArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.HeavyArtillery)) HeavyArtilleryBox.Show(); else HeavyArtilleryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Repair)) RepairBox.Show(); else RepairBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Build)) BuildBox.Show(); else BuildBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Load)) LoadBox.Show(); else LoadBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Unload)) UnloadBox.Show(); else UnloadBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Torpedo)) TorpedoBox.Show(); else TorpedoBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.DiveOrSurface))
                {
                    DiveBox.Show();
                    DiveButton.Text = selectedUnit.IsSubmerged ? "Surface" : "Dive";
                }
                else DiveBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.LoadTorpedoes)) LoadTorpedoesBox.Show(); else LoadTorpedoesBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.DepthCharge)) DepthChargeBox.Show(); else DepthChargeBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.InstallBattery)) InstallBatteryBox.Show(); else InstallBatteryBox.Hide();
                if (selectedUnit.Type.Abilities.Contains(Order.Capture)) CaptureBox.Show(); else CaptureBox.Hide();

                MoveBox.Enabled = selectedUnit.MovesLeft >= 1;
                LightArtilleryBox.Enabled = selectedUnit.LightShotsLeft >= 1 && !selectedUnit.IsSubmerged;
                HeavyArtilleryBox.Enabled = selectedUnit.HeavyShotsLeft >= 1 && !selectedUnit.IsSubmerged;
                BuildBox.Enabled = !selectedUnit.IsSubmerged;
                RepairBox.Enabled = selectedUnit.RepairsLeft >= 1 && !selectedUnit.IsSubmerged;
                LoadBox.Enabled = selectedUnit.LoadsLeft >= 1 && !selectedUnit.IsSubmerged;
                UnloadBox.Enabled = selectedUnit.LoadsLeft >= 1 && !selectedUnit.IsSubmerged;
                TorpedoBox.Enabled = selectedUnit.TorpedoesLeft >= 1;
                DiveBox.Enabled = selectedUnit.DivesLeft >= 1;
                LoadTorpedoesBox.Enabled = selectedUnit.Torpedoes < 4;
                DepthChargeBox.Enabled = selectedUnit.DepthChargesLeft >= 1;
                InstallBatteryBox.Enabled = selectedUnit.InstallsLeft >= 1;
                CaptureBox.Enabled = selectedUnit.CapturesLeft >= 1;

                if (MapDisplay.CurrentOrder == Order.Move && selectedUnit.MovesLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.LightArtillery && selectedUnit.LightShotsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.HeavyArtillery && selectedUnit.HeavyShotsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Repair && selectedUnit.RepairsLeft< 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Load && selectedUnit.LoadsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Unload && selectedUnit.LoadsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Torpedo && selectedUnit.TorpedoesLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.LoadTorpedoes && selectedUnit.Torpedoes == selectedUnit.Type.MaxTorpedoes) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.DepthCharge && selectedUnit.DepthChargesLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.InstallBattery && selectedUnit.InstallsLeft < 1) MapDisplay.CurrentOrder = null;
                if (MapDisplay.CurrentOrder == Order.Capture && selectedUnit.CapturesLeft < 1) MapDisplay.CurrentOrder = null;

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
            MapDisplay.Game.CurrentPlayer = null;
            NextTurnButton.Hide();
            BeginTurnButton.Show();
            MapDisplay.PlaySound("Data\\Bell.wav");
        }

        private void BeginTurnButtonClick(object sender, EventArgs e)
        {
            MapDisplay.Game.NextPlayer();
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
                MapDisplay.PlaySound("Data\\Klaxon.wav");
            }
        }

        private void LoadTorpedoesButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.LoadTorpedoes;
        }

        private void DepthChargeButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.DepthCharge;
        }

        private void InstallBatteryButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.InstallBattery;
        }

        private void CaptureButtonClick(object sender, EventArgs e)
        {
            MapDisplay.CurrentOrder = Order.Capture;
        }
    }
}
