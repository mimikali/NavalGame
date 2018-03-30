using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace NavalGame
{
    public partial class BuildForm : Form
    {
        Game _Game;
        Point _BuildPosition;
        Unit _Builder;
        SoundPlayer _SoundPlayer;

        public BuildForm(Game game, Unit builder, Point buildPosition)
        {
            _SoundPlayer = new SoundPlayer();
            _Game = game;
            _Builder = builder;
            _BuildPosition = buildPosition;

            InitializeComponent();
            _BuildButton.Enabled = false;
            SpendText.Text = "Available to spend: " + _Builder.Cargo;

            foreach (var unitType in UnitType.UnitTypes)
            {
                if (unitType.BuildTime > 0)
                {
                    UnitList.Items.Add(unitType);
                }
            }

            if (UnitList.Items.Count >= 1) UnitList.SelectedIndex = 0;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BuildButtonClick(object sender, EventArgs e)
        {
            ShipInProgress newShip = new ShipInProgress(_Game.CurrentPlayer, _BuildPosition);
            newShip.TurnsUntilCompletion = ((UnitType)UnitList.SelectedItem).BuildTime;
            newShip.ShipType = (UnitType)UnitList.SelectedItem;
            _Game.AddUnit(newShip);
            _Builder.Cargo -= newShip.ShipType.Cost;
            PlaySound("Data\\Construction.wav");
            Close();
        }

        private void UnitListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnitList.SelectedItem != null)
            {
                if (_Builder.Cargo >= ((UnitType)UnitList.SelectedItem).Cost)
                {
                    _BuildButton.Enabled = true;
                    SpendText.BackColor = Color.Green;
                }
                else
                {
                    _BuildButton.Enabled = false;
                    SpendText.BackColor = Color.Red;
                }
                UnitDescription.Text = ((UnitType)UnitList.SelectedItem).Name + Environment.NewLine;
                UnitDescription.Text += "Cost: " + ((UnitType)UnitList.SelectedItem).Cost.ToString() + Environment.NewLine;
                UnitDescription.Text += "Time to build: " + ((UnitType)UnitList.SelectedItem).BuildTime.ToString() + Environment.NewLine;
                UnitDescription.Text += "Speed: " + ((UnitType)UnitList.SelectedItem).Speed.ToString() + Environment.NewLine;
                UnitDescription.Text += "Armour: " + ((UnitType)UnitList.SelectedItem).Armour.ToString() + Environment.NewLine;
                if (((UnitType)UnitList.SelectedItem).Capacity >= 1) UnitDescription.Text += "Cargo capacity: " + ((UnitType)UnitList.SelectedItem).Capacity.ToString() + Environment.NewLine;
                if (((UnitType)UnitList.SelectedItem).LightPower > 0 || ((UnitType)UnitList.SelectedItem).HeavyPower > 0)
                {
                    UnitDescription.Text += "Power: " + Math.Max(((UnitType)UnitList.SelectedItem).LightPower, ((UnitType)UnitList.SelectedItem).HeavyPower).ToString() + Environment.NewLine;
                    UnitDescription.Text += "Range: " + Math.Max(((UnitType)UnitList.SelectedItem).LightRange, ((UnitType)UnitList.SelectedItem).HeavyRange).ToString() + Environment.NewLine;
                }
                UnitView.BackColor = DefaultBackColor;
                UnitView.Image = ((UnitType)UnitList.SelectedItem).LargeBitmap;
            }
            else
            {
                _BuildButton.Enabled = false;
                UnitView.BackColor = Color.Black;
                UnitView.Image = null;
                UnitDescription.Text = "";
            }
        }

        public void PlaySound(string fileName)
        {
            _SoundPlayer.SoundLocation = fileName;
            _SoundPlayer.Play();
        }
    }
}
