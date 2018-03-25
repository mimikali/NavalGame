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
    public partial class BuildForm : Form
    {
        Game _Game;
        Point _BuildPosition;
        Unit _Builder;

        public BuildForm(Game game, Unit builder, Point buildPosition)
        {
            _Game = game;
            _Builder = builder;
            _BuildPosition = buildPosition;

            InitializeComponent();
            _BuildButton.Enabled = false;

            foreach (var unitType in UnitType.UnitTypes)
            {
                if (unitType.BuildTime > 0)
                {
                    UnitList.Items.Add(unitType);
                }
            }
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
            _Builder.BuildsLeft--;
            Close();
        }

        private void UnitListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnitList.SelectedItem != null)
            {
                _BuildButton.Enabled = true;
                UnitDescription.Text = ((UnitType)UnitList.SelectedItem).Name;
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
    }
}
