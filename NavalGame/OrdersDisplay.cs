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
        }

        MapDisplay _MapDisplay;
        private Player _NextPlayer;
        List<Rectangle> ButtonLocations = new List<Rectangle>();
        List<Rectangle> CounterLocations = new List<Rectangle>();

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
            if (MapDisplay.Game.CurrentPlayer == null)
            {
                BeginTurnButton.Show();
                NextTurnButton.Hide();
            }
            else
            {
                BeginTurnButton.Hide();
                NextTurnButton.Show();
            }
            if (MapDisplay.Game.SelectedUnit != null)
            {
                MoveBox.Hide();
                LightArtilleryBox.Hide();
                HeavyArtilleryBox.Hide();
                foreach (Order ability in MapDisplay.Game.SelectedUnit.Abilities)
                {
                    switch (ability)
                    {
                        case Order.Move:
                            MoveBox.Show();
                            break;

                        case Order.LightArtillery:
                            LightArtilleryBox.Show();
                            break;

                        case Order.HeavyArtillery:
                            HeavyArtilleryBox.Show();
                            break;
                    }
                }

                MoveBar.Minimum = 0;
                MoveBar.Maximum = MapDisplay.Game.SelectedUnit.Speed;
                MoveBar.Value = (int)Math.Truncate(MapDisplay.Game.SelectedUnit.MovesLeft);

                LightArtilleryBar.Minimum = 0;
                LightArtilleryBar.Maximum = MapDisplay.Game.SelectedUnit.LightShots;
                LightArtilleryBar.Value = MapDisplay.Game.SelectedUnit.LightShotsLeft;

                HeavyArtilleryBar.Minimum = 0;
                HeavyArtilleryBar.Maximum = MapDisplay.Game.SelectedUnit.HeavyShots;
                HeavyArtilleryBar.Value = MapDisplay.Game.SelectedUnit.HeavyShotsLeft;

                HealthBar.Minimum = 0;
                HealthBar.Maximum = 100;
                HealthBar.Value = (int)(MapDisplay.Game.SelectedUnit.Health * 100);
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
    }
}
