using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NavalGame
{
    public partial class ScenarioSelectionForm : Form
    {
        class Scenario
        {
            public string Name;
            public string Description;
            public Bitmap Map;
            public override string ToString()
            {
                return Name;
            }
        }

        public ScenarioSelectionForm()
        {
            InitializeComponent();

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "Test Scenario",
                    Description = "This is the description of this test scenario.",
                    Map = Bitmaps.Get("Data\\TestScenario.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Channel",
                    Description = "THE CHANNEL.",
                    Map = Bitmaps.Get("Data\\TheChannel.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Four Corners",
                    Description = "THE FOUR CORNERS.",
                    Map = Bitmaps.Get("Data\\TheFourCorners.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Archipelago",
                    Description = "THE ARCHIPELAGO.",
                    Map = Bitmaps.Get("Data\\TheArchipelago.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "Wake Island",
                    Description = "WAKE ISLAND.",
                    Map = Bitmaps.Get("Data\\WakeIsland.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Islands",
                    Description = "THE ISLANDS.",
                    Map = Bitmaps.Get("Data\\TheIslands.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Siege of Malta",
                    Description = "THE SIEGE OF MALTA.",
                    Map = Bitmaps.Get("Data\\Malta.png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Islands (3)",
                    Description = "THE ISLANDS 3.",
                    Map = Bitmaps.Get("Data\\TheIslands(3).png")
                });

            ScenarioList.Items.Add(
                new Scenario
                {
                    Name = "The Four Corners (4)",
                    Description = "The Four Corners (4)",
                    Map = Bitmaps.Get("Data\\TheFourCorners(4).png")
                });

            ScenarioList.SelectedIndex = 0;
        }

        private void ScenarioListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScenarioList.SelectedItem != null)
            {
                var scenario = (Scenario)ScenarioList.SelectedItem;
                ScenarioDescriptionBox.Text = scenario.Description;
                MapView.Terrain = new Terrain(scenario.Map);
            }
            else
            {
                ScenarioDescriptionBox.Text = "";
                MapView.Terrain = null;
            }

            MapView.Invalidate();

        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (ScenarioList.SelectedItem != null)
            {
                var scenario = (Scenario)ScenarioList.SelectedItem;
                Hide();
                new Form1(new Game(scenario.Map, scenario.Name), this).ShowDialog();
            }
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Naval Game Files (*.nvg)|*.nvg";

            fileDialog.FileOk += FileDialogFileOk;

            fileDialog.ShowDialog();

        }

        private void FileDialogFileOk(object sender, CancelEventArgs e)
        {
            XElement element = XElement.Load(((FileDialog)sender).FileName);
            new Form1(Game.Load(element), this).ShowDialog();
        }
    }
}
