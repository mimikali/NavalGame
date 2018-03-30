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
        Dictionary<string, List<Faction>> _ScenarioFactions;
        Dictionary<string, List<UnitType>> _ScenarioUnits;
        Dictionary<string, List<Faction>> _ScenarioUnitOwners;
        Dictionary<string, List<Point>> _ScenarioUnitPositions;


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

        public Dictionary<string, List<Faction>> ScenarioFactions
        {
            get
            {
                return _ScenarioFactions;
            }

            set
            {
                _ScenarioFactions = value;
            }
        }

        public Dictionary<string, List<UnitType>> ScenarioUnits
        {
            get
            {
                return _ScenarioUnits;
            }

            set
            {
                _ScenarioUnits = value;
            }
        }

        public Dictionary<string, List<Faction>> ScenarioUnitOwners
        {
            get
            {
                return _ScenarioUnitOwners;
            }

            set
            {
                _ScenarioUnitOwners = value;
            }
        }

        public Dictionary<string, List<Point>> ScenarioUnitPositions
        {
            get
            {
                return _ScenarioUnitPositions;
            }

            set
            {
                _ScenarioUnitPositions = value;
            }
        }

        public ScenarioSelectionForm()
        {
            InitializeComponent();
            ScenarioList.BeginUpdate();
            ScenarioList.Enabled = true;
            ScenarioList.ScrollAlwaysVisible = true;

            ScenarioDescriptions = new Dictionary<string, string>();
            ScenarioTerrain = new Dictionary<string, Terrain>();
            ScenarioFactions = new Dictionary<string, List<Faction>>();
            ScenarioUnits = new Dictionary<string, List<UnitType>>();
            ScenarioUnitOwners = new Dictionary<string, List<Faction>>();
            ScenarioUnitPositions = new Dictionary<string, List<Point>>();

            #region Test Scenario
            ScenarioDescriptions.Add("Test Scenario", "This is a test scenario.");
            ScenarioList.Items.Add("Test Scenario");
            ScenarioFactions.Add("Test Scenario", new List<Faction>() { Faction.USA, Faction.Japan, Faction.Neutral });
            ScenarioUnits.Add("Test Scenario", new List<UnitType>() { UnitType.Port, UnitType.Port });
            ScenarioUnitOwners.Add("Test Scenario", new List<Faction>() { Faction.USA, Faction.Japan });
            ScenarioUnitPositions.Add("Test Scenario", new List<Point>() { new Point(9, 12), new Point(10, 13) });
            ScenarioTerrain.Add("Test Scenario", Game.GenerateTerrain(32, 32, 15756));
            #endregion

            #region Four Corners
            ScenarioDescriptions.Add("Four Corners", "This is a large, slower paced map.");
            ScenarioFactions.Add("Four Corners", new List<Faction>() { Faction.USA, Faction.Germany, Faction.Neutral });
            ScenarioUnits.Add("Four Corners", new List<UnitType>() { UnitType.Port, UnitType.Factory, UnitType.Port, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Port, UnitType.Port, UnitType.Factory, UnitType.Factory, UnitType.Factory, UnitType.Factory });
            ScenarioUnitOwners.Add("Four Corners", new List<Faction>() { Faction.USA, Faction.Neutral, Faction.Germany, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.USA, Faction.Germany, Faction.Neutral, Faction.Neutral, Faction.Neutral, Faction.Neutral });
            ScenarioUnitPositions.Add("Four Corners", new List<Point>() { new Point(5, 59), new Point(6, 63), new Point(58, 4), new Point(51, 2), new Point(59, 59), new Point(58, 55), new Point(13, 1), new Point(0, 8), new Point(10, 16), new Point(47, 43), new Point(19, 44), new Point(38, 16), new Point(29, 39), new Point(43, 36), new Point(23, 31), new Point(42, 29)});

            ScenarioList.Items.Add("Four Corners");

            ScenarioTerrain.Add("Four Corners", MapDisplay.StringToTerrain(64,
            "1111111111000000100000000000000000000000000000000000000001111111" +
            "1100111111111100000000000000000000000000000000000011000001111001" +
            "1111111111111111000000000000000000000000000000000111100000111011" +
            "1111001111111000000000000000000000000000000000001110000000011111" +
            "1111001111000000001000000000000000000000000000000100000000111111" +
            "1111111100000000001000000000011000000000000000000000000000011111" +
            "1111100000000000000000000001111100000000000000000000000010001111" +
            "1111000110000000000000000000010000000000000000000000000000000111" +
            "1110000000000000000000000000000000000000000000000001100000000000" +
            "0100000000000000000000000000000000000000000000000000000000011000" +
            "0000000000000000000000000000000000000000000000000000000000111100" +
            "0000000000000000000000000000000000000000000000000000000000001000" +
            "0000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000001110000000000000000000000000000000000000000000000000" +
            "0000000000111111100000000000000000000110000000000000000000000000" +
            "0000000000011111111000000000000000001111000000000000000000000000" +
            "0000000000000111100000000000000000011111110000000000000000000000" +
            "0000000000000010000000000000011000001111111100000000000000000000" +
            "0000000000000000000000000000000000001111111111000000000000000000" +
            "0000000000000000000000110000000000000000111110000000000000000000" +
            "0000000000000000000000111001111111100000000000000000000000000000" +
            "0000000000000000000001111110111111111010000000000000000000000000" +
            "0000000000000000000001111111011111111011100000000000000000000000" +
            "0000000000000000000111111111011111111101111000000000000000000000" +
            "0000000000000000011111111111011111111101111100000000000000000000" +
            "0000000000000000011111111110111111111110111111110000000000000000" +
            "0000000000000011101111111110111111111111000111000000000000011000" +
            "0000000000000111110001111101111111111111111011110000000000000000" +
            "0000000000011111111110111101111111110111111101110000000000000000" +
            "0000000001111111111110111011111111100011111110010000000000000000" +
            "0000000000001111111110000111111111000011111111101000000000000000" +
            "0000000000000001111111001111111111000011111111111100000000000000" +
            "0000000000011000011111101111111111100111111111111000000000000000" +
            "0000000000111000011111110111111111111111111111100000000000000000" +
            "0000000000011100001111110001111111111111111100000000010000000000" +
            "0000000000111000000111000000111111111111111111110000000000000000" +
            "0000000000010000000000000000111111111111111111111000000000000000" +
            "0000000000000000011111100000011111111111111111111100000000000000" +
            "0000000000000001111111111000111101111111111111111000000000100000" +
            "0000000000000001111111111101111111111111110000111000000011000000" +
            "0000000000000000111111111101111111111100001111000000000000000000" +
            "0000000000000000011111111110111111111011111111110000000000000000" +
            "0000000100000000000111111110111111110111111111000000000000000000" +
            "0000000000000000000000111111011111110111111110000000000000000000" +
            "0000000000000000000000001111011111101111001100000000000000000000" +
            "0000000000000000000000000010101111110010000000000000000000000000" +
            "0000000000000000000000000000000000000000000011111110000000000000" +
            "0000000000000000000000000000000000000000011111111111000000000000" +
            "0000000100000001100000000000000000000000001111111111100000000000" +
            "0000000100000001110000000000000000000000000011111110000000000000" +
            "0000000000000000100000000000000000000000000000110000000000000000" +
            "0001000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000100000000000001110000" +
            "1110000100000000000000000000000000000000001100000000000000111000" +
            "1111110000000000110000000000000000000000000000000000000000110010" +
            "1111100000000000000000011100000000000000000000000000000000000000" +
            "1111100000001000000000000000000000000000000000000000000000000011" +
            "1111110110000000000000000000000000000000000000000000100000011111" +
            "1111111110000000100000000000000000000000000000000000000001111111" +
            "1111111100000000100000000000000000000100000000000000000011100011" +
            "1111100011000000000000000000000000000000000000000001000111110011" +
            "1111011110000000000000000000000000000000000000000000000111111111"));
            #endregion

            #region The Channel
            ScenarioDescriptions.Add("The Channel", "This is a large, slower paced map.");
            ScenarioFactions.Add("The Channel", new List<Faction>() { Faction.England, Faction.Germany, Faction.Neutral });
            ScenarioUnits.Add("The Channel", new List<UnitType>() { });
            ScenarioUnitOwners.Add("The Channel", new List<Faction>() { });
            ScenarioUnitPositions.Add("The Channel", new List<Point>() { });

            ScenarioList.Items.Add("The Channel");

            ScenarioTerrain.Add("The Channel", MapDisplay.StringToTerrain(32,
            "11111111111111111111111111111111" +
            "11111111111111111111111111111111" +
            "11111111111111111111111111111100" +
            "11111111111111111111111110000000" +
            "11111111111111111100000000000000" +
            "11111111111100000000011100000000" +
            "11000000000000000001111111000000" +
            "00000000000000000011111111100000" +
            "00000000000000000011111111100000" +
            "00000000000000000001111111000000" +
            "00000000000000000000001110000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000000000000000000000" +
            "00000000000000011100000000000000" +
            "00000000000011111111110000000000" +
            "00000000011111111111111111100000" +
            "00000111111111111111111111111111" +
            "00111111111111111111111111111111" +
            "11111111111111111111111111111111" +
            "11111111111111111111111111111111"));
            #endregion

            if (ScenarioList.Items.Count >= 1) ScenarioList.SelectedIndex = 0;

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
            new Form1(ScenarioTerrain[SelectedScenario], this, ScenarioFactions[SelectedScenario], ScenarioUnits[SelectedScenario], ScenarioUnitOwners[SelectedScenario], ScenarioUnitPositions[SelectedScenario]).ShowDialog();
        }
    }
}
