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
    public partial class ScenarioSelectionForm : Form
    {
        string _SelectedScenario;
        Dictionary<string, string> _ScenarioDescriptions;
        Dictionary<string, Terrain> _ScenarioTerrain;


        public string SelectedScenario
        {
            get
            {
                return _SelectedScenario;
            }

            set
            {
                _SelectedScenario = value;
            }
        }

        public Dictionary<string, string> ScenarioDescriptions
        {
            get
            {
                return _ScenarioDescriptions;
            }

            set
            {
                _ScenarioDescriptions = value;
            }
        }

        public Dictionary<string, Terrain> ScenarioTerrain
        {
            get
            {
                return _ScenarioTerrain;
            }

            set
            {
                _ScenarioTerrain = value;
            }
        }

        public ScenarioSelectionForm()
        {
            InitializeComponent();
            ScenarioList.Enabled = true;
            ScenarioList.ScrollAlwaysVisible = true;

            ScenarioDescriptions = new Dictionary<string, string>();
            ScenarioTerrain = new Dictionary<string, Terrain>();

            ScenarioTerrain.Add("Test Scenario", Game.GenerateTerrain(32, 32, 15756));
            ScenarioDescriptions.Add("Test Scenario", "This is a test scenario.");

            ScenarioList.BeginUpdate();
            ScenarioList.Items.Add("Test Scenario");
            ScenarioList.EndUpdate();
        }

        private void ScenarioListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScenarioList.SelectedItem == null)
            {
                SelectedScenario = "none";
                ScenarioDescriptionBox.Text = "";
                MapView.Terrain = null;
                MapView.Invalidate();
                return;
            }

            SelectedScenario = ScenarioList.SelectedItem.ToString();
            if (_ScenarioDescriptions.ContainsKey(SelectedScenario))
            {
                ScenarioDescriptionBox.Text = _ScenarioDescriptions[SelectedScenario];
            }
            else
            {
                ScenarioDescriptionBox.Text = "No description.";
            }

            if (_ScenarioTerrain.ContainsKey(SelectedScenario))
            {
                MapView.Terrain = _ScenarioTerrain[SelectedScenario];
                MapView.Invalidate();
            }
            else
            {
                MapView.Image = Bitmaps.Get("Data\\UnimplementedTerrain.png");
                MapView.Invalidate();
            }
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (SelectedScenario == null || !_ScenarioTerrain.ContainsKey(SelectedScenario))
            {
                MessageBox.Show("No scenario selected", "Scenario Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            Hide();
            new Form1(_ScenarioTerrain[_SelectedScenario], this).ShowDialog();
        }
    }
}
